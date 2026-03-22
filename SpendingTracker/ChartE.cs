using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpendingTracker
{
    public partial class ChartE : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;";
        int currentUserId = Session.Id; 
        bool isLoading = false;
        public ChartE()
        {
            InitializeComponent();

            isLoading = true;           
            LoadMonthComboBox();        
            LoadYearComboBox();         
            LoadCategoryComboBox();     
            isLoading = false;          

            LoadExpenseChart();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return; 
            LoadExpenseChart();
        }

        private void ChartE_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return; 
            LoadExpenseChart();
        }

        private void LoadMonthComboBox()
        {
            comboBoxMonth.Items.Clear(); 

            string[] months = {
                "January", "February", "March", "April",
                "May", "June", "July", "August",
                "September", "October", "November", "December"
            };

            foreach (string m in months)
                comboBoxMonth.Items.Add(m);

            comboBoxMonth.SelectedIndex = DateTime.Now.Month - 1; 
        }

        //Load Years 
        private void LoadYearComboBox()
        {
            comboBoxYear.Items.Clear(); 

            string query = @"SELECT DISTINCT YEAR(date_payment) AS year 
                             FROM [dbo].[expense] 
                             WHERE user_id = @userId
                             ORDER BY year DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", currentUserId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    comboBoxYear.Items.Add(reader["year"].ToString());
            }

            if (comboBoxYear.Items.Count == 0)
                comboBoxYear.Items.Add(DateTime.Now.Year.ToString());

            comboBoxYear.SelectedItem = DateTime.Now.Year.ToString();
            if (comboBoxYear.SelectedIndex < 0)
                comboBoxYear.SelectedIndex = 0;
        }

        //Load Categories
        private void LoadCategoryComboBox()
        {
            string query = @"SELECT Id, category 
                     FROM [dbo].[category] 
                     WHERE user_id = @userId 
                       AND status = 'Active'
                       AND type = 'Expense'";

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("category", typeof(string));

            DataRow allRow = dt.NewRow();
            allRow["Id"] = 0;
            allRow["category"] = "All Categories";
            dt.Rows.Add(allRow);

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", currentUserId);
                conn.Open();

                DataTable temp = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(temp);

                foreach (DataRow row in temp.Rows)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Id"] = row["Id"];
                    newRow["category"] = row["category"];
                    dt.Rows.Add(newRow);
                }
            }

            comboBoxCategory.DataSource = dt;
            comboBoxCategory.DisplayMember = "category";
            comboBoxCategory.ValueMember = "Id";
            comboBoxCategory.SelectedIndex = 0;
        }
        
        private void LoadExpenseChart()
        {
            int selectedMonth = (comboBoxMonth.SelectedIndex >= 0)
                                ? comboBoxMonth.SelectedIndex + 1
                                : DateTime.Now.Month;

            int selectedYear;
            if (comboBoxYear.SelectedItem == null ||
                !int.TryParse(comboBoxYear.SelectedItem.ToString(), out selectedYear))
                selectedYear = DateTime.Now.Year;

            int selectedCategory = 0;
            if (comboBoxCategory.SelectedItem is DataRowView row)
                int.TryParse(row["Id"].ToString(), out selectedCategory);

            if (selectedMonth < 1 || selectedMonth > 12) selectedMonth = DateTime.Now.Month;
            if (selectedYear < 1 || selectedYear > 9999) selectedYear = DateTime.Now.Year;

            string query = @"
                SELECT 
                    DAY(e.date_payment)  AS day_num,
                    c.category           AS category_name,
                    SUM(e.payment)       AS total_expense
                FROM [dbo].[expense] e
                INNER JOIN [dbo].[category] c ON e.category_id = c.Id
                WHERE e.user_id = @userId
                  AND MONTH(e.date_payment) = @month
                  AND YEAR(e.date_payment)  = @year";

            if (selectedCategory > 0)
                query += " AND e.category_id = @categoryId";

            query += @" GROUP BY DAY(e.date_payment), c.category
                        ORDER BY DAY(e.date_payment)";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", currentUserId);
                cmd.Parameters.AddWithValue("@month", selectedMonth);
                cmd.Parameters.AddWithValue("@year", selectedYear);
                if (selectedCategory > 0)
                    cmd.Parameters.AddWithValue("@categoryId", selectedCategory);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            BuildChart(dt, selectedMonth, selectedYear);
        }

        
        private void BuildChart(DataTable dt, int month, int year)
        {
            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.Titles.Clear();

            if (month < 1 || month > 12) month = DateTime.Now.Month;
            if (year < 1 || year > 9999) year = DateTime.Now.Year;

            string monthName = new DateTime(year, month, 1).ToString("MMMM yyyy");
            int daysInMonth = DateTime.DaysInMonth(year, month);

            chart1.Titles.Add(new Title($"Expenses – {monthName}",
                                         Docking.Top,
                                         new Font("Segoe UI", 12, FontStyle.Bold),
                                         Color.DimGray));

            chart1.ChartAreas[0].AxisX.Title = "Day";
            chart1.ChartAreas[0].AxisY.Title = "Total Expense";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = daysInMonth;

            var categories = dt.AsEnumerable()
                               .Select(r => r["category_name"].ToString())
                               .Distinct();

            foreach (string cat in categories)
            {
                Series series = new Series(cat);
                series.ChartType = SeriesChartType.Column;
                series.BorderWidth = 2;

                foreach (DataRow row in dt.AsEnumerable()
                                          .Where(r => r["category_name"].ToString() == cat))
                {
                    series.Points.AddXY(
                        Convert.ToInt32(row["day_num"]),
                        Convert.ToDouble(row["total_expense"])
                    );
                }

                chart1.Series.Add(series);
            }

            chart1.Legends.Add(new Legend("Categories"));

            labelTotal.Text = dt.Rows.Count > 0
                ? $"Total Expenses: {dt.AsEnumerable().Sum(r => Convert.ToDecimal(r["total_expense"])):C}"
                : "No expenses found for this period.";
        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return; 
            LoadExpenseChart();
        }

        private void labelTotal_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

}
