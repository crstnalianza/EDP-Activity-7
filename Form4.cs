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
    public partial class Recover : Form
    {
        public Recover()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {
            // Define the start and end colors of the gradient (shades of blue and orange)
            Color startColor = Color.FromArgb(0, 122, 255);   // macOS-like blue
            Color endColor = Color.FromArgb(255, 149, 0);     // macOS-like orange

            // Create a linear gradient brush
            LinearGradientBrush brush = new LinearGradientBrush(RecoverPanel.ClientRectangle, startColor, endColor, LinearGradientMode.Vertical);

            // Fill the panel's background with the gradient brush
            e.Graphics.FillRectangle(brush, RecoverPanel.ClientRectangle);
        }

        private void LoginPanel_Paint_1(object sender, PaintEventArgs e)
        {
            // Define the start and end colors of the gradient (shades of blue and orange)
            Color startColor = Color.FromArgb(34, 34, 34);   // macOS-like blue
            Color endColor = Color.FromArgb(99, 99, 99);

            // Create a linear gradient brush
            LinearGradientBrush brush = new LinearGradientBrush(RecoverPanel.ClientRectangle, startColor, endColor, LinearGradientMode.Vertical);

            // Fill the panel's background with the gradient brush
            e.Graphics.FillRectangle(brush, RecoverPanel.ClientRectangle);
        }

        private void LoginLabel_Click(object sender, EventArgs e)
        {

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

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

          
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
            Register Register = new Register();
            Register.ShowDialog();
        }

        private void BtnCirRecover_Click(object sender, EventArgs e)
        {
            // Show the Register form
            this.Hide();
            Recover Recover = new Recover();
            Recover.ShowDialog();
        }

        private void TxtBoxNewPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxSiblings_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtBoxNickname_TextChanged(object sender, EventArgs e)
        {

        }

        private void TestBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnRecover_Click(object sender, EventArgs e)
        {
                string username = TestBoxUsername.Text;
                string siblings = TextBoxSiblings.Text;
                string nickname = TxtBoxNickname.Text;
                string newPassword = TxtBoxNewPassword.Text;

                MySQLConnectionManager connectionManager = new MySQLConnectionManager();

                if (connectionManager.OpenConnection())
                {
                    // Construct the SQL query to check if the provided credentials match
                    string query = $"SELECT COUNT(*) FROM usercredentials WHERE Username='{username}' AND PR_NumberOfSiblings='{siblings}' AND PR_Nickname='{nickname}'";

                    MySqlCommand command = new MySqlCommand(query, connectionManager.GetConnection());
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    // Check if the provided credentials match
                    if (userCount > 0)
                    {
                        // Credentials match, update the password
                        query = $"UPDATE usercredentials SET password='{newPassword}' WHERE username='{username}'";
                        command = new MySqlCommand(query, connectionManager.GetConnection());
                        command.ExecuteNonQuery();

                        MessageBox.Show("Password updated successfully.", "Password Recovery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Credentials do not match, display an error message
                        MessageBox.Show("Invalid credentials. Please try again.", "Password Recovery Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connectionManager.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Failed to connect to the database. Please try again later.", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
    }
}
