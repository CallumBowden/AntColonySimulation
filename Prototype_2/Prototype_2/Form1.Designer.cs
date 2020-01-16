namespace Prototype_2
{
    partial class PrototypeACsim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gb_Options = new System.Windows.Forms.GroupBox();
            this.lbl_SmartAnt = new System.Windows.Forms.Label();
            this.txt_SmartAnts = new System.Windows.Forms.TextBox();
            this.lbl_DispersionRate = new System.Windows.Forms.Label();
            this.txt_PheromoneDecayRate = new System.Windows.Forms.TextBox();
            this.txt_followProb = new System.Windows.Forms.TextBox();
            this.txt_PheromoneStrength = new System.Windows.Forms.TextBox();
            this.lbl_followProb = new System.Windows.Forms.Label();
            this.lbl_PheromoneStrength = new System.Windows.Forms.Label();
            this.txt_MaxFoodPerSource = new System.Windows.Forms.TextBox();
            this.lbl_MaxFoodPerSource = new System.Windows.Forms.Label();
            this.lbl_MinFoodPerSource = new System.Windows.Forms.Label();
            this.txt_MinFoodPerSource = new System.Windows.Forms.TextBox();
            this.lbl_StartNoOfAnts = new System.Windows.Forms.Label();
            this.txt_StartingNoOfAnts = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.gb_Stats = new System.Windows.Forms.GroupBox();
            this.txt_Moves = new System.Windows.Forms.TextBox();
            this.txt_FoodSourcesLeft = new System.Windows.Forms.TextBox();
            this.txt_NestFood = new System.Windows.Forms.TextBox();
            this.txt_FoodLeft = new System.Windows.Forms.TextBox();
            this.txt_NumOfAnt = new System.Windows.Forms.TextBox();
            this.lbl_Moves = new System.Windows.Forms.Label();
            this.lbl_FoodSourcesLeft = new System.Windows.Forms.Label();
            this.lbl_FoodAtNest = new System.Windows.Forms.Label();
            this.lbl_TotalFoodLeft = new System.Windows.Forms.Label();
            this.lbl_NumOfAnts = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.Tick_Counter = new System.Windows.Forms.Timer(this.components);
            this.gb_userInteraction = new System.Windows.Forms.GroupBox();
            this.lbl_LeftClick = new System.Windows.Forms.Label();
            this.lbl_RightClick = new System.Windows.Forms.Label();
            this.Panel_sim = new System.Windows.Forms.Panel();
            this.lbl_notes = new System.Windows.Forms.Label();
            this.lbl_note1 = new System.Windows.Forms.Label();
            this.lbl_SmartAntInfo = new System.Windows.Forms.Label();
            this.lbl_smartAntInfo2 = new System.Windows.Forms.Label();
            this.gb_Options.SuspendLayout();
            this.gb_Stats.SuspendLayout();
            this.gb_userInteraction.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Options
            // 
            this.gb_Options.BackColor = System.Drawing.Color.LightGray;
            this.gb_Options.Controls.Add(this.lbl_SmartAnt);
            this.gb_Options.Controls.Add(this.txt_SmartAnts);
            this.gb_Options.Controls.Add(this.lbl_DispersionRate);
            this.gb_Options.Controls.Add(this.txt_PheromoneDecayRate);
            this.gb_Options.Controls.Add(this.txt_followProb);
            this.gb_Options.Controls.Add(this.txt_PheromoneStrength);
            this.gb_Options.Controls.Add(this.lbl_followProb);
            this.gb_Options.Controls.Add(this.lbl_PheromoneStrength);
            this.gb_Options.Controls.Add(this.txt_MaxFoodPerSource);
            this.gb_Options.Controls.Add(this.lbl_MaxFoodPerSource);
            this.gb_Options.Controls.Add(this.lbl_MinFoodPerSource);
            this.gb_Options.Controls.Add(this.txt_MinFoodPerSource);
            this.gb_Options.Controls.Add(this.lbl_StartNoOfAnts);
            this.gb_Options.Controls.Add(this.txt_StartingNoOfAnts);
            this.gb_Options.Location = new System.Drawing.Point(12, 12);
            this.gb_Options.Name = "gb_Options";
            this.gb_Options.Size = new System.Drawing.Size(982, 100);
            this.gb_Options.TabIndex = 0;
            this.gb_Options.TabStop = false;
            this.gb_Options.Text = "Options";
            // 
            // lbl_SmartAnt
            // 
            this.lbl_SmartAnt.AutoSize = true;
            this.lbl_SmartAnt.Location = new System.Drawing.Point(313, 22);
            this.lbl_SmartAnt.Name = "lbl_SmartAnt";
            this.lbl_SmartAnt.Size = new System.Drawing.Size(61, 13);
            this.lbl_SmartAnt.TabIndex = 14;
            this.lbl_SmartAnt.Text = "Smart Ants:";
            // 
            // txt_SmartAnts
            // 
            this.txt_SmartAnts.Location = new System.Drawing.Point(485, 20);
            this.txt_SmartAnts.Name = "txt_SmartAnts";
            this.txt_SmartAnts.Size = new System.Drawing.Size(139, 20);
            this.txt_SmartAnts.TabIndex = 13;
            // 
            // lbl_DispersionRate
            // 
            this.lbl_DispersionRate.AutoSize = true;
            this.lbl_DispersionRate.Location = new System.Drawing.Point(313, 79);
            this.lbl_DispersionRate.Name = "lbl_DispersionRate";
            this.lbl_DispersionRate.Size = new System.Drawing.Size(129, 13);
            this.lbl_DispersionRate.TabIndex = 12;
            this.lbl_DispersionRate.Text = "Dispersion Rate (0-100%):";
            // 
            // txt_PheromoneDecayRate
            // 
            this.txt_PheromoneDecayRate.Location = new System.Drawing.Point(485, 72);
            this.txt_PheromoneDecayRate.Name = "txt_PheromoneDecayRate";
            this.txt_PheromoneDecayRate.Size = new System.Drawing.Size(139, 20);
            this.txt_PheromoneDecayRate.TabIndex = 11;
            this.txt_PheromoneDecayRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserEntry_TextEntered);
            // 
            // txt_followProb
            // 
            this.txt_followProb.Location = new System.Drawing.Point(795, 20);
            this.txt_followProb.Name = "txt_followProb";
            this.txt_followProb.Size = new System.Drawing.Size(139, 20);
            this.txt_followProb.TabIndex = 11;
            this.txt_followProb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserEntry_TextEntered);
            // 
            // txt_PheromoneStrength
            // 
            this.txt_PheromoneStrength.Location = new System.Drawing.Point(485, 46);
            this.txt_PheromoneStrength.Name = "txt_PheromoneStrength";
            this.txt_PheromoneStrength.Size = new System.Drawing.Size(139, 20);
            this.txt_PheromoneStrength.TabIndex = 10;
            this.txt_PheromoneStrength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserEntry_TextEntered);
            // 
            // lbl_followProb
            // 
            this.lbl_followProb.AutoSize = true;
            this.lbl_followProb.Location = new System.Drawing.Point(644, 23);
            this.lbl_followProb.Name = "lbl_followProb";
            this.lbl_followProb.Size = new System.Drawing.Size(135, 13);
            this.lbl_followProb.TabIndex = 9;
            this.lbl_followProb.Text = "Follow Probability (0-100%):";
            // 
            // lbl_PheromoneStrength
            // 
            this.lbl_PheromoneStrength.AutoSize = true;
            this.lbl_PheromoneStrength.Location = new System.Drawing.Point(313, 49);
            this.lbl_PheromoneStrength.Name = "lbl_PheromoneStrength";
            this.lbl_PheromoneStrength.Size = new System.Drawing.Size(151, 13);
            this.lbl_PheromoneStrength.TabIndex = 8;
            this.lbl_PheromoneStrength.Text = "Pheromone Strength (0-100%):";
            // 
            // txt_MaxFoodPerSource
            // 
            this.txt_MaxFoodPerSource.Location = new System.Drawing.Point(156, 72);
            this.txt_MaxFoodPerSource.Name = "txt_MaxFoodPerSource";
            this.txt_MaxFoodPerSource.Size = new System.Drawing.Size(139, 20);
            this.txt_MaxFoodPerSource.TabIndex = 7;
            this.txt_MaxFoodPerSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserEntry_TextEntered);
            // 
            // lbl_MaxFoodPerSource
            // 
            this.lbl_MaxFoodPerSource.AutoSize = true;
            this.lbl_MaxFoodPerSource.Location = new System.Drawing.Point(6, 75);
            this.lbl_MaxFoodPerSource.Name = "lbl_MaxFoodPerSource";
            this.lbl_MaxFoodPerSource.Size = new System.Drawing.Size(140, 13);
            this.lbl_MaxFoodPerSource.TabIndex = 7;
            this.lbl_MaxFoodPerSource.Text = "Max Food Per Food Source:";
            // 
            // lbl_MinFoodPerSource
            // 
            this.lbl_MinFoodPerSource.AutoSize = true;
            this.lbl_MinFoodPerSource.Location = new System.Drawing.Point(6, 49);
            this.lbl_MinFoodPerSource.Name = "lbl_MinFoodPerSource";
            this.lbl_MinFoodPerSource.Size = new System.Drawing.Size(137, 13);
            this.lbl_MinFoodPerSource.TabIndex = 6;
            this.lbl_MinFoodPerSource.Text = "Min Food Per Food Source:";
            // 
            // txt_MinFoodPerSource
            // 
            this.txt_MinFoodPerSource.Location = new System.Drawing.Point(156, 46);
            this.txt_MinFoodPerSource.Name = "txt_MinFoodPerSource";
            this.txt_MinFoodPerSource.Size = new System.Drawing.Size(139, 20);
            this.txt_MinFoodPerSource.TabIndex = 6;
            this.txt_MinFoodPerSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserEntry_TextEntered);
            // 
            // lbl_StartNoOfAnts
            // 
            this.lbl_StartNoOfAnts.AutoSize = true;
            this.lbl_StartNoOfAnts.Location = new System.Drawing.Point(6, 23);
            this.lbl_StartNoOfAnts.Name = "lbl_StartNoOfAnts";
            this.lbl_StartNoOfAnts.Size = new System.Drawing.Size(124, 13);
            this.lbl_StartNoOfAnts.TabIndex = 4;
            this.lbl_StartNoOfAnts.Text = "Starting Number Of Ants:";
            // 
            // txt_StartingNoOfAnts
            // 
            this.txt_StartingNoOfAnts.Location = new System.Drawing.Point(156, 20);
            this.txt_StartingNoOfAnts.Name = "txt_StartingNoOfAnts";
            this.txt_StartingNoOfAnts.Size = new System.Drawing.Size(139, 20);
            this.txt_StartingNoOfAnts.TabIndex = 0;
            this.txt_StartingNoOfAnts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserEntry_TextEntered);
            // 
            // btn_Start
            // 
            this.btn_Start.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_Start.Location = new System.Drawing.Point(781, 426);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(94, 51);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = false;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackColor = System.Drawing.Color.Red;
            this.btn_Stop.Location = new System.Drawing.Point(781, 483);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(96, 51);
            this.btn_Stop.TabIndex = 1;
            this.btn_Stop.Text = "Stop/Reset";
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // gb_Stats
            // 
            this.gb_Stats.BackColor = System.Drawing.Color.LightGray;
            this.gb_Stats.Controls.Add(this.txt_Moves);
            this.gb_Stats.Controls.Add(this.txt_FoodSourcesLeft);
            this.gb_Stats.Controls.Add(this.txt_NestFood);
            this.gb_Stats.Controls.Add(this.txt_FoodLeft);
            this.gb_Stats.Controls.Add(this.txt_NumOfAnt);
            this.gb_Stats.Controls.Add(this.lbl_Moves);
            this.gb_Stats.Controls.Add(this.lbl_FoodSourcesLeft);
            this.gb_Stats.Controls.Add(this.lbl_FoodAtNest);
            this.gb_Stats.Controls.Add(this.lbl_TotalFoodLeft);
            this.gb_Stats.Controls.Add(this.lbl_NumOfAnts);
            this.gb_Stats.Location = new System.Drawing.Point(781, 118);
            this.gb_Stats.Name = "gb_Stats";
            this.gb_Stats.Size = new System.Drawing.Size(213, 149);
            this.gb_Stats.TabIndex = 1;
            this.gb_Stats.TabStop = false;
            this.gb_Stats.Text = "Stats";
            // 
            // txt_Moves
            // 
            this.txt_Moves.Location = new System.Drawing.Point(106, 120);
            this.txt_Moves.Name = "txt_Moves";
            this.txt_Moves.ReadOnly = true;
            this.txt_Moves.Size = new System.Drawing.Size(96, 20);
            this.txt_Moves.TabIndex = 6;
            // 
            // txt_FoodSourcesLeft
            // 
            this.txt_FoodSourcesLeft.Location = new System.Drawing.Point(106, 94);
            this.txt_FoodSourcesLeft.Name = "txt_FoodSourcesLeft";
            this.txt_FoodSourcesLeft.ReadOnly = true;
            this.txt_FoodSourcesLeft.Size = new System.Drawing.Size(96, 20);
            this.txt_FoodSourcesLeft.TabIndex = 6;
            // 
            // txt_NestFood
            // 
            this.txt_NestFood.Location = new System.Drawing.Point(106, 68);
            this.txt_NestFood.Name = "txt_NestFood";
            this.txt_NestFood.ReadOnly = true;
            this.txt_NestFood.Size = new System.Drawing.Size(96, 20);
            this.txt_NestFood.TabIndex = 6;
            // 
            // txt_FoodLeft
            // 
            this.txt_FoodLeft.Location = new System.Drawing.Point(106, 42);
            this.txt_FoodLeft.Name = "txt_FoodLeft";
            this.txt_FoodLeft.ReadOnly = true;
            this.txt_FoodLeft.Size = new System.Drawing.Size(96, 20);
            this.txt_FoodLeft.TabIndex = 6;
            // 
            // txt_NumOfAnt
            // 
            this.txt_NumOfAnt.Location = new System.Drawing.Point(106, 16);
            this.txt_NumOfAnt.Name = "txt_NumOfAnt";
            this.txt_NumOfAnt.ReadOnly = true;
            this.txt_NumOfAnt.Size = new System.Drawing.Size(96, 20);
            this.txt_NumOfAnt.TabIndex = 5;
            // 
            // lbl_Moves
            // 
            this.lbl_Moves.AutoSize = true;
            this.lbl_Moves.Location = new System.Drawing.Point(6, 123);
            this.lbl_Moves.Name = "lbl_Moves";
            this.lbl_Moves.Size = new System.Drawing.Size(42, 13);
            this.lbl_Moves.TabIndex = 4;
            this.lbl_Moves.Text = "Moves:";
            // 
            // lbl_FoodSourcesLeft
            // 
            this.lbl_FoodSourcesLeft.AutoSize = true;
            this.lbl_FoodSourcesLeft.Location = new System.Drawing.Point(6, 97);
            this.lbl_FoodSourcesLeft.Name = "lbl_FoodSourcesLeft";
            this.lbl_FoodSourcesLeft.Size = new System.Drawing.Size(97, 13);
            this.lbl_FoodSourcesLeft.TabIndex = 3;
            this.lbl_FoodSourcesLeft.Text = "Food Sources Left:";
            // 
            // lbl_FoodAtNest
            // 
            this.lbl_FoodAtNest.AutoSize = true;
            this.lbl_FoodAtNest.Location = new System.Drawing.Point(6, 71);
            this.lbl_FoodAtNest.Name = "lbl_FoodAtNest";
            this.lbl_FoodAtNest.Size = new System.Drawing.Size(72, 13);
            this.lbl_FoodAtNest.TabIndex = 2;
            this.lbl_FoodAtNest.Text = "Food At Nest:";
            // 
            // lbl_TotalFoodLeft
            // 
            this.lbl_TotalFoodLeft.AutoSize = true;
            this.lbl_TotalFoodLeft.Location = new System.Drawing.Point(6, 45);
            this.lbl_TotalFoodLeft.Name = "lbl_TotalFoodLeft";
            this.lbl_TotalFoodLeft.Size = new System.Drawing.Size(82, 13);
            this.lbl_TotalFoodLeft.TabIndex = 1;
            this.lbl_TotalFoodLeft.Text = "Total Food Left:";
            // 
            // lbl_NumOfAnts
            // 
            this.lbl_NumOfAnts.AutoSize = true;
            this.lbl_NumOfAnts.Location = new System.Drawing.Point(6, 19);
            this.lbl_NumOfAnts.Name = "lbl_NumOfAnts";
            this.lbl_NumOfAnts.Size = new System.Drawing.Size(70, 13);
            this.lbl_NumOfAnts.TabIndex = 0;
            this.lbl_NumOfAnts.Text = "Num Of Ants:";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_Close.Location = new System.Drawing.Point(883, 483);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(94, 51);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Tick_Counter
            // 
            this.Tick_Counter.Enabled = true;
            this.Tick_Counter.Interval = 4;
            this.Tick_Counter.Tick += new System.EventHandler(this.Tick_Counter_tick);
            // 
            // gb_userInteraction
            // 
            this.gb_userInteraction.Controls.Add(this.lbl_LeftClick);
            this.gb_userInteraction.Controls.Add(this.lbl_RightClick);
            this.gb_userInteraction.Location = new System.Drawing.Point(781, 273);
            this.gb_userInteraction.Name = "gb_userInteraction";
            this.gb_userInteraction.Size = new System.Drawing.Size(213, 66);
            this.gb_userInteraction.TabIndex = 6;
            this.gb_userInteraction.TabStop = false;
            this.gb_userInteraction.Text = "User Interactions";
            // 
            // lbl_LeftClick
            // 
            this.lbl_LeftClick.AutoSize = true;
            this.lbl_LeftClick.Location = new System.Drawing.Point(6, 41);
            this.lbl_LeftClick.Name = "lbl_LeftClick";
            this.lbl_LeftClick.Size = new System.Drawing.Size(116, 13);
            this.lbl_LeftClick.TabIndex = 1;
            this.lbl_LeftClick.Text = "Left Click = Food (Red)";
            // 
            // lbl_RightClick
            // 
            this.lbl_RightClick.AutoSize = true;
            this.lbl_RightClick.Location = new System.Drawing.Point(6, 16);
            this.lbl_RightClick.Name = "lbl_RightClick";
            this.lbl_RightClick.Size = new System.Drawing.Size(122, 13);
            this.lbl_RightClick.TabIndex = 0;
            this.lbl_RightClick.Text = "Right Click = Nest (Blue)";
            // 
            // Panel_sim
            // 
            this.Panel_sim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_sim.Location = new System.Drawing.Point(12, 118);
            this.Panel_sim.Name = "Panel_sim";
            this.Panel_sim.Size = new System.Drawing.Size(763, 416);
            this.Panel_sim.TabIndex = 7;
            this.Panel_sim.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Paint);
            this.Panel_sim.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            // 
            // lbl_notes
            // 
            this.lbl_notes.AutoSize = true;
            this.lbl_notes.Location = new System.Drawing.Point(778, 342);
            this.lbl_notes.Name = "lbl_notes";
            this.lbl_notes.Size = new System.Drawing.Size(38, 13);
            this.lbl_notes.TabIndex = 8;
            this.lbl_notes.Text = "Notes;";
            // 
            // lbl_note1
            // 
            this.lbl_note1.AutoSize = true;
            this.lbl_note1.Location = new System.Drawing.Point(778, 359);
            this.lbl_note1.Name = "lbl_note1";
            this.lbl_note1.Size = new System.Drawing.Size(208, 13);
            this.lbl_note1.TabIndex = 9;
            this.lbl_note1.Text = "P Strength contributes to Follow Probability";
            // 
            // lbl_SmartAntInfo
            // 
            this.lbl_SmartAntInfo.AutoSize = true;
            this.lbl_SmartAntInfo.Location = new System.Drawing.Point(778, 372);
            this.lbl_SmartAntInfo.Name = "lbl_SmartAntInfo";
            this.lbl_SmartAntInfo.Size = new System.Drawing.Size(152, 13);
            this.lbl_SmartAntInfo.TabIndex = 10;
            this.lbl_SmartAntInfo.Text = "Smart Ants know Nest location";
            // 
            // lbl_smartAntInfo2
            // 
            this.lbl_smartAntInfo2.AutoSize = true;
            this.lbl_smartAntInfo2.Location = new System.Drawing.Point(778, 385);
            this.lbl_smartAntInfo2.Name = "lbl_smartAntInfo2";
            this.lbl_smartAntInfo2.Size = new System.Drawing.Size(224, 13);
            this.lbl_smartAntInfo2.TabIndex = 11;
            this.lbl_smartAntInfo2.Text = "SmartAnt num must be lower than num of Ants";
            // 
            // PrototypeACsim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 539);
            this.Controls.Add(this.lbl_smartAntInfo2);
            this.Controls.Add(this.lbl_SmartAntInfo);
            this.Controls.Add(this.lbl_note1);
            this.Controls.Add(this.lbl_notes);
            this.Controls.Add(this.Panel_sim);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.gb_userInteraction);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.gb_Stats);
            this.Controls.Add(this.gb_Options);
            this.Name = "PrototypeACsim";
            this.Text = "Ant Colony Simulation";
            this.gb_Options.ResumeLayout(false);
            this.gb_Options.PerformLayout();
            this.gb_Stats.ResumeLayout(false);
            this.gb_Stats.PerformLayout();
            this.gb_userInteraction.ResumeLayout(false);
            this.gb_userInteraction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Options;
        private System.Windows.Forms.GroupBox gb_Stats;
        private System.Windows.Forms.TextBox txt_Moves;
        private System.Windows.Forms.TextBox txt_FoodSourcesLeft;
        private System.Windows.Forms.TextBox txt_NestFood;
        private System.Windows.Forms.TextBox txt_FoodLeft;
        private System.Windows.Forms.TextBox txt_NumOfAnt;
        private System.Windows.Forms.Label lbl_Moves;
        private System.Windows.Forms.Label lbl_FoodSourcesLeft;
        private System.Windows.Forms.Label lbl_FoodAtNest;
        private System.Windows.Forms.Label lbl_TotalFoodLeft;
        private System.Windows.Forms.Label lbl_NumOfAnts;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label lbl_StartNoOfAnts;
        private System.Windows.Forms.TextBox txt_StartingNoOfAnts;
        private System.Windows.Forms.Label lbl_DispersionRate;
        private System.Windows.Forms.TextBox txt_PheromoneDecayRate;
        private System.Windows.Forms.TextBox txt_followProb;
        private System.Windows.Forms.TextBox txt_PheromoneStrength;
        private System.Windows.Forms.Label lbl_followProb;
        private System.Windows.Forms.Label lbl_PheromoneStrength;
        private System.Windows.Forms.TextBox txt_MaxFoodPerSource;
        private System.Windows.Forms.Label lbl_MaxFoodPerSource;
        private System.Windows.Forms.Label lbl_MinFoodPerSource;
        private System.Windows.Forms.TextBox txt_MinFoodPerSource;
        private System.Windows.Forms.Timer Tick_Counter;
        private System.Windows.Forms.GroupBox gb_userInteraction;
        private System.Windows.Forms.Label lbl_LeftClick;
        private System.Windows.Forms.Label lbl_RightClick;
        private System.Windows.Forms.Panel Panel_sim;
        private System.Windows.Forms.Label lbl_notes;
        private System.Windows.Forms.Label lbl_note1;
        private System.Windows.Forms.Label lbl_SmartAnt;
        private System.Windows.Forms.TextBox txt_SmartAnts;
        private System.Windows.Forms.Label lbl_SmartAntInfo;
        private System.Windows.Forms.Label lbl_smartAntInfo2;
    }
}

