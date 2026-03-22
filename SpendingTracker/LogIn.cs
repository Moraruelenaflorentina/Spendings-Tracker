using MaterialSkin;
using MaterialSkin.Controls;
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
using System.Xml.Linq;

namespace SpendingTracker
{
    public partial class LogIn : MaterialForm
    {
        SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;");

        public LogIn()
        {
            InitializeComponent();
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(
                Primary.DeepPurple700,   
                Primary.DeepPurple900,   
                Primary.DeepPurple200,   
                Accent.Purple200,       
                TextShade.WHITE         
            );
        }

        public bool checkConnection()
        {
            try
            {
                c.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
                return false;
            }
            finally
            {
                if (c.State == ConnectionState.Open)
                    c.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(panel1.ClientRectangle,
             Color.FromArgb(114, 9, 183),  // deep purple
             Color.FromArgb(13, 27, 42),   // dark navy

                 90f))
            {
                e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
            }
            using (var pen = new Pen(Color.FromArgb(100, 255, 255, 255), 1))
                e.Graphics.DrawLine(pen, 30, panel1.Height / 2 + 60, panel1.Width - 30, panel1.Height / 2 + 60);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            SignUp form1 = new SignUp();
            form1.Show();
            this.Close();
        }

      
        

        private void LogIn_Load(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = '*';
           

        }

       

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbEmail.Text) ||
                string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                
                MessageBox.Show("Please fill in all fields.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkConnection())
            {
                try
                {
                    c.Open();
                    string query = "SELECT Id, Name, Password FROM Users WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(query, c);
                    cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["Password"].ToString();
                            string enteredPass = tbPassword.Text.Trim();

                            //Verify password against stored hash
                            bool isValid = BCrypt.Net.BCrypt.Verify(enteredPass, storedHash);

                            if (isValid)
                            {
                                Session.Id = Convert.ToInt32(reader["Id"]);
                                Session.Name = reader["Name"].ToString();

                                MessageBox.Show("Login successful!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MainForm mainForm = new MainForm();
                                mainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid email or password.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid email or password.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (c.State == ConnectionState.Open)
                        c.Close();
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            using (var brush = new SolidBrush(Color.FromArgb(114, 9, 183)))
                e.Graphics.FillRectangle(brush, 40, 20, 4, 40);
        }
    }
}
