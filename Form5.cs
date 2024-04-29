using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HealthFitness_System
{
    public partial class Workout : Form
    {
       // private string connectionString = "Server=localhost;Database=healthfitness;Uid=root;Pwd=tintin;";
        public Workout()
        {
            InitializeComponent();

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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void WorkoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Workouts Workouts = new Workouts();
            Workouts.ShowDialog();
        }

        private void SleepBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sleep Sleep = new Sleep();
            Sleep.ShowDialog();
        }

        private void MealBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Meals Meals = new Meals();
            Meals.ShowDialog();
        }

        private void GoalBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Goals Goals = new Goals();
            Goals.ShowDialog();
        }

        private void Workout_Load(object sender, EventArgs e)
        {

        }
    }
}

