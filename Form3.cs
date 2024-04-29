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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
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

        private void RegisterPanel_Paint(object sender, PaintEventArgs e)
        {
            // Define the start and end colors of the gradient (shades of blue and orange)
            Color startColor = Color.FromArgb(34, 34, 34);   
            Color endColor = Color.FromArgb(99, 99, 99);

            // Create a linear gradient brush
            LinearGradientBrush brush = new LinearGradientBrush(RegisterPanel.ClientRectangle, startColor, endColor, LinearGradientMode.Vertical);

            // Fill the panel's background with the gradient brush
            e.Graphics.FillRectangle(brush, RegisterPanel.ClientRectangle);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void BtnCirLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Show the Register form
            Login Login = new Login();
            Login.ShowDialog();
        }

        private void BtnCirRecover_Click(object sender, EventArgs e)
        {
            // Show the Register form
            this.Hide();
            Recover Recover = new Recover();
            Recover.ShowDialog();
        }

        private void BtnCirRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register Register = new Register();
            Register.ShowDialog();
        }

        private void TestBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxNickname_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxSiblings_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            MySQLConnectionManager connectionManager = new MySQLConnectionManager();

            try
            {
                // Check if the connection is closed and open it if necessary
                if (connectionManager.OpenConnection())
                {
                    // Obtain the connection object from the MySQLConnectionManager instance
                    MySqlConnection connection = connectionManager.GetConnection();

                    // Insert data into the users table to generate a new UserID
                    string insertUserQuery = "INSERT INTO users () VALUES ()";
                    MySqlCommand insertUserCommand = new MySqlCommand(insertUserQuery, connection);
                    insertUserCommand.ExecuteNonQuery();

                    // Retrieve the generated UserID
                    string getUserIDQuery = "SELECT LAST_INSERT_ID()";
                    MySqlCommand getUserIDCommand = new MySqlCommand(getUserIDQuery, connection);
                    int userID = Convert.ToInt32(getUserIDCommand.ExecuteScalar());

                    // Insert data into the usercredentials table with the retrieved UserID
                    string insertCredentialsQuery = "INSERT INTO usercredentials (UserID, Username, Password, PR_Nickname, PR_NumberOfSiblings) VALUES (@UserID, @Username, @Password, @PR_Nickname, @PR_NoOfSiblings)";
                    MySqlCommand insertCredentialsCommand = new MySqlCommand(insertCredentialsQuery, connection);
                    insertCredentialsCommand.Parameters.AddWithValue("@UserID", userID);
                    insertCredentialsCommand.Parameters.AddWithValue("@Username", TxtBoxUsername.Text);
                    insertCredentialsCommand.Parameters.AddWithValue("@Password", TxtBoxPassword.Text);
                    insertCredentialsCommand.Parameters.AddWithValue("@PR_Nickname", TxtBoxNickname.Text);
                    insertCredentialsCommand.Parameters.AddWithValue("@PR_NoOfSiblings", TxtBoxSiblings.Text);
                    insertCredentialsCommand.ExecuteNonQuery();

                    MessageBox.Show("Account registered successfully.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to connect to the database. Please try again later.", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure to close the connection
                connectionManager.CloseConnection();
            }
        }
    }
}
