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

namespace SpendingTracker
{
    public partial class DashboardForm : UserControl
    {
        public DashboardForm()
        {
            InitializeComponent();
            todayIncome();
            todayExpense();
            incomeYesterday();
            expenseYesterday();
            thisMonthIncome();
            thisYearIncome();
            thisMonthExpense();
            thisYearExpense();
            totalIncome();
            totalExpense();
        }

        public void todayIncome()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(income) FROM income WHERE date_income = @date_income";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@date_income", today);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal todayIncome = Convert.ToDecimal(result);
                        lbTodayInc.Text = todayIncome.ToString("C");
                    }
                    else
                    {
                        lbTodayInc.Text = "$0.00";
                    }
                }

            }

        }

        public void incomeYesterday()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(income) FROM income WHERE CONVERT(DATE, date_income) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal yesterdayIncome = Convert.ToDecimal(result);
                        lbYesterdayInc.Text = yesterdayIncome.ToString("C");
                    }
                    else
                    {
                        lbYesterdayInc.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        public void thisMonthIncome()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(income) FROM income WHERE MONTH(date_income) = MONTH(GETDATE()) AND YEAR(date_income) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisMonthIncome = Convert.ToDecimal(result);
                        lbMonthInc.Text = thisMonthIncome.ToString("C");
                    }
                    else
                    {
                        lbMonthInc.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        public void thisYearIncome()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(income) FROM income WHERE YEAR(date_income) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisYearIncome = Convert.ToDecimal(result);
                        lbYearInc.Text = thisYearIncome.ToString("C");
                    }
                    else
                    {
                        lbYearInc.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        public void totalIncome()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(income) FROM income";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal totalIncome = Convert.ToDecimal(result);
                        lbTotalInc.Text = totalIncome.ToString("C");
                    }
                    else
                    {
                        lbTotalInc.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        //expense

        public void todayExpense()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(payment) FROM expense WHERE date_payment = @date_payment";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@date_payment", today);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal todayExpe = Convert.ToDecimal(result);
                        lbTodayExp.Text = todayExpe.ToString("C");
                    }
                    else
                    {
                        lbTodayExp.Text = "$0.00";
                    }
                }

            }

        }

        public void expenseYesterday()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(payment) FROM expense WHERE CONVERT(DATE, date_payment) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal yesterdayIncome = Convert.ToDecimal(result);
                        lbYesterdayExp.Text = yesterdayIncome.ToString("C");
                    }
                    else
                    {
                        lbYesterdayExp.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        public void thisMonthExpense()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(payment) FROM expense WHERE MONTH(date_payment) = MONTH(GETDATE()) AND YEAR(date_payment) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisMonthIncome = Convert.ToDecimal(result);
                        lbMonthExp.Text = thisMonthIncome.ToString("C");
                    }
                    else
                    {
                        lbMonthExp.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        public void thisYearExpense()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(payment) FROM expense WHERE YEAR(date_payment) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisYearIncome = Convert.ToDecimal(result);
                        lbYearExp.Text = thisYearIncome.ToString("C");
                    }
                    else
                    {
                        lbYearExp.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        public void totalExpense()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            {
                c.Open();
                string query = "SELECT SUM(payment) FROM expense";
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal totalIncome = Convert.ToDecimal(result);
                        lbTotalExp.Text = totalIncome.ToString("C");
                    }
                    else
                    {
                        lbTotalExp.Text = "$0.00";
                    }
                }
                c.Close();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
