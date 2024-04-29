using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HealthFitness_System
{
    public partial class Profile : Form
    {
        private MySQLConnectionManager connectionManager;
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private DataTable dataTable;

        public Profile()
        {
            InitializeComponent();
            connectionManager = new MySQLConnectionManager();
            connection = connectionManager.GetConnection();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.ShowDialog();
        }

        private void BtnWorkout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Workout workout = new Workout();
            workout.ShowDialog();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            this.Hide();
            About about = new About();
            about.ShowDialog();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "SELECT * FROM usercredentials";
                cmd = new MySqlCommand(query, connection);
                adapter = new MySqlDataAdapter(cmd);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataGrid.DataSource = dataTable;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the connection is closed and open it if necessary
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

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
                insertCredentialsCommand.Parameters.AddWithValue("@Username", TxtBoxName.Text);
                insertCredentialsCommand.Parameters.AddWithValue("@Password", TxtBoxPassword.Text);
                insertCredentialsCommand.Parameters.AddWithValue("@PR_Nickname", TxtBoxNickname.Text);
                insertCredentialsCommand.Parameters.AddWithValue("@PR_NoOfSiblings", TxtBoxSiblings.Text);
                insertCredentialsCommand.ExecuteNonQuery();

                MessageBox.Show("Record inserted successfully.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                // Close the connection
                connection.Close();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MySQLConnectionManager connectionManager = new MySQLConnectionManager();

                if (connectionManager.OpenConnection())
                {
                    string query = "UPDATE usercredentials SET Username = @Username, Password = @Password, PR_Nickname = @Nickname, PR_NumberOfSiblings = @NoOfSiblings WHERE UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, connectionManager.GetConnection());

                    cmd.Parameters.AddWithValue("@Username", TxtBoxName.Text);
                    cmd.Parameters.AddWithValue("@Password", TxtBoxPassword.Text);
                    cmd.Parameters.AddWithValue("@Nickname", TxtBoxNickname.Text);
                    cmd.Parameters.AddWithValue("@NoOfSiblings", TxtBoxSiblings.Text);
                    cmd.Parameters.AddWithValue("@UserID", TxtBoxID.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Failed to connect to the database. Please try again later.", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionManager.CloseConnection();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "DELETE FROM usercredentials WHERE UserID = @UserID";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserID", TxtBoxID.Text);
                cmd.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Record deleted successfully.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "SELECT * FROM usercredentials WHERE UserID = @UserID";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserID", TxtBoxID.Text);
                adapter = new MySqlDataAdapter(cmd);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    TxtBoxName.Text = dataTable.Rows[0]["Username"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found.");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        private void TxtBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
