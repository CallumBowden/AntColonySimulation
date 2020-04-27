using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Prototype_3
{
    public partial class PrototypeACsim : Form
    {
        // http://najim.co.uk/Ant-Colony-CSharp/ used as foundation for Prototype 1
        // https://docs.microsoft.com/en-us/archive/msdn-magazine/2012/february/test-run-ant-colony-optimization used for ACO help
        //
        private Random random = new Random(0);
        private List<Ants> list_ants;
        private List<Food> list_food;
        private List<Nest> list_nest;
        private List<Pheromones> list_P;
        private List<Obstacle> list_obst;
        private Random randomAntObj;
        private int stockpile; // measures food in nest
        private int tick; // every tick, there is movement 
        private int foodSources = 0; // measures each source of food left
        private int foodVolume; // measures how muchfood is left over all sources in sim
        private double beta = 1; // distance weight - higher than pheromones so distance is a bigger factor
        private double DecayRate = 0.5; // set by user, pheromone decay rate
        private double Q = 240.0; // constant - large due to speed of sim and size of map
        private int nests;
        private int smartAnts = 0; // smart ants know nest location and will use move probability to choose/move to destination nest
        private int moveCount = 0; // a counter to be used before pheromoneDecay is called due to P dissapearing too fast
        private int obstRedraw = 10; // so obstacles are not redrawn everytime one is placed
        private int obstMax = 60; // max obstacles that can be placed
        private int obstCount = 0; // counter
        //private int difTrigger = 0; // a counter to trigger diffusion - due to preformance hit it produces 
        private bool _start = false; // boolean for start of sim 

        public PrototypeACsim()
        {
            InitializeComponent();
            randomAntObj = new Random();
            list_food = new List<Food>();
            list_nest = new List<Nest>();
            list_P = new List<Pheromones>();
            list_obst = new List<Obstacle>();
            stockpile = 0; // measures how much food in nest
            Tick_Counter.Stop(); // sim starts on startup if .Stop() not used
            // stat box starter values - to be overwritten during sim
            txt_FoodLeft.Text = "0";
            txt_FoodSourcesLeft.Text = "0";
            txt_NestFood.Text = "0";
            txt_Moves.Text = "0";
        }

        // all code below is for the simulation panel

        private void SpawnAnts()
        {
            Ants tempAnt;
            list_ants = new List<Ants>(); // create list of ants to loop through
            int ant_UDV = Convert.ToInt32(txt_StartingNoOfAnts.Text); // UDV = user determined value
            if (ant_UDV <= 0) // validation for quantity of ants
            {
                MessageBox.Show("Number of ants is too low");
                return;
            }
            if (ant_UDV > 400)
            {
                MessageBox.Show("Number of ants is too high"); // later prototype version might use threading to avoid this
                return;
            }
            for (int i = 0; i < ant_UDV; i++)
            {
                tempAnt = new Ants(randomAntObj); // create ant objects
                list_ants.Add(tempAnt); // add them to the list
                int j = i + 1; // +1 due to index an actual value being a difference of 1
                txt_NumOfAnt.Text = j.ToString(); // add num of ants to stat box
                list_ants.ElementAt(i).Smart_Ant = false;
                list_ants.ElementAt(i).destinationNest = false;
            }
        }
        //private void spawnObst() // for if user wants it to spawn on start
        //{
        //    Obstacle tempObst;
        //    list_obst = new List<Obstacle>();
        //    for (int i = 0; i < 100; i++) // i can be changed 
        //    {
        //        tempObst = new Obstacle(random);
        //        list_obst.Add(tempObst);
        //        DrawObstacle(i);
        //    }
        //}
        private void DrawAnts(int antID, int oldPosX, int oldPosY)
        { // draws specified ant on new co-ordinate and wipes previous position (visually)
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawAnt = new SolidBrush(Color.Black))
            using (Brush drawCarryFood = new SolidBrush(Color.Yellow))
            using (Brush brushClear = new SolidBrush(Color.White))
            {
                worldGraphics.FillRectangle(brushClear, oldPosX, oldPosY, 6, 6);
                posX = list_ants.ElementAt(antID).X;
                posY = list_ants.ElementAt(antID).Y;
                worldGraphics.FillRectangle(drawAnt, posX, posY, 5, 5);
                if (list_ants.ElementAt(antID).isCarrying == true)
                {
                    worldGraphics.FillRectangle(drawCarryFood, posX + 1, posY + 1, 4, 4);
                }
            }
        }

        private void DrawPheromones(int pID, int oldPosX, int oldPosY)
        { // place P if val above 2
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawPheromone = new SolidBrush(Color.Purple))
            {
                worldGraphics.FillRectangle(drawPheromone, oldPosX, oldPosY, 2, 2);
            }
        }

        private void DrawPheromones_low(int pID, int oldPosX, int oldPosY)
        { // place P if value below 2
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawPheromone = new SolidBrush(Color.HotPink))
            {
                worldGraphics.FillRectangle(drawPheromone, oldPosX, oldPosY, 2, 2);
            }
        }

        private void DrawPheromonesDecay(int pID, int oldPosX, int oldPosY)
        { // draw updated P at co's X, Y after pheromoneDecay is called
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawPheromone = new SolidBrush(Color.HotPink))
            {
                worldGraphics.FillRectangle(drawPheromone, oldPosX, oldPosY, 2, 2);
            }
        }

        private void ErasePheromones(int pID, int oldPosX, int oldPosY)
        { // erase P from panel
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush clearPheromone = new SolidBrush(Color.White))
            {
                worldGraphics.FillRectangle(clearPheromone, oldPosX, oldPosY, 4, 4);
            }
            list_P.RemoveAt(pID);
            list_P.Insert(pID, null);
        }

        private void DrawFood(int foodID)
        { // draw food on panel
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawFood = new SolidBrush(Color.Red))
            {
                posX = list_food.ElementAt(foodID).X;
                posY = list_food.ElementAt(foodID).Y;
                worldGraphics.FillEllipse(drawFood, posX, posY, 16, 16);
            }
            
        }

        private void EraseFood(int foodID)
        { // remove food from sim - called when food pile is empty
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush ClearFood = new SolidBrush(Color.White))
            {
                posX = list_food.ElementAt(foodID).X;
                posY = list_food.ElementAt(foodID).Y;
                worldGraphics.FillEllipse(ClearFood, posX, posY, 18, 18); // draw food + set size
            }
            for (int i = 0; i < list_ants.Count; i++)
            {
                if (list_ants.ElementAt(i).foodClosest == foodID)
                {
                    list_ants.ElementAt(i).foodLocationKnown = false;
                }
            }
            // Remove food from list
            list_food.RemoveAt(foodID);
            list_food.Insert(foodID, null);
            Console.WriteLine("No more food");
        }

        private void DrawNest(int nestID)
        { // draw nest on panel
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawNest = new SolidBrush(Color.Blue))
            {
                posX = list_nest.ElementAt(nestID).X;
                posY = list_nest.ElementAt(nestID).Y;
                worldGraphics.FillEllipse(drawNest, posX, posY, 22, 22); // draw nest + set size
            }
        }

        private void DrawObstacle(int obstID)
        { // draw obstacle on panel
            if (obstRedraw == 10) // iterator used to stop obstacle redraw being called every move
            {
                obstRedraw = 0;
                for (int i = 0; i < list_obst.Count; i++)
                {
                    obstID = i;
                    int posX, posY;
                    using (Graphics worldGraphics = Panel_sim.CreateGraphics())
                    using (Brush drawObsta = new SolidBrush(Color.DarkGreen))
                    {
                        posX = list_obst.ElementAt(obstID).X;
                        posY = list_obst.ElementAt(obstID).Y;
                        worldGraphics.FillEllipse(drawObsta, posX, posY, 6, 6); // draw obstacle + set size
                    }
                }
            }
            else
            {
                obstRedraw = obstRedraw + 1;
            }
        }

        private void Tick_Counter_tick(object sender, EventArgs e)
        { // runs the simulation
            tick++; // acts as counter
            txt_Moves.Text = tick.ToString();
            for (int i = 0; i < list_ants.Count; i++)
            {
                list_ants.ElementAt(i).isCloseToNest = false;
                list_ants.ElementAt(i).isCloseToFood = false;
                for (int j = 0; j < list_food.Count; j++) // food nearest detection
                {
                    if (list_food.ElementAt(j) != null)
                    {
                        if (list_ants.ElementAt(i).X <= (list_food.ElementAt(j).X + 16) && list_ants.ElementAt(i).X >= (list_food.ElementAt(j).X - 5))
                        // if ant is horizontally close to food
                        {
                            if (list_ants.ElementAt(i).Y <= (list_food.ElementAt(j).Y + 16) && list_ants.ElementAt(i).Y >= (list_food.ElementAt(j).Y - 5))
                            // if ant is vertically close to food
                            {
                                list_ants.ElementAt(i).isCloseToFood = true;
                                list_ants.ElementAt(i).foodClosest = j;
                            }
                        }
                    }
                }
                for (int k = 0; k < list_nest.Count; k++) // nest nearest detection
                {
                    if (list_ants.ElementAt(i).X <= (list_nest.ElementAt(k).X + 20) && list_ants.ElementAt(i).X >= (list_nest.ElementAt(k).X - 5))
                    // if ant is horizontally close to nest
                    {
                        if (list_ants.ElementAt(i).Y <= (list_nest.ElementAt(k).Y + 20) && list_ants.ElementAt(i).Y >= (list_nest.ElementAt(k).Y - 5))
                        // if ant is vertically close to nest
                        {
                            list_ants.ElementAt(i).isCloseToNest = true;
                            list_ants.ElementAt(i).nestClosest = k;
                        }
                    }
                }
                for (int f = 0; f < list_obst.Count; f++) // obstacle nearest detection
                {
                    if (list_ants.ElementAt(i).X <= (list_obst.ElementAt(f).X + 2) && list_ants.ElementAt(i).X >= (list_obst.ElementAt(f).X - 2))
                    // if ant is horizontally close to nest
                    {
                        if (list_ants.ElementAt(i).Y <= (list_obst.ElementAt(f).Y + 2) && list_ants.ElementAt(i).Y >= (list_obst.ElementAt(f).Y - 2))
                        // if ant is vertically close to nest
                        {
                            list_ants.ElementAt(i).isCloseToObst = true;
                            list_ants.ElementAt(i).obstClosest = f;
                        }
                    }
                }
                if (list_ants.ElementAt(i).isCarrying == true)
                {
                    if (list_ants.ElementAt(i).destinationNest == false) // to stop destination nest changing every move
                    {
                        if (nests == 0)
                        {
                            RandomMove_Carrying(i);
                        }
                        else if (smartAnts < Convert.ToInt32(txt_SmartAnts.Text))
                        {
                            smartAnts = smartAnts + 1; // assign this ant to be a smart/elitist ant
                            int nestID = 0; // will be set 
                            int[] distances = new int[nests];
                            for (int m = 0; m < nests; m++)
                            {
                                distances[m] = m;
                            }
                            NextNest(i, nestID, distances);
                            list_ants.ElementAt(i).destinationNest = true;
                            list_ants.ElementAt(i).Smart_Ant = true;
                        }
                    }
                    if (list_ants.ElementAt(i).isCloseToNest == true)
                    {
                        DepositFood(i);
                    }
                    else
                    {
                        if (list_ants.ElementAt(i).Smart_Ant == true)
                        {
                            MoveToDestination(i);
                        }
                        int aX, aY;
                        aX = list_ants.ElementAt(i).X;
                        aY = list_ants.ElementAt(i).Y;
                        int j;
                        for (j = 0; j < list_P.Count; j++) // following P if carrying food
                        {
                            if (list_P.ElementAt(j).X == aX && list_P.ElementAt(j).Y == aY)
                            {
                                followPheromone(i);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (j == list_P.Count - 1 || j == list_P.Count)
                        {
                            RandomMove_Carrying(i); // none smart ants will random move and lay pheromones at a decreased value
                        }
                    }
                }
                else
                {
                    if (list_ants.ElementAt(i).isCloseToFood == true)
                    {
                        PickUpFood(i);
                    }
                    else
                    {
                        int aX, aY;
                        aX = list_ants.ElementAt(i).X;
                        aY = list_ants.ElementAt(i).Y;
                        int j;
                        for (j = 0; j < list_P.Count; j++) // follow P if not carrying food
                        {
                            if (list_P.ElementAt(j).X == aX && list_P.ElementAt(j).Y == aY)
                            {
                                followPheromone(i);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (j == list_P.Count - 1 || j == list_P.Count)
                        {
                            RandomMove(i);
                        }
                    }
                }
                boundary(i);
            }
            moveCount = moveCount + 1;
            //difTrigger = difTrigger + 1;
            if (moveCount == 5) // to stop P decaying too fast
            {
                moveCount = 0;
                decayPheromone(); // loops through list of placed p rather than each ants trail so called at end
                for (int p = 0; p < list_obst.Count; p++) // clean obstacle list so no 2 obstacle objects are on the same position - synced with move count so isnt running every move
                {
                    for (int t = p + 1; t < list_obst.Count; t++)
                    {
                        if (list_obst.ElementAt(p).X == list_obst.ElementAt(t).X && list_obst.ElementAt(p).Y == list_obst.ElementAt(t).Y)
                        {
                            list_obst.RemoveAt(t);
                        }
                    }
                }
                int O = 0;
                DrawObstacle(O); // so obstacles dont disappear
            }
            //if (difTrigger == 20) // attempt at minimalising preformance hit by reducing when diffusion is run
            //{
            //    for (int n = list_P.Count - 1; n > 0; n--)
            //    {
            //        diffusion(n);
            //    }
            //    difTrigger = 0;
            //}
        }

        private void boundary(int antID)
        { // virtual boundary limits below - keep same as panel size
            int aX, aY;
            aX = list_ants.ElementAt(antID).X;
            aY = list_ants.ElementAt(antID).Y;
            if (aX < 0)
            {
                list_ants.ElementAt(antID).X = 763;
            }
            if (aX > 763)
            {
                list_ants.ElementAt(antID).X = 0;
            }
            if (aY < 0)
            {
                list_ants.ElementAt(antID).Y = 416;
            }
            if (aY > 416)
            {
                list_ants.ElementAt(antID).Y = 0;
            }
        }

        private void RandomMove(int antID)
        { // due to randomness of movement, movement distance is doubled to increase range of wandering ants (prevent small circling)
            int randomNum;
            int aX, aY, preX, preY;
            aX = list_ants.ElementAt(antID).X;
            aY = list_ants.ElementAt(antID).Y;
            list_ants.ElementAt(antID).preX = aX;
            list_ants.ElementAt(antID).preY = aY;
            preX = aX;
            preY = aY;
            randomNum = randomAntObj.Next(1, 9); 
            switch (randomNum)
            { // switch statement for movement of ants
                case 1:
                    list_ants.ElementAt(antID).Y--;
                    list_ants.ElementAt(antID).X--;
                    list_ants.ElementAt(antID).Y--;
                    list_ants.ElementAt(antID).X--;
                    break;
                case 2:
                    list_ants.ElementAt(antID).Y--;
                    list_ants.ElementAt(antID).Y--;
                    break;
                case 3:
                    list_ants.ElementAt(antID).Y--;
                    list_ants.ElementAt(antID).X++;
                    list_ants.ElementAt(antID).Y--;
                    list_ants.ElementAt(antID).X++;
                    break;
                case 4:
                    list_ants.ElementAt(antID).X++;
                    list_ants.ElementAt(antID).X++;
                    break;
                case 5:
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).X++;
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).X++;
                    break;
                case 6:
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).Y++;
                    break;
                case 7:
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).X--;
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).X--;
                    break;
                case 8:
                    list_ants.ElementAt(antID).X--;
                    list_ants.ElementAt(antID).X--;
                    break;
            }
            if (list_ants.ElementAt(antID).isCloseToObst == true)
            {
                for (int j = 0; j < list_obst.Count; j++)
                {
                    if (aX + 1 == list_obst.ElementAt(j).X)
                    {
                        list_ants.ElementAt(antID).X = preX;
                        list_ants.ElementAt(antID).X--;
                    }
                    if (aX - 1 == list_obst.ElementAt(j).X)
                    {
                        list_ants.ElementAt(antID).X = preX;
                        list_ants.ElementAt(antID).X++;
                    }
                    if (aY + 1 == list_obst.ElementAt(j).Y)
                    {
                        list_ants.ElementAt(antID).Y = preY;
                        list_ants.ElementAt(antID).Y--;
                    }
                    if (aY - 1 == list_obst.ElementAt(j).Y)
                    {
                        list_ants.ElementAt(antID).Y = preY;
                        list_ants.ElementAt(antID).Y++;
                    }
                }
            }
            boundary(antID);
            DrawAnts(antID, aX, aY);
            if (list_ants.ElementAt(antID).isCloseToNest == true)
            {
                DrawNest(list_ants.ElementAt(antID).nestClosest); // sets id of and draws nest that's close
            }
            if (list_ants.ElementAt(antID).isCloseToFood == true)
            {
                DrawFood(list_ants.ElementAt(antID).foodClosest); // sets id of and draws food that's close
            }
        }

        private void PickUpFood(int antID)
        { // pick up nearby food and call EraseFood if food pile is empty
            int foodClosest;
            foodVolume = Convert.ToInt32(txt_FoodLeft.Text);
            foodVolume = foodVolume - 1;
            txt_FoodLeft.Text = foodVolume.ToString();
            foodClosest = list_ants.ElementAt(antID).foodClosest;
            list_ants.ElementAt(antID).foodSourceID = foodClosest;
            list_food.ElementAt(foodClosest).Quantity--;
            list_ants.ElementAt(antID).isCarrying = true; // changes colour of ant
            Console.WriteLine("food picked up");
            if (list_food.ElementAt(foodClosest).Quantity == 0) // reduce food sources left value and clear food from panel
            {
                foodSources--;
                txt_FoodSourcesLeft.Text = foodSources.ToString(); // updates stat box with correct value
                EraseFood(foodClosest);
            }
        }

        private void DepositFood(int antID)
        { // deposits food an adds to stockpile
            list_ants.ElementAt(antID).isCarrying = false; // isCarrying set to false so colour of ant goes back to default
            stockpile = stockpile + 1;
            txt_NestFood.Text = stockpile.ToString();
            Console.WriteLine("deposited");
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e) // user interaction
        {
            Point clickPoint;
            MouseButtons buttonPressed;
            clickPoint = e.Location;
            buttonPressed = e.Button;
            if (buttonPressed == MouseButtons.Left) // place food
            {
                if (txt_MinFoodPerSource.Text.Trim() == string.Empty || txt_MaxFoodPerSource.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("either min or max food is empty so can't place food");
                    return;
                }
                Random rndNum = new Random();
                int min = Convert.ToInt32(txt_MinFoodPerSource.Text);
                int max = Convert.ToInt32(txt_MaxFoodPerSource.Text);
                Food tempFood;
                tempFood = new Food(clickPoint.X, clickPoint.Y);
                tempFood.Quantity = rndNum.Next(min, max); // takes user input for min and max value and picks random num between those values
                list_food.Add(tempFood);
                if (foodSources > 10)
                {
                    MessageBox.Show("Food Source limit reached: 10");
                }
                else
                {
                    DrawFood(list_food.Count - 1);
                    foodSources++;
                    foodVolume = foodVolume + tempFood.Quantity;
                    txt_FoodSourcesLeft.Text = foodSources.ToString();
                    txt_FoodLeft.Text = foodVolume.ToString();
                }
            }
            if (buttonPressed == MouseButtons.Right) // place nest 
            {
                Nest tempNest;
                tempNest = new Nest(clickPoint.X, clickPoint.Y);
                if (nests > 4)
                {
                    MessageBox.Show("Nest limit reached: 4");
                }
                else
                {
                    list_nest.Add(tempNest);
                    DrawNest(list_nest.Count - 1);
                    nests = nests + 1; // For ACO
                }
            }
            if (buttonPressed == MouseButtons.Middle)
            { // place object/obstacle in small square around clickpoint
                if (_start == false)
                {
                    MessageBox.Show("Obstacles can't be placed before starting sim");
                }
                if (obstCount >= obstMax)
                {
                    MessageBox.Show("Obstacle limit reached: 60");
                }
                else
                { // multiple placed so ants dont appear to sink into obstacle + obstacles place are visible (small draw size)
                    Obstacle tempObj;
                    int x = clickPoint.X;
                    int y = clickPoint.Y;
                    tempObj = new Obstacle(x, y);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x - 1, y - 1);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x + 1, y - 1);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x - 1, y + 1);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x + 1, y + 1);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x - 1, y);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x + 1, y);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x, y + 1);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    tempObj = new Obstacle(x, y - 1);
                    list_obst.Add(tempObj);
                    DrawObstacle(list_obst.Count - 1);
                    obstCount++; // max obst count for clicks, not actual obstacle objects
                }
            }
        }

        private void RandomMove_Carrying(int antID)
        { // movement is halved for carrying ants
            int randomNum;
            int aX, aY, preX, preY;
            aX = list_ants.ElementAt(antID).X;
            aY = list_ants.ElementAt(antID).Y;
            list_ants.ElementAt(antID).preX = aX;
            list_ants.ElementAt(antID).preY = aY;
            preX = aX;
            preY = aY;
            randomNum = randomAntObj.Next(1, 9); // 1 to 8 clockwise, 1 = northwest
            switch (randomNum)
            { // switch statement for movement of ants
                case 1:
                    list_ants.ElementAt(antID).Y--;
                    list_ants.ElementAt(antID).X--;
                    break;
                case 2:
                    list_ants.ElementAt(antID).Y--;
                    break;
                case 3:
                    list_ants.ElementAt(antID).Y--;
                    list_ants.ElementAt(antID).X++;
                    break;
                case 4:
                    list_ants.ElementAt(antID).X++;
                    break;
                case 5:
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).X++;
                    break;
                case 6:
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).Y++;
                    break;
                case 7:
                    list_ants.ElementAt(antID).Y++;
                    list_ants.ElementAt(antID).X--;
                    break;
                case 8:
                    list_ants.ElementAt(antID).X--;
                    break;
            }
            if (list_ants.ElementAt(antID).isCloseToObst == true)
            {
                for (int j = 0; j < list_obst.Count; j++)
                {
                    if (aX + 1 == list_obst.ElementAt(j).X)
                    {
                        list_ants.ElementAt(antID).X = preX;
                        list_ants.ElementAt(antID).X--;
                    }
                    if (aX - 1 == list_obst.ElementAt(j).X)
                    {
                        list_ants.ElementAt(antID).X = preX;
                        list_ants.ElementAt(antID).X++;
                    }
                    if (aY + 1 == list_obst.ElementAt(j).Y)
                    {
                        list_ants.ElementAt(antID).Y = preY;
                        list_ants.ElementAt(antID).Y--;
                    }
                    if (aY - 1 == list_obst.ElementAt(j).Y)
                    {
                        list_ants.ElementAt(antID).Y = preY;
                        list_ants.ElementAt(antID).Y++;
                    }
                }
            }
            boundary(antID);
            DrawAnts(antID, aX, aY);
            if (list_ants.ElementAt(antID).isCloseToNest == true)
            {
                DrawNest(list_ants.ElementAt(antID).nestClosest); // sets id of and draws nest that's close
            }
            if (list_ants.ElementAt(antID).isCloseToFood == true)
            {
                DrawFood(list_ants.ElementAt(antID).foodClosest); // sets id of and draws food that's close
            }
            int foodID = list_ants.ElementAt(antID).foodSourceID;
            layPheromone(antID, foodID);
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            // no code needed - event purely to allow panel to be painted on
        }
        
        // code below is for ACO

        private void followPheromone(int antID)
        { // if ant opts to follow trail, this method tells them what to do
            int rnd_num = random.Next(1, 99);
            if (list_ants.ElementAt(antID).isCarrying == true)
            {
                rnd_num = rnd_num - 5;
            }
            if (list_ants.ElementAt(antID).followP == true)
            {
                rnd_num = rnd_num + 8;
            }
            int probUDV = Convert.ToInt32(txt_followProb.Text); // use this elsewhere
            int pSTR = Convert.ToInt32(txt_PheromoneStrength.Text);
            int prob = probUDV + (pSTR / 5);
            if (prob < rnd_num || list_P.Count < 2)
            {
                RandomMove(antID);
                list_ants.ElementAt(antID).followP = false;
                return;
            }
            else
            {
                list_ants.ElementAt(antID).followP = true;
                List<int> pIDList = new List<int>();
                int antX = list_ants.ElementAt(antID).X;
                int antY = list_ants.ElementAt(antID).Y;
                int pX, pY = 0; // same for pX and pY values, ant pos will change to pY, pX with of pID with strongest value
                int i, j; // , k, l;
                for (i = 0; i < list_P.Count; i++) // for loop to get id no. of pheromone on current location
                {
                    if (list_P.ElementAt(i).X == antX && list_P.ElementAt(i).Y == antY)
                    {
                        pX = list_P.ElementAt(i).X;
                        pY = list_P.ElementAt(i).Y;
                        for (j = 0; j < list_P.Count; j++) // loop to get surrounding p
                        {
                            if (list_P.ElementAt(j).X == pX - 1 && list_P.ElementAt(j).Y == pY)
                            {
                                pIDList.Add(j);
                            }
                            else if (list_P.ElementAt(j).X == pX + 1 && list_P.ElementAt(j).Y == pY)
                            {
                                pIDList.Add(j);
                            }
                            else if (list_P.ElementAt(j).X == pX && list_P.ElementAt(j).Y - 1 == pY)
                            {
                                pIDList.Add(j);
                            }
                            else if (list_P.ElementAt(j).X == pX && list_P.ElementAt(j).Y + 1 == pY)
                            {
                                pIDList.Add(j);
                            }
                            else if (list_P.ElementAt(j).X == pX - 1 && list_P.ElementAt(j).Y + 1 == pY)
                            {
                                pIDList.Add(j);
                            }
                            else if (list_P.ElementAt(j).X == pX - 1 && list_P.ElementAt(j).Y - 1 == pY)
                            {
                                pIDList.Add(j);
                            }
                            else if (list_P.ElementAt(j).X == pX + 1 && list_P.ElementAt(j).Y + 1 == pY)
                            {
                                pIDList.Add(j);
                            }
                            else if (list_P.ElementAt(j).X == pX + 1 && list_P.ElementAt(j).Y - 1 == pY)
                            {
                                pIDList.Add(j);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                //for (k = 0; k < pIDList.Count; k++) // p value based movement - commented out but kept if needed
                //{
                //    int id = pIDList[k];
                //    double temp = list_P.ElementAt(id).value;
                //    int valueid = -1; // -1 so if pIDList has a count of 0
                //    for (l = 0; l < pIDList.Count; l++)
                //    {
                //        int id2 = pIDList[l];
                //        double temp2 = list_P.ElementAt(id2).value;
                //        if (temp >= temp2)
                //        {
                //            valueid = id;
                //        }
                //    }
                //    if (valueid <= 0)
                //    {
                //        RandomMove(antID);
                //        break;
                //    }
                //    else
                //    {
                //        list_ants.ElementAt(antID).X = list_P.ElementAt(valueid).X;
                //        list_ants.ElementAt(antID).Y = list_P.ElementAt(valueid).Y;
                //    }
                //}
                if (pIDList.Count < 1)
                {
                    RandomMove(antID);
                    list_ants.ElementAt(antID).followP = false;
                    return;
                }
                int max = pIDList.Count;
                int rnd_num2 = random.Next(0, max); // random move through placed P nearby
                int move = pIDList[rnd_num2];
                list_ants.ElementAt(antID).X = list_P.ElementAt(move).X;
                list_ants.ElementAt(antID).Y = list_P.ElementAt(move).Y;
                boundary(antID);
                DrawAnts(antID, antX, antY);
                if (list_ants.ElementAt(antID).isCloseToNest == true)
                {
                    DrawNest(list_ants.ElementAt(antID).nestClosest); // sets id and draws nest that's close
                }
                if (list_ants.ElementAt(antID).isCloseToFood == true)
                {
                    DrawFood(list_ants.ElementAt(antID).foodClosest); // sets id and draws food that's close
                }
                if (i == list_P.Count - 1 || list_P.Count == 0) // if pheromone can not be found
                {
                    RandomMove(antID);
                    list_ants.ElementAt(antID).followP = false;
                    return;
                }
            }
        }

        private void MoveToDestination(int antID) // assumes nest location is known - pathfinding may need to be used
        {
            int aX, aY, preX, preY;
            aX = list_ants.ElementAt(antID).X;
            aY = list_ants.ElementAt(antID).Y;
            list_ants.ElementAt(antID).preX = aX;
            list_ants.ElementAt(antID).preY = aY;
            preX = aX;
            preY = aY;
            int rndNum;
            rndNum = random.Next(0, 5);
            if (list_nest.Count == 0) // add randomness to move
            {
                RandomMove_Carrying(antID);
                return;
            }
            else
            {
                if (rndNum == 0) // move randomly
                {
                    RandomMove_Carrying(antID);
                }
                if (rndNum == 1 || rndNum == 2) // move in x direction
                {
                    for (int i = 0; i < list_nest.Count; i++)
                    {
                        if (i == list_ants.ElementAt(antID).DestNestID) // if nest id == set destination for ant
                        {
                            if (list_nest.ElementAt(i).X < aX)
                            {
                                list_ants.ElementAt(antID).X--; // horizontal move
                            }
                            if (list_nest.ElementAt(i).X > aX)
                            {
                                list_ants.ElementAt(antID).X++; // horizontal move
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < list_nest.Count; i++)
                    {
                        if (i == list_ants.ElementAt(antID).DestNestID) // if nest id == set destination for ant
                        {
                            if (list_nest.ElementAt(i).Y > aY)
                            {
                                list_ants.ElementAt(antID).Y++; // verticle move
                            }
                            if (list_nest.ElementAt(i).Y < aY)
                            {
                                list_ants.ElementAt(antID).Y--; // verticle move
                            }
                        }
                    }
                }
                aX = list_ants.ElementAt(antID).X;
                aY = list_ants.ElementAt(antID).Y;
                RandomMove_Carrying(antID); // this is here to add randomness to movement
                // obstacle navigation
                if (list_ants.ElementAt(antID).isCloseToObst == true)
                {
                    for (int j = 0; j < list_obst.Count; j++)
                    {
                        if (aX + 1 == list_obst.ElementAt(j).X)
                        {
                            list_ants.ElementAt(antID).X = preX;
                            list_ants.ElementAt(antID).X--;
                        }
                        if (aX - 1 == list_obst.ElementAt(j).X)
                        {
                            list_ants.ElementAt(antID).X = preX;
                            list_ants.ElementAt(antID).X++;
                        }
                        if (aY + 1 == list_obst.ElementAt(j).Y)
                        {
                            list_ants.ElementAt(antID).Y = preY;
                            list_ants.ElementAt(antID).Y--;
                        }
                        if (aY - 1 == list_obst.ElementAt(j).Y)
                        {
                            list_ants.ElementAt(antID).Y = preY;
                            list_ants.ElementAt(antID).Y++;
                        }
                    }
                }
                aX = list_ants.ElementAt(antID).X;
                aY = list_ants.ElementAt(antID).Y;
                int nestID = list_ants.ElementAt(antID).DestNestID;
                layPheromone_Strong(antID, nestID);
                DrawAnts(antID, aX, aY);
            }
        }

        private int Distance(int antID, int nestID, int[] distances)
        {
            int i;
            nestID = 0;
            for (i = 0; i < nests; i++) // gets distance between ant and nests
            {
                int antX = list_ants.ElementAt(antID).X;
                int antY = list_ants.ElementAt(antID).Y;
                int nestX = list_nest.ElementAt(nestID).X;
                int nestY = list_nest.ElementAt(nestID).Y;
                int x = antX - nestX;
                int y = antY - nestY;
                int d;
                if (x < 0) // no negative x value
                {
                    x = x * -1;
                }
                if (y < 0) // no negative y value
                {
                    y = y * -1;
                }
                d = (x + y) / 100; // high value/wrong eq due to distance being main factor + replace with pathfinding
                distances[i] = d;
                nestID++;
            }
            i = i - 1;
            return distances[i]; // return distance between ant and nest
        }

        private int NextNest(int antID, int nestID, int[] distances)
        { // for ant k at food source, what is next/nearest nest?
            if (nests == 0)
            {
                RandomMove(antID);
                return antID;
            }
            else
            {
                try
                {
                    double[] probs = MoveProbs(antID, nestID, distances);
                    double[] cumulative = new double[probs.Length + 1];
                    for (int i = 0; i < probs.Length; i++)
                    {
                        cumulative[i + 1] = cumulative[i] + probs[i];
                    }
                    double p = random.NextDouble();
                    for (int i = 0; i < cumulative.Length - 1; i++)
                    {
                        if (p >= cumulative[i] && p < cumulative[i + 1]) // decides a nest based on how rndnum p compares to values obtained
                        {
                            list_ants.ElementAt(antID).DestNestID = i;
                            return i;
                        }
                    }
                }
                catch
                {
                    RandomMove(antID);
                    return antID;
                }
            }
            throw new Exception("Failure to return valid nest"); // catches nest errors
        }

        private double[] MoveProbs(int antID, int nestID, int[] distances)
        { // for ant k, located at food source, return the prob of moving to each nest
            int _nests = nests; 
            double[] T = new double[_nests];
            float alpha = int.Parse(txt_PheromoneStrength.Text); // pheromone weight
            double sum = 0.0; // sum of all T
            for (int i = 0; i < list_nest.Count; i++)
            {
                double tempP = 0.2 * (alpha); // 0.2 * p Strength - because trails might not exist/decayed
                T[i] = Math.Pow(tempP, alpha) * Math.Pow(1.0 / Distance(antID, nestID, distances), beta);
                if (T[i] < 0.0001)
                {
                    T[i] = 0.0001;
                }
                else if (T[i] > (double.MaxValue / (_nests * 100)))
                {
                    T[i] = double.MaxValue / (_nests * 100);
                }
                sum += T[i];
                nestID++;
            }
            double[] probs = new double[_nests];
            for (int i = 0; i < probs.Length; i++)
            {
                probs[i] = T[i] / sum;
                // big trouble if sum = 0.0
            }
            return probs;
        }

        private void decayPheromone() //REALLY STRONG FOR SOME REASON
        { // loop through list of pheromones (at each coordinate) and decrease it
            DecayRate = Convert.ToDouble(txt_PheromoneDecayRate.Text);
            DecayRate = DecayRate / 100;
            for (int i = list_P.Count - 1; i >= 0; i--)
            {
                int pX, pY;
                pX = list_P.ElementAt(i).X;
                pY = list_P.ElementAt(i).Y;
                double tempP = double.Parse(Convert.ToString(list_P.ElementAt(i).value));
                if (tempP <= 0.12) // remove pheromone from sim 
                {
                    ErasePheromones(i, pX, pY);
                    list_P.RemoveAt(i);
                }
                else
                {
                    double dec = ((1 - DecayRate) * tempP) * 0.88; // *0.88 due to extreme strength of decay rate
                    list_P.ElementAt(i).value = dec;
                    if (dec < 2)
                    {
                        DrawPheromonesDecay(i, pX, pY);
                    }
                    else
                    {
                        DrawPheromones(i, pX, pY);
                    }
                }
            }
        }
        //private void diffusion(int pID) // causing major preformance issues - commented out for now
        //{
        //    Pheromones ph, ph1, ph2, ph3;
        //    int pX = list_P.ElementAt(pID).X;
        //    int pY = list_P.ElementAt(pID).Y;
        //    bool phflag = false, ph1flag = false, ph2flag = false, ph3flag = false;
        //    double tempP = double.Parse(Convert.ToString(list_P.ElementAt(pID).value));
        //    if (list_P.Count == 0)
        //    {
        //        return;
        //    }
        //    for (int j = 0; j < list_P.Count; j++) // loop to get surrounding p
        //    {
        //        if (list_P.ElementAt(j).X == pX - 1 && list_P.ElementAt(j).Y == pY)
        //        {
        //            list_P.ElementAt(j).value = (list_P.ElementAt(j).value + list_P.ElementAt(pID).value) / 2.5;
        //            phflag = true;
        //        }
        //        if (list_P.ElementAt(j).X == pX + 1 && list_P.ElementAt(j).Y == pY)
        //        {
        //            list_P.ElementAt(j).value = (list_P.ElementAt(j).value + list_P.ElementAt(pID).value) / 2.5;
        //            ph1flag = true;
        //        }
        //        if (list_P.ElementAt(j).X == pX && list_P.ElementAt(j).Y - 1 == pY)
        //        {
        //            list_P.ElementAt(j).value = (list_P.ElementAt(j).value + list_P.ElementAt(pID).value) / 2.5;
        //            ph2flag = true;
        //        }
        //        if (list_P.ElementAt(j).X == pX && list_P.ElementAt(j).Y + 1 == pY)
        //        {
        //            list_P.ElementAt(j).value = (list_P.ElementAt(j).value + list_P.ElementAt(pID).value) / 2.5;
        //            ph3flag = true;
        //        }
        //    }
        //    if (phflag == false)
        //    {
        //        ph = new Pheromones(pX - 1, pY);
        //        ph.value = tempP / 2.5;
        //        list_P.Add(ph);
        //        if (ph.value < 2)
        //        {
        //            DrawPheromones_low(list_P.Count - 1, pX - 1, pY);
        //        }
        //        else
        //        {
        //            DrawPheromones(list_P.Count - 1, pX - 1, pY);
        //        }
        //    }
        //    if (ph1flag == false)
        //    {
        //        ph1 = new Pheromones(pX + 1, pY);
        //        ph1.value = tempP / 2.5;
        //        list_P.Add(ph1);
        //        if (ph1.value < 2)
        //        {
        //            DrawPheromones_low(list_P.Count - 1, pX + 1, pY);
        //        }
        //        else
        //        {
        //            DrawPheromones(list_P.Count - 1, pX + 1, pY);
        //        }
        //    }
        //    if (ph2flag == false)
        //    {
        //        ph2 = new Pheromones(pX, pY - 1);
        //        ph2.value = tempP / 2.5;
        //        list_P.Add(ph2);
        //        if (ph2.value < 2)
        //        {
        //            DrawPheromones_low(list_P.Count - 1, pX, pY - 1);
        //        }
        //        else
        //        {
        //            DrawPheromones(list_P.Count - 1, pX, pY - 1);
        //        }
        //    }
        //    if (ph3flag == false)
        //    {
        //        ph3 = new Pheromones(pX, pY + 1);
        //        ph3.value = tempP / 2.5;
        //        list_P.Add(ph3);
        //        if (ph3.value < 2)
        //        {
        //            DrawPheromones_low(list_P.Count - 1, pX, pY + 1);
        //        }
        //        else
        //        {
        //            DrawPheromones(list_P.Count - 1, pX, pY + 1);
        //        }
        //    }
        //}
        private void layPheromone_Strong(int antID, int nestID)
        {
            bool flag = false;
            int length = 0;
            int px, py; // pheromone coordinates
            int ax, ay; // ant coordinates
            ax = list_ants.ElementAt(antID).X;
            ay = list_ants.ElementAt(antID).Y;
            double pSTR = int.Parse(txt_PheromoneStrength.Text); // is a multiplier between 0.0-10.0
            pSTR = pSTR / 10;
            double inc = 0.0;
            while (flag == false)
            {
                for (int i = 0; i < list_P.Count; i++)
                { // loop through every valid pheromone so correct one can be updated
                    px = list_P.ElementAt(i).X;
                    py = list_P.ElementAt(i).Y;
                    if (px == ax && py == ay)
                    {
                        double tempP = Convert.ToDouble(list_P.ElementAt(i).value);
                        length = (list_ants.ElementAt(antID).X - list_nest.ElementAt(nestID).X) + (list_ants.ElementAt(antID).Y - list_nest.ElementAt(nestID).Y);
                        if (length < 0)
                        {
                            length = length * -1;
                        }
                        inc = Q / length;
                        inc = inc * 1.2; // multiplier effecting strength of P, elitist ants lay stronger P
                        inc = inc * pSTR; // Strength effcting P
                        list_P.ElementAt(i).value = tempP + inc;
                        flag = true;
                        if (list_P.ElementAt(i).value < 2)
                        {
                            DrawPheromones_low(antID, ax, ay);
                            DrawAnts(antID, ax, ay); // so ant is visible
                        }
                        else
                        {
                            DrawPheromones(antID, ax, ay);
                            DrawAnts(antID, ax, ay); // so ant is visible
                        }
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                // code below if ant sits on coordinate with no pheromone
                Pheromones phero;
                phero = new Pheromones(ax, ay);
                length = (list_ants.ElementAt(antID).X - list_nest.ElementAt(nestID).X) + (list_ants.ElementAt(antID).Y - list_nest.ElementAt(nestID).Y);
                if (length < 0)
                {
                    length = length * -1;
                }
                inc = Q / length;
                inc = inc * 1.2; // multiplier effecting strength of P, elitist ants lay stronger P
                inc = inc * pSTR; // Strength effcting P
                phero.value = inc;
                list_P.Add(phero);
                if (phero.value < 2)
                {
                    DrawPheromones_low(antID, ax, ay);
                    DrawAnts(antID, ax, ay); // so ant is visible
                }
                else
                {
                    DrawPheromones(antID, ax, ay);
                    DrawAnts(antID, ax, ay); // so ant is visible
                }
                flag = true;
            }
        }

        private void layPheromone(int antID, int foodID)
        {
            bool flag = false;
            int length = 0;
            int px, py; // pheromone coordinates
            int ax, ay; // ant coordinates
            ax = list_ants.ElementAt(antID).X;
            ay = list_ants.ElementAt(antID).Y;
            double pSTR = int.Parse(txt_PheromoneStrength.Text); // is a multiplier between 0.0-10.0
            pSTR = pSTR / 10;
            double inc = 0.0;
            while (flag == false)
            {
                for (int i = 0; i < list_P.Count; i++)
                { // loop through every valid pheromone so correct one can be updated
                    px = list_P.ElementAt(i).X;
                    py = list_P.ElementAt(i).Y;
                    if (px == ax && py == ay)
                    {
                        double tempP = Convert.ToDouble(list_P.ElementAt(i).value);
                        try
                        {
                            length = (list_ants.ElementAt(antID).X - list_food.ElementAt(foodID).X) + (list_ants.ElementAt(antID).Y - list_food.ElementAt(foodID).Y); // throws an object instance error at long runtime of sim if food sources disappear
                        }
                        catch
                        {
                            length = 5;
                        }
                        if (length < 0)
                        {
                            length = length * -1;
                        }
                        inc = Q / length;
                        inc = inc * 0.8; // multiplier effecting strength of P, none elitist ants lay weaker P
                        inc = inc * pSTR; // Strength effcting P, multipliers used as P was becoming infinite at times
                        list_P.ElementAt(i).value = tempP + inc;
                        flag = true;
                        if (list_P.ElementAt(i).value < 2)
                        {
                            DrawPheromones_low(antID, ax, ay);
                            DrawAnts(antID, ax, ay); // so ant is visible
                        }
                        else
                        {
                            DrawPheromones(antID, ax, ay);
                            DrawAnts(antID, ax, ay); // so ant is visible
                        }
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                // code below if ant sits on coordinate with no pheromone
                Pheromones phero;
                phero = new Pheromones(ax, ay);
                try
                {
                    length = (list_ants.ElementAt(antID).X - list_food.ElementAt(foodID).X) + (list_ants.ElementAt(antID).Y - list_food.ElementAt(foodID).Y); // throws an object instance error at long runtime of sim
                }
                catch
                {
                    length = 5;
                }
                if (length < 0)
                {
                    length = length * -1;
                }
                inc = Q / length;
                inc = inc * 0.8;
                inc = inc * pSTR; // Strength effcting P
                phero.value = inc;
                list_P.Add(phero);
                if (phero.value < 2)
                {
                    DrawPheromones_low(antID, ax, ay);
                    DrawAnts(antID, ax, ay); // so ant is visible
                }
                else
                {
                    DrawPheromones(antID, ax, ay);
                    DrawAnts(antID, ax, ay); // so ant is visible
                }
                flag = true;
            }
        }

        // Code below is for the other buttons/settings 

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UserEntry_TextEntered(object sender, KeyPressEventArgs e)
        {   // IsDigit checks if specified character is catagorised as digit
            // char.IsControl returns true/false depending on character being a non-printing (special) character
            // use KeyChar to sample/modify keystrokes at runtime
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        { // clears old sim first
            _start = true;
            Panel_sim.Refresh();
            // validate user entry - stop user breaking sim 
            // num of ants
            if (txt_StartingNoOfAnts.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No value entered - Num of Ants");
                return;
            }
            // min food source size
            if (txt_MinFoodPerSource.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No value entered - Min food");
                return;
            }
            if (txt_MaxFoodPerSource.Text.Trim() == string.Empty) // max food - to stop invalid input string error
            {
                MessageBox.Show("No value entered - Max food");
                return;
            }
            if (Convert.ToInt32(txt_MinFoodPerSource.Text) <= 0)
            {
                MessageBox.Show("Min Food Too Low - below 0");
                return;
            }
            if (Convert.ToInt32(txt_MinFoodPerSource.Text) > Convert.ToInt32(txt_MaxFoodPerSource.Text))
            {
                MessageBox.Show("Min can't be higher than Max food");
                return;
            }
            // max food source size
            if (txt_MaxFoodPerSource.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No value entered - Max food");
                return;
            }
            if (Convert.ToInt32(txt_MaxFoodPerSource.Text) <= 0)
            {
                MessageBox.Show("Max food Too Low");
                return;
            }
            // follow probability
            if (txt_followProb.Text.Trim() == string.Empty) // may leave out so num of obsticles defaults to 0
            {
                MessageBox.Show("No value entered - follow probability");
                return;
            }
            if (Convert.ToInt32(txt_followProb.Text) < 0 || Convert.ToInt32(txt_followProb.Text) > 100)
            {
                MessageBox.Show("follow prob Value is not between 0-100");
                return;
            }
            // pheromone strength
            if (txt_PheromoneStrength.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No value entered - Pheromone strength");
                return;
            }
            if (Convert.ToInt32(txt_PheromoneStrength.Text) < 0 || Convert.ToInt32(txt_PheromoneStrength.Text) > 100)
            {
                MessageBox.Show("P strength Value is not between 0-100");
                return;
            }
            // pheromone decay rate
            if (txt_PheromoneDecayRate.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No value entered - Decay Rate");
                return;
            }
            if (Convert.ToInt32(txt_PheromoneDecayRate.Text) < 0 || Convert.ToInt32(txt_PheromoneDecayRate.Text) > 100)
            {
                MessageBox.Show("P decay rate Value is not between 0-100");
                return;
            }
            // smart Ants
            if (txt_SmartAnts.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No value entered - Smart Ants");
                return;
            }
            if (Convert.ToInt32(txt_SmartAnts.Text) < 0 || Convert.ToInt32(txt_SmartAnts.Text) > Convert.ToInt32(txt_StartingNoOfAnts.Text))
            {
                MessageBox.Show("Smart ant number must be 0 or above, and less than the number of ants");
                return;
            }
            txt_StartingNoOfAnts.Enabled = false;
            txt_SmartAnts.Enabled = false;
            txt_PheromoneDecayRate.Enabled = false;
            txt_PheromoneStrength.Enabled = false;
            txt_followProb.Enabled = false;
            txt_MaxFoodPerSource.Enabled = false;
            txt_MinFoodPerSource.Enabled = false;
            // start simulation
            SpawnAnts();
            Tick_Counter.Start();
        }

        private void btn_Stop_Click(object sender, EventArgs e) // stops sim, and clears it
        {
            _start = false;
            for (int j = 0; j < 10; j++) // for loop because refresh does not clear properly first time for some reason
            {
                if (list_ants == null)
                {
                    MessageBox.Show("Ants list == Null. Can't run reset properly. Please restart program");
                    Close();
                }
                else
                {
                    Tick_Counter.Stop();
                    Panel_sim.Refresh();
                    // remove all food and nests and P from existance
                    for (int i = 0; i < list_food.Count; i++)
                    {
                        list_food.RemoveAt(i);
                        foodSources = 0;
                        foodVolume = 0;
                        txt_FoodSourcesLeft.Text = foodSources.ToString();
                        txt_FoodLeft.Text = foodVolume.ToString();
                    }
                    for (int i = 0; i < list_nest.Count; i++)
                    {
                        list_nest.RemoveAt(i);
                        nests = 0;
                    }
                    for (int i = 0; i < list_P.Count; i++)
                    {
                        int x, y;
                        x = list_P.ElementAt(i).X;
                        y = list_P.ElementAt(i).Y;
                        ErasePheromones(i, x, y);
                        list_P.RemoveAt(i);
                    }
                    for (int i = 0; i < list_ants.Count; i++)
                    {
                        list_ants.ElementAt(i).isCarrying = false;
                        list_ants.ElementAt(i).destinationNest = false;
                        list_ants.RemoveAt(i);
                    }
                    for (int i = 0; i < list_obst.Count; i++)
                    {
                        list_obst.RemoveAt(i);
                    }
                    txt_SmartAnts.Enabled = true;
                    txt_PheromoneDecayRate.Enabled = true;
                    txt_PheromoneStrength.Enabled = true;
                    txt_followProb.Enabled = true;
                    txt_MaxFoodPerSource.Enabled = true;
                    txt_MinFoodPerSource.Enabled = true;
                    txt_StartingNoOfAnts.Enabled = true;
                    smartAnts = 0;
                    txt_NestFood.Text = "0";
                    tick = 0;
                }
            }
        }
    }
}