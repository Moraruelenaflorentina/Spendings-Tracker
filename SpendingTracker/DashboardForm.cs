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
        
       public void RefreshAll()
       {
          todayIncome(); todayExpense();
          incomeYesterday(); expenseYesterday();
          thisMonthIncome(); thisMonthExpense();
          thisYearIncome(); thisYearExpense();
          totalIncome(); totalExpense();
        }
        
         public void todayIncome()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(income) FROM income WHERE date_income = @date_income AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@date_income", DateTime.Today);
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbTodayInc.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void incomeYesterday()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(income) FROM income WHERE CONVERT(DATE, date_income) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1) AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbYesterdayInc.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void thisMonthIncome()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(income) FROM income WHERE MONTH(date_income) = MONTH(GETDATE()) AND YEAR(date_income) = YEAR(GETDATE()) AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbMonthInc.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void thisYearIncome()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(income) FROM income WHERE YEAR(date_income) = YEAR(GETDATE()) AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbYearInc.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void totalIncome()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(income) FROM income WHERE user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbTotalInc.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void todayExpense()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(payment) FROM expense WHERE date_payment = @date_payment AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@date_payment", DateTime.Today);
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbTodayExp.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void expenseYesterday()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(payment) FROM expense WHERE CONVERT(DATE, date_payment) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1) AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbYesterdayExp.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void thisMonthExpense()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(payment) FROM expense WHERE MONTH(date_payment) = MONTH(GETDATE()) AND YEAR(date_payment) = YEAR(GETDATE()) AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbMonthExp.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void thisYearExpense()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(payment) FROM expense WHERE YEAR(date_payment) = YEAR(GETDATE()) AND user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbYearExp.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
     }
 }

 public void totalExpense()
 {
     using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
     {
         c.Open();
         string query = "SELECT SUM(payment) FROM expense WHERE user_id = @user_id";
         using (SqlCommand cmd = new SqlCommand(query, c))
         {
             cmd.Parameters.AddWithValue("@user_id", Session.Id);

             object result = cmd.ExecuteScalar();
             lbTotalExp.Text = (result != DBNull.Value && result != null)
                 ? Convert.ToDecimal(result).ToString("C") : "$0.00";
         }
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
