/*
AccurateAge
VERSION: 1.0

Inputs: Date of Birth

Outputs: Age with Years, Month, Days, Hours, Days, Seconds

Description: This software tool allows to calculate your accurate age.

Developer: Nicolas CHEN
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AccurateAge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*** MENU BARS ***/
        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] instructions = new string[]
            {
                "AccurateAge is a software tool that calculates your age with years, month, days, hours, minutes and seconds."
                , ""
                , "1) Indicate your date of birth."
                , ""
                , "2) Click on the button 'Get Your Accurate Age'."
                , ""
                , "3) Your Accurate Age will be displayed."                
            };

            MessageBoxMultiLines(instructions);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] about = new string[]
            {
                "AccurateAge"
                , ""
                , "VERSION: 1.0"
                , ""
                , "Developed by Nicolas Chen"
            };

            MessageBoxMultiLines(about);
        }


        /*** METHODS ***/
        public void Reset()
        {
            yearsLabel.Text = "__";
            monthsLabel.Text = "__";
            daysLabel.Text = "__";
            hoursLabel.Text = "__";
            minutesLabel.Text = "__";
            secondsLabel.Text = "__";
        }

        static List<int> CalculateYourAge(DateTime dateOfBirth)
        {            
           DateTime Now = DateTime.Now;
           int Years = new DateTime(DateTime.Now.Subtract(dateOfBirth).Ticks).Year - 1;

            DateTime PastYearDate = dateOfBirth.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;   

            List<int> accurateAge = new List<int>(new int[] { Years, Months, Days, Hours, Minutes, Seconds });

            return accurateAge;
        }

        public void MessageBoxMultiLines(IEnumerable<string> lines)
        {
            var instructionLine = new StringBuilder();
            bool firstLine = false;
            foreach (string line in lines)
            {
                if (firstLine)
                    instructionLine.Append(Environment.NewLine);

                instructionLine.Append(line);
                firstLine = true;
            }
            MessageBox.Show(instructionLine.ToString(), "Information");
        }


        /*** BUTTON ***/
        private void button1_Click(object sender, EventArgs e)
        {            
            DateTime dateOfBirth = Convert.ToDateTime(dateTimePicker1.Text);
            List<int> dateOfBirthValues = CalculateYourAge(dateOfBirth);                    

            int year = dateOfBirthValues[0];
            int month = dateOfBirthValues[1];
            int day = dateOfBirthValues[2];
            int hours = dateOfBirthValues[3];
            int minutes = dateOfBirthValues[4];
            int secondes = dateOfBirthValues[5];

            yearsLabel.Text = year.ToString();
            monthsLabel.Text = month.ToString();
            daysLabel.Text = day.ToString();
            hoursLabel.Text = hours.ToString();
            minutesLabel.Text = minutes.ToString();
            secondsLabel.Text = secondes.ToString();

            dateOfBirthValues.Clear();
        }
    }
}
