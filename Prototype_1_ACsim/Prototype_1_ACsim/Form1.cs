using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype_1_ACsim
{
    public partial class PrototypeACsim : Form
    {
        //
        // http://najim.co.uk/Ant-Colony-CSharp/ used as inspiration and a foundation for which prototype 1 is built
        // prototype 2 will have proper pathfinding, including Ant Colony optimisation Algorithm (ACO) as project requires
        // the pathfinding was included because in prototype 1, ACO will not be implimented - it is still buggy however
        // all classes and methods are subject to change in further versions
        //

        private List<Ants> list_ants;
        private List<Food> list_food;
        private List<Nest> list_nest; 
        private Random randomAntObj;
        private int stockpile; // measures food in nest
        private int tick; // every tick, there is movement 
        private int foodSources; // measures each source of food left
        private int foodVolume; // measures how muchfood is left over all sources in sim

        public PrototypeACsim()
        {
            InitializeComponent();
            randomAntObj = new Random();
            list_food = new List<Food>();
            list_nest = new List<Nest>();
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
            if (ant_UDV > 1500)
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
            }
        }

        private void DrawAnts(int antID, int oldPosX, int oldPosY)
        {
            // draws specified ant on new co-ordinate and wipes previous position (visually)
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawAnt = new SolidBrush(Color.Black))
            using (Brush drawCarryFood = new SolidBrush(Color.Yellow))
            using (Brush brushClear = new SolidBrush(Color.LightGray))
            {
                worldGraphics.FillRectangle(brushClear, oldPosX, oldPosY, 5, 5);
                posX = list_ants.ElementAt(antID).X;
                posY = list_ants.ElementAt(antID).Y;
                worldGraphics.FillRectangle(drawAnt, posX, posY, 5, 5);
                if (list_ants.ElementAt(antID).isCarrying == true)
                {
                    worldGraphics.FillRectangle(drawCarryFood, posX + 1, posY + 1, 3, 3);
                }
            }
        }

        private void DrawFood(int foodID)
        { 
            // draw food on panel
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawFood = new SolidBrush(Color.Red))
            {
                posX = list_food.ElementAt(foodID).X;
                posY = list_food.ElementAt(foodID).Y;
                worldGraphics.FillEllipse(drawFood, posX, posY, 15, 15);
            }
        }

        private void EraseFood(int foodID)
        {
            // remove food from sim - called when food pile is empty
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush ClearFood = new SolidBrush(Color.White))
            {
                posX = list_food.ElementAt(foodID).X;
                posY = list_food.ElementAt(foodID).Y;

                worldGraphics.FillEllipse(ClearFood, posX, posY, 14, 14); // draw food + set size
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
        {
            // draw nest on panel
            int posX, posY;
            using (Graphics worldGraphics = Panel_sim.CreateGraphics())
            using (Brush drawNest = new SolidBrush(Color.Blue))
            {
                posX = list_nest.ElementAt(nestID).X;
                posY = list_nest.ElementAt(nestID).Y;
                worldGraphics.FillEllipse(drawNest, posX, posY, 20, 20); // draw nest + set size
            }
        }

        private void Tick_Counter_tick(object sender, EventArgs e)
        { 
            // runs the simulation
            //
            // code below is temporary pathfinding, in further versions, this code will become ACO
            // code below just tries to show what the user can expect from final version
            //
            //
            tick++; // acts as counter
            txt_Moves.Text = tick.ToString();
            for (int i = 0; i < list_ants.Count; i++)
            {
                list_ants.ElementAt(i).isCloseToNest = false;
                list_ants.ElementAt(i).isCloseToFood = false;
                for (int j = 0; j < list_food.Count; j++)
                {
                    if (list_food.ElementAt(j) != null)
                    {
                        if (list_ants.ElementAt(i).X <= (list_food.ElementAt(j).X + 20) && list_ants.ElementAt(i).X >= (list_food.ElementAt(j).X - 5))
                        // if ant is horizontally close to food
                        {
                            if (list_ants.ElementAt(i).Y <= (list_food.ElementAt(j).Y + 20) && list_ants.ElementAt(i).Y >= (list_food.ElementAt(j).Y - 5))
                            // if ant is vertically close to food
                            {
                                list_ants.ElementAt(i).isCloseToFood = true;
                                list_ants.ElementAt(i).foodClosest = j;
                            }
                        }
                    }
                }
                for (int k = 0; k < list_nest.Count; k++)
                {
                    if (list_ants.ElementAt(i).X <= (list_nest.ElementAt(k).X + 20) && list_ants.ElementAt(i).X >= (list_nest.ElementAt(k).X - 5))
                    // if ant is horizontally close to nest
                    {
                        if (list_ants.ElementAt(i).Y <= (list_nest.ElementAt(k).Y + 20) && list_ants.ElementAt(i).Y >= (list_nest.ElementAt(k).Y - 5))
                        // if ant is vertically close to nest
                        {
                            list_ants.ElementAt(i).isCloseToNest = true;
                            list_ants.ElementAt(i).nestClosest = k;
                            list_ants.ElementAt(i).nestLocationKnown = true;
                        }
                    }
                }
                list_ants.ElementAt(i).isCloseToAnt = false;
                for (int l = 0; l < list_ants.Count; l++)
                {
                    if (list_ants.ElementAt(i).X <= (list_ants.ElementAt(l).X + 10) && list_ants.ElementAt(i).X >= (list_ants.ElementAt(l).X - 5))
                    //if ant is horizontally close to another ant
                    {
                        if (list_ants.ElementAt(i).Y <= (list_ants.ElementAt(l).Y + 10) && list_ants.ElementAt(i).Y >= (list_ants.ElementAt(l).Y - 5))
                        //if ant is vertically close to another ant
                        {
                            list_ants.ElementAt(i).isCloseToAnt = true;
                            list_ants.ElementAt(i).closestAnt = l;
                            break;
                        }
                    }
                }
                if (list_ants.ElementAt(i).isCarrying == true)
                {
                    if (list_ants.ElementAt(i).isCloseToNest == true)
                    { 
                        DepositFood(i);
                    }
                    else
                    {
                        if (list_ants.ElementAt(i).nestLocationKnown == true)
                        {
                            MoveToNest(i); // ACO impliment here
                        }
                        else
                        {
                            RandomMove(i); // ACO impliment here
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
                        if (list_ants.ElementAt(i).foodLocationKnown == true)
                        {
                            MoveToFood(i); // ACO impliment here
                        }
                        else
                        {
                            RandomMove(i); // ACO impliment here
                        }
                    }
                }
                if (list_ants.ElementAt(i).isCloseToAnt == true)
                {
                    AskForLocations(i); // pheromones will go here
                }
            }
        }

        private void RandomMove(int antID)
        { // due to randomness of movement, movement distance is doubled to increase range of wandering ants (prevent small circling)
            int randomNum;
            int tempX, tempY;
            tempX = list_ants.ElementAt(antID).X;
            tempY = list_ants.ElementAt(antID).Y;
            randomNum = randomAntObj.Next(1, 9); // 1 to 8 clockwise, 1 = northwest
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
            DrawAnts(antID, tempX, tempY);
            if (list_ants.ElementAt(antID).isCloseToNest == true)
            {
                DrawNest(list_ants.ElementAt(antID).nestClosest); // sets location of nest that's close
            }
            if (list_ants.ElementAt(antID).isCloseToFood == true)
            {
                DrawFood(list_ants.ElementAt(antID).foodClosest); // sets location of food that's close
            }
        }

        private void PickUpFood(int antID)
        { 
            // pick up nearby food and call EraseFood if food pile is empty
            int foodClosest;
            foodVolume = Convert.ToInt32(txt_FoodLeft.Text); 
            foodVolume = foodVolume - 1;
            txt_FoodLeft.Text = foodVolume.ToString();

            foodClosest = list_ants.ElementAt(antID).foodClosest;
            list_food.ElementAt(foodClosest).Quantity--;
            list_ants.ElementAt(antID).isCarrying = true; // changes colour of ant
            list_ants.ElementAt(antID).foodLocationKnown = true; //  <-- temporary whilst ACO missing
            Console.WriteLine("food picked up");
            if (list_food.ElementAt(foodClosest).Quantity == 0) // reduce food sources left value and clear food from panel
            {
                foodSources--;
                txt_FoodSourcesLeft.Text = foodSources.ToString(); // updates stat box with correct value
                EraseFood(foodClosest);
            }
        }

        private void DepositFood(int antID)
        {
            // deposits food an adds to stockpile
            list_ants.ElementAt(antID).isCarrying = false; // isCarrying set to false so colour of ant goes back to default
            list_ants.ElementAt(antID).nestLocationKnown = true; //  <-- temporary whilst ACO missing
            stockpile = stockpile + 1;
            txt_NestFood.Text = stockpile.ToString();
            Console.WriteLine("deposited");
        }

        private void MoveToNest(int antID)
        { // movement here is by single co-ordinates to prevent overshooting 
            int tempX, tempY;
            tempX = list_ants.ElementAt(antID).X;
            tempY = list_ants.ElementAt(antID).Y;

            if (list_ants.ElementAt(antID).X < list_ants.ElementAt(list_ants.ElementAt(antID).nestClosest).X)
            {
                list_ants.ElementAt(antID).X++; // horizontal move
            }
            else
            {
                list_ants.ElementAt(antID).X--; // horizontal move
            }
            if (list_ants.ElementAt(antID).Y < list_ants.ElementAt(list_ants.ElementAt(antID).nestClosest).Y)
            {
                list_ants.ElementAt(antID).Y++; // verticle move
            }
            else
            {
                list_ants.ElementAt(antID).Y--; // verticle move
            }

            DrawAnts(antID, tempX, tempY);
            if (list_ants.ElementAt(antID).isCloseToFood == true)
            {
                DrawFood(list_ants.ElementAt(antID).foodClosest);
            }
            if (list_ants.ElementAt(antID).isCloseToNest == true)
            {
                DrawNest(list_ants.ElementAt(antID).nestClosest);
            }
        }

        private void MoveToFood(int antID)
        { // movement here is by single co-ordinates to prevent overshooting 
            int tempX, tempY;
            tempX = list_ants.ElementAt(antID).X;
            tempY = list_ants.ElementAt(antID).Y;

            if (list_ants.ElementAt(antID).X < list_food.ElementAt(list_ants.ElementAt(antID).foodClosest).X)
            {
                list_ants.ElementAt(antID).X++; // horizontal move
            }
            else
            {
                list_ants.ElementAt(antID).X--; // horizontal move
            }

            if (list_ants.ElementAt(antID).Y < list_food.ElementAt(list_ants.ElementAt(antID).foodClosest).Y)
            {
                list_ants.ElementAt(antID).Y++; // verticle move
            }
            else
            {
                list_ants.ElementAt(antID).Y--; // verticle move
            }

            DrawAnts(antID, tempX, tempY);
            if (list_ants.ElementAt(antID).isCloseToFood == true)
            {
                DrawFood(list_ants.ElementAt(antID).foodClosest);
            }
            if (list_ants.ElementAt(antID).isCloseToNest == true)
            {
                DrawNest(list_ants.ElementAt(antID).nestClosest);
            }
        }

        private void AskForLocations(int antID) // temporary until ACO implimented - does not work properly
        {
            int closestAnt;
            closestAnt = list_ants.ElementAt(antID).closestAnt;

            if (list_ants.ElementAt(antID).foodLocationKnown == false && list_ants.ElementAt(closestAnt).foodLocationKnown == true)
            {
                list_ants.ElementAt(antID).foodClosest = list_ants.ElementAt(closestAnt).foodClosest;
                list_ants.ElementAt(antID).foodLocationKnown = true;
                Console.WriteLine("food learned");
            }
            if (list_ants.ElementAt(antID).nestLocationKnown == false && list_ants.ElementAt(closestAnt).nestLocationKnown == true)
            {
                list_ants.ElementAt(antID).nestClosest = list_ants.ElementAt(closestAnt).nestClosest;
                list_ants.ElementAt(antID).nestLocationKnown = true;
                Console.WriteLine("nest learned");
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e) // user interaction
        {
            Point clickPoint;
            MouseButtons buttonPressed;
            clickPoint = e.Location;
            buttonPressed = e.Button;

            if (buttonPressed == MouseButtons.Left) // place food
            {
                Random rndNum = new Random();
                int min = Convert.ToInt32(txt_MinFoodPerSource.Text);
                int max = Convert.ToInt32(txt_MaxFoodPerSource.Text);
                Food tempFood;

                tempFood = new Food(clickPoint.X, clickPoint.Y);
                tempFood.Quantity = rndNum.Next(min, max); // takes user input for min and max value and picks random num between those values
                list_food.Add(tempFood);
                DrawFood(list_food.Count - 1);
                foodSources++;
                foodVolume = foodVolume + tempFood.Quantity;
                txt_FoodSourcesLeft.Text = foodSources.ToString();
                txt_FoodLeft.Text = foodVolume.ToString(); 
            }
            else if (buttonPressed == MouseButtons.Right) // place nest
            {
                Nest tempNest;
                tempNest = new Nest(clickPoint.X, clickPoint.Y);
                list_nest.Add(tempNest);
                DrawNest(list_nest.Count - 1);
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            // no code needed - event purely to allow panel to be painted on
        }

        // all code below is for the other buttons/settings 

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UserEntry_TextEntered(object sender, KeyPressEventArgs e)
        {
            // IsDigit checks if specified character is catagorised as digit
            // char.IsControl returns true/false depending on character being a non-printing character
            // use KeyChar to sample/modify keystrokes at runtime
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            // clears old sim first
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
            // obstacles - leave out for now
            if (txt_NoOfObsticles.Text.Trim() == string.Empty) // may leave out so num of obsticles defaults to 0
            {
                MessageBox.Show("No value entered - Obstacles");
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
                MessageBox.Show("Value is not between 0-100");
            }
            // pheromone decay rate
            if (txt_PheromoneDecayRate.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No value entered - Decay Rate");
                return;
            }
            if (Convert.ToInt32(txt_PheromoneDecayRate.Text) < 0 || Convert.ToInt32(txt_PheromoneDecayRate.Text) > 100)
            {
                MessageBox.Show("Value is not between 0-100");
            }
            // start simulation - for now, clicking start clears sim an starts it again from fresh
            SpawnAnts();
            Tick_Counter.Start();
        }

        private void btn_Stop_Click(object sender, EventArgs e) // stops sim, does not clear it however
        {
            Tick_Counter.Stop();
        }
    }
}
