using MySql.Data.MySqlClient;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {
            // Define the start and end colors of the gradient (shades of blue and orange)
            Color startColor = Color.FromArgb(34, 34, 34);   // macOS-like blue
            Color endColor = Color.FromArgb(99, 99, 99);     // macOS-like orange

            // Create a linear gradient brush
            LinearGradientBrush brush = new LinearGradientBrush(LoginPanel.ClientRectangle, startColor, endColor, LinearGradientMode.Vertical);

            // Fill the panel's background with the gradient brush
            e.Graphics.FillRectangle(brush, LoginPanel.ClientRectangle);
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Define the semi-transparent gray color
            Color semiTransparentColor = Color.FromArgb(100, 128, 128, 128); // Adjust alpha (transparency) value as needed

            // Fill the panel's background with the semi-transparent gray color
            using (SolidBrush semiTransparentBrush = new SolidBrush(semiTransparentColor))
            {
                e.Graphics.FillRectangle(semiTransparentBrush, guna2CustomGradientPanel1.ClientRectangle);
            }

            // Set the background color of the panel to transparent
            guna2CustomGradientPanel1.BackColor = Color.Transparent;

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }

        private void LoginLabel_Click(object sender, EventArgs e)
        {
          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the Register form
            this.Hide();
            Register Register = new Register();
            Register.ShowDialog();
        }

        private void ForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the Register form
            this.Hide();
            Recover Recover = new Recover();
            Recover.ShowDialog();
        }

        private void BtnCirLogin_Click(object sender, EventArgs e)
        {
            // Show the Register form
            this.Hide();
            Login Login = new Login();
            Login.ShowDialog();
        }

        private void BtnCirRegister_Click(object sender, EventArgs e)
        {
            // Show the Register form
            this.Hide();
            Recover Recover = new Recover();
            Recover.ShowDialog();
        }

        private void BtnCirRecover_Click(object sender, EventArgs e)
        {
            // Show the Register form
            this.Hide();
            Recover Recover = new Recover();
            Recover.ShowDialog();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();// Get the username and passwor d entered by the user
            string Username = TxtBoxUsername.Text;
            string Password = TxtBoxPassword.Text;

            // Create an instance of the MySQLConnectionManager to manage the database connection
            MySQLConnectionManager connectionManager = new MySQLConnectionManager();

            // Open the database connection
            if (connectionManager.OpenConnection())
            {
                // Construct the SQL query to check if the user exists with the given credentials
                string query = $"SELECT COUNT(*) FROM usercredentials WHERE Username='{Username}' AND Password='{Password}' AND status='active'";

                // Execute the query
                MySqlCommand command = new MySqlCommand(query, connectionManager.GetConnection());
                int userCount = Convert.ToInt32(command.ExecuteScalar());

                // Close the database connection
                connectionManager.CloseConnection();

                // Check if a user with the provided credentials exists
                if (userCount > 0)
                {
                    // Authentication successful, open the main form
                    Main mainForm = new Main();
                    mainForm.ShowDialog();
                }
                else
                {
                    // Authentication failed, display an error message
                    MessageBox.Show("Invalid username or password or account is inactive. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Failed to establish a connection with the database, display an error message
                MessageBox.Show("Failed to connect to the database. Please try again later.", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
