using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;


namespace SpendingTracker
{
    public partial class Chart : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;";
        public Chart()
        {
            InitializeComponent();
           
        }

        private void LoadYearComboBox()
        {
            string query = @"SELECT DISTINCT YEAR(date_income) AS year 
                         FROM [dbo].[income] 
                         WHERE user_id = @userId
                         ORDER BY year DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", Session.Id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                comboBoxYear.Items.Add("All Years");
                while (reader.Read())
                    comboBoxYear.Items.Add(reader["year"].ToString());
            }
            comboBoxYear.SelectedIndex = 0;
        }

        private void LoadCategoryComboBox()
        {
            string query = @"SELECT Id, category 
                         FROM [dbo].[category] 
                         WHERE user_id = @userId 
                           AND status = 'Active'
                           AND type = 'Income'";  

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", Session.Id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                comboBoxCategory.Items.Add("All Categories");
                comboBoxCategory.DisplayMember = "category";
                comboBoxCategory.ValueMember = "Id";

                DataTable dt = new DataTable();
                dt.Load(reader);

                comboBoxCategory.DataSource = dt;
                DataRow allRow = dt.NewRow();
                allRow["Id"] = 0;
                allRow["category"] = "All Categories";
                dt.Rows.InsertAt(allRow, 0);
            }
            comboBoxCategory.SelectedIndex = 0;
        }

        private void LoadIncomeChart()
        {
            int selectedYear = comboBoxYear.SelectedItem?.ToString() == "All Years"
                               ? 0
                               : Convert.ToInt32(comboBoxYear.SelectedItem);

            int selectedCategory = comboBoxCategory.SelectedValue != null
                                   ? Convert.ToInt32(comboBoxCategory.SelectedValue)
                                   : 0;

            string query = @"
            SELECT 
                DATENAME(MONTH, i.date_income) AS month_name,
                MONTH(i.date_income) AS month_num,
                c.category AS category_name,
                SUM(i.income) AS total_income
            FROM [dbo].[income] i
            INNER JOIN [dbo].[category] c ON i.category_id = c.Id
            WHERE i.user_id = @userId";

            if (selectedYear > 0)
                query += " AND YEAR(i.date_income) = @year";

            if (selectedCategory > 0)
                query += " AND i.category_id = @categoryId";

            query += @" GROUP BY MONTH(i.date_income), DATENAME(MONTH, i.date_income), c.category
                   ORDER BY MONTH(i.date_income)";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", Session.Id);
                if (selectedYear > 0)
                    cmd.Parameters.AddWithValue("@year", selectedYear);
                if (selectedCategory > 0)
                    cmd.Parameters.AddWithValue("@categoryId", selectedCategory);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            BuildChart(dt);
        }

        private void BuildChart(DataTable dt)
        {
            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.Titles.Clear();

            chart1.Titles.Add("Income by Month");
            chart1.ChartAreas[0].AxisX.Title = "Month";
            chart1.ChartAreas[0].AxisY.Title = "Total Income";
            chart1.ChartAreas[0].AxisX.Interval = 1;

            var categories = dt.AsEnumerable()
                               .Select(r => r["category_name"].ToString())
                               .Distinct();

            foreach (string cat in categories)
            {
                Series series = new Series(cat);
                series.ChartType = SeriesChartType.Column; 
                series.BorderWidth = 2;

                var rows = dt.AsEnumerable()
                             .Where(r => r["category_name"].ToString() == cat);

                foreach (DataRow row in rows)
                {
                    series.Points.AddXY(
                        row["month_name"].ToString(),
                        Convert.ToDouble(row["total_income"])
                    );
                }

                chart1.Series.Add(series);
            }

            chart1.Legends.Add(new Legend("Categories"));
        }



        private void Chart_Load(object sender, EventArgs e)
        {
            LoadYearComboBox();
            LoadCategoryComboBox();
            LoadIncomeChart();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
           

        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadIncomeChart();

        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        { LoadIncomeChart();

        }
    }
}
