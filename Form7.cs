using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthFitness_System
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            Profile Profile = new Profile();
            Profile.ShowDialog();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            this.Hide();
            About About = new About();
            About.ShowDialog();
        }

        private void BtnWorkout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Workout Workout = new Workout();
            Workout.ShowDialog();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main Main = new Main();
            Main.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
