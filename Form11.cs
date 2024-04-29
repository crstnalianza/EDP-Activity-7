using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using Google.Protobuf.WellKnownTypes;

namespace HealthFitness_System
{
    public partial class Goals : Form
    {
        private MySQLConnectionManager connectionManager;
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private DataTable dataTable;
        public Goals()
        {
            InitializeComponent();
            connectionManager = new MySQLConnectionManager();
            connection = connectionManager.GetConnection();
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

        private void BtnWorkout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Workout Workout = new Workout();
            Workout.ShowDialog();
        }

        private void Goals_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "SELECT * FROM goals";
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Workout Workout = new Workout();
            Workout.ShowDialog();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
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

                    string insertExerciseQuery = "INSERT INTO goals (UserID, GoalType, Target, StartDate, Deadline) VALUES (@UserID, @GoalType, @Target, @StartDate, @Deadline)";
                    MySqlCommand insertExerciseCommand = new MySqlCommand(insertExerciseQuery, connection);
                    insertExerciseCommand.Parameters.AddWithValue("@UserID", userID);
                    insertExerciseCommand.Parameters.AddWithValue("@GoalType", GoalType.Text);
                    insertExerciseCommand.Parameters.AddWithValue("@Target", Target.Text);
                    insertExerciseCommand.Parameters.AddWithValue("@StartDate", StartDate.Value.Date);
                    insertExerciseCommand.Parameters.AddWithValue("@Deadline", EndDate.Value.Date);
                    insertExerciseCommand.ExecuteNonQuery();


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
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        private void ExerciseType_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the DataGridView has any data
                if (DataGrid.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Prompt user to select a location to save the Excel file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.FileName = "ExportedWorkoutData_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"; // Append timestamp to ensure uniqueness
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Load Excel template
                    string templateFilePath = @"C:\Users\Cristina\Documents\HealthFitnessReport.xlsx";
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Open(templateFilePath);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Export DataGridView data to Sheet1 starting from row 11
                    for (int i = 0; i < DataGrid.Columns.Count; i++)
                    {
                        worksheet.Cells[11, i + 1] = DataGrid.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < DataGrid.Rows.Count; i++)
                    {
                        for (int j = 0; j < DataGrid.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 12, j + 1] = DataGrid.Rows[i].Cells[j].Value;
                        }
                    }

                    // Create a new worksheet with a unique name
                    string newWorksheetName = "Chart";
                    Excel.Worksheet worksheet2 = (Excel.Worksheet)workbook.Sheets.Add(After: worksheet);
                    worksheet2.Name = newWorksheetName;

                    // Create a chart based on the entire DataGridView
                    Excel.ChartObjects chartObjects = (Excel.ChartObjects)worksheet2.ChartObjects(System.Type.Missing);
                    Excel.ChartObject chartObject = chartObjects.Add(100, 100, 300, 300);
                    Excel.Chart chart = chartObject.Chart;
                    int rowCount = DataGrid.Rows.Count;
                    int columnCount = DataGrid.Columns.Count;
                    Excel.Range range = worksheet.Range[worksheet.Cells[11, 1], worksheet.Cells[11 + rowCount - 1, columnCount]];
                    chart.SetSourceData(range, System.Type.Missing);
                    chart.ChartType = Excel.XlChartType.xlColumnClustered;

                    // Save and close the workbook
                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();

                    MessageBox.Show("Data exported successfully.", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
