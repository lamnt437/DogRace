using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DogRaceNew
{
    public partial class Form1 : Form
    {
        //guy
        public Guy joe = new Guy() { Name = "Joe", Cash = 50 };
        public Guy bob = new Guy() { Name = "Bob", Cash = 75 };
        public Guy al = new Guy() { Name = "Al", Cash = 100 };
        public Guy[] guyArray = new Guy[3];
        public Guy guyPointer;

        //dog
        public Greyhound[] dogArr = new Greyhound[4];
        public PictureBox[] dogPict = new PictureBox[4];

        private Random MyRandomizer = new Random();
        private Timer raceTimer = new Timer();
        private int number;
        private int DogWin;

        private int maxLoc = 0;
        private int trigger = 0;

        public Form1()
        {
            InitializeComponent();

            //Initialize guys
            joe.MyLabel = joeBetLabel;
            joe.MyRadioButton = joeRadioButton;

            bob.MyLabel = bobBetLabel;
            bob.MyRadioButton = bobRadioButton;

            al.MyLabel = alBetLabel;
            al.MyRadioButton = alRadioButton;

            guyArray[0] = joe;
            guyArray[1] = bob;
            guyArray[2] = al;

            guyPointer = joe;

            //dog
            dogPict[0] = dog0Pict;
            dogPict[1] = dog1Pict;
            dogPict[2] = dog2Pict;
            dogPict[3] = dog3Pict;

            for (int i = 0; i < 4; i++)
            {
                dogArr[i] = new Greyhound();
                dogArr[i].MyPictureBox = dogPict[i];
                dogArr[i].StartingPosition = 26;
                dogArr[i].RacetrackLength = 596;
                dogArr[i].Randomizer = MyRandomizer;
            }

            //timer
            raceTimer.Interval = 1000;
            raceTimer.Tick += new EventHandler(raceTimer_Tick);
        }

        private void raceTimer_Tick(object Sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                dogArr[i].Run();

                if (dogArr[i].Location >= dogArr[i].RacetrackLength)
                {
                    trigger = 1;
                    if (dogArr[i].Location > maxLoc)
                    {
                        maxLoc = dogArr[i].Location;
                        DogWin = i;
                    }
                }
            }

            if (trigger == 1)
            {
                raceTimer.Stop();
                MessageBox.Show("Dog " + (DogWin + 1).ToString() + " won!");
                Pay();
                bettingGroupBox.Enabled = true;
            }

            number += 1;
            timerLabel.Text = number.ToString();
        }

        private void Pay()
        {
            for (int i = 0; i < 3; i++)
            {
                if (guyArray[i].MyBet != null)
                {
                    guyArray[i].MyBet.Payout(DogWin);
                    guyArray[i].ClearBet();
                }
            }
            UpdateForm();
        }

        private void UpdateForm()
        {
            infoLabel.Text = guyPointer.Name + " is being chosen! Cash: " + guyPointer.Cash;
            nameLabel.Text = guyPointer.Name;
        }

        private void joeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            guyPointer = joe;
            UpdateForm();
        }

        private void bobRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            guyPointer = bob;
            UpdateForm();
        }

        private void alRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            guyPointer = al;
            UpdateForm();
        }

        private void betButton_Click(object sender, EventArgs e)
        {
            guyPointer.PlaceBet((int)betValueSelect.Value, (int)dogNumberSelect.Value - 1);
            UpdateForm();
        }

        private void makeRun_Click(object sender, EventArgs e)
        {
            bettingGroupBox.Enabled = false;
            StartRunning();
        }

        private void StartRunning()
        {
            for (int i = 0; i < 4; i++)
                dogArr[i].TakeStartingPosition();
            trigger = 0;
            maxLoc = 0;
            raceTimer.Start();
            number = 0;
        }
    }
}
