using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthFitness_System
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Define the start and end colors
            Color startColor = Color.FromArgb(0, 122, 255); // macOS-like blue
            Color endColor = Color.FromArgb(255, 149, 0);   // macOS-like orange

            // Create a linear gradient brush
            LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, // Rectangle to fill
                startColor, // Starting color
                endColor,   // Ending color
                LinearGradientMode.Vertical); // Gradient mode (vertical)

            // Assign the brush as the background of the form
            this.BackgroundImage = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(this.BackgroundImage))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void BtnWorkout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Workout Workout = new Workout();
            Workout.ShowDialog();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            this.Hide();
            About About = new About();
            About.ShowDialog();
        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            Profile Profile = new Profile();
            Profile.ShowDialog();
        }

        private void BtnBMI_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
    }
}
