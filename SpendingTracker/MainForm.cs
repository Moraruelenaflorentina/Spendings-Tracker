
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SpendingTracker
{
    public partial class MainForm : MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Green800,
                Primary.Green900,
                Primary.Green600,
                Accent.LightGreen200,
                TextShade.WHITE

            );


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Dashboardmbtn_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = true;
            categoryForm1.Visible = false;
            incomeForm1.Visible = false;
            expenseForm1.Visible = false;
            dashboardForm1.BringToFront();
        }

        private void Categorybtnm_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = true;
            incomeForm1.Visible = false;
            expenseForm1.Visible = false;
            categoryForm1.BringToFront();
        }

        private void Incomebtnm_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = false;
            incomeForm1.Visible = true;
            expenseForm1.Visible = false;
            incomeForm1.BringToFront();
        }

        private void Expensebtnm_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = false;
            incomeForm1.Visible = false;
            expenseForm1.Visible = true;
            expenseForm1.BringToFront();
        }

        private void logoutbtnm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LogIn logInForm = new LogIn();
                logInForm.Show();
                this.Hide();
            }
        }

        private void categoryForm1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void incomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chart op = new Chart();
            op.Show();
        }

        private void expenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartE op=new ChartE();
            op.Show();
        }
    }
}
