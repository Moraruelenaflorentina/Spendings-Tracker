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
using System.Configuration;
using MaterialSkin;
using MaterialSkin.Controls;
using BCrypt.Net;


namespace SpendingTracker
{
    public partial class SignUp : MaterialForm
    {
        SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;");

        public SignUp()
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
            LogIn logIn = new LogIn();
            logIn.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbEmail.Text) ||
                string.IsNullOrWhiteSpace(tbPassword.Text) ||
                string.IsNullOrWhiteSpace(tbCPassword.Text))
            {
                
                MessageBox.Show("Please fill in all fields.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = tbPassword.Text;
            if (password.Length < 8 ||
                !password.Any(char.IsDigit) ||
                !password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                MessageBox.Show("Password must be at least 8 characters long, contain " +
                    "at least one number and one special character.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tbPassword.Text != tbCPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!checkConnection())
                return;

            try
            {
                c.Open();

                string selectUsername = "SELECT COUNT(*) FROM users WHERE Name = @Name";
                using (SqlCommand checkUser = new SqlCommand(selectUsername, c))
                {
                    checkUser.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbName.Text.Trim();
                    int userExists = (int)checkUser.ExecuteScalar();

                    if (userExists > 0)
                    {
                        
                        MessageBox.Show("Username already exists.", "Error Message",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string selectEmail = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                using (SqlCommand checkEmail = new SqlCommand(selectEmail, c))
                {
                    checkEmail.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbEmail.Text.Trim();
                    int emailExists = (int)checkEmail.ExecuteScalar();

                    if (emailExists > 0)
                    {
                        MessageBox.Show("Email already registered.", "Error Message",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string insertData = @"INSERT INTO users (Name, Password, Email, Date_Create) 
                                      VALUES (@Name, @Password, @Email, @Date_Create);
                                      SELECT SCOPE_IDENTITY();";

                using (SqlCommand insertUser = new SqlCommand(insertData, c))
                {
                    insertUser.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbName.Text.Trim();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(tbPassword.Text.Trim());
                    insertUser.Parameters.Add("@Password", SqlDbType.VarChar).Value = hashedPassword;

                   // insertUser.Parameters.Add("@Password", SqlDbType.VarChar).Value = tbPassword.Text.Trim();
                    insertUser.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbEmail.Text.Trim();
                    insertUser.Parameters.Add("@Date_Create", SqlDbType.Date).Value = DateTime.Today;

                    int newUserId = Convert.ToInt32(insertUser.ExecuteScalar());

                    Session.Id = newUserId;
                    Session.Name = tbName.Text.Trim();
                }

                MessageBox.Show("Account created successfully!", "Success Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (c.State == ConnectionState.Open)
                    c.Close();
            }

        }
      

        private void SignUp_Load(object sender, EventArgs e)
        {
           
            tbCPassword.PasswordChar = '*';
            tbPassword.PasswordChar = '*';
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';

        }

        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            tbCPassword.PasswordChar = checkBox2.Checked ? '\0' : '*';

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb(114, 9, 183)))
                e.Graphics.FillRectangle(brush, 40, 20, 4, 40);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            // 1. Validate empty fields
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbEmail.Text) ||
                string.IsNullOrWhiteSpace(tbPassword.Text) ||
                string.IsNullOrWhiteSpace(tbCPassword.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Validate password strength
            string password = tbPassword.Text;
            if (password.Length < 8 ||
                !password.Any(char.IsDigit) ||
                !password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                MessageBox.Show("Password must be at least 8 characters long, contain " +
                    "at least one number and one special character.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Confirm passwords match
            if (tbPassword.Text != tbCPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Check connection
            if (!checkConnection())
                return;

            try
            {
                c.Open();

                // 5. Check if username already exists
                string selectUsername = "SELECT COUNT(*) FROM users WHERE Name = @Name";
                using (SqlCommand checkUser = new SqlCommand(selectUsername, c))
                {
                    checkUser.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbName.Text.Trim();
                    int userExists = (int)checkUser.ExecuteScalar();

                    if (userExists > 0)
                    {
                        MessageBox.Show("Username already exists.", "Error Message",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 6. Check if email already exists
                string selectEmail = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                using (SqlCommand checkEmail = new SqlCommand(selectEmail, c))
                {
                    checkEmail.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbEmail.Text.Trim();
                    int emailExists = (int)checkEmail.ExecuteScalar();

                    if (emailExists > 0)
                    {
                        MessageBox.Show("Email already registered.", "Error Message",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 7. Insert new user and get new ID
                string insertData = @"INSERT INTO users (Name, Password, Email, Date_Create) 
                                      VALUES (@Name, @Password, @Email, @Date_Create);
                                      SELECT SCOPE_IDENTITY();";

                using (SqlCommand insertUser = new SqlCommand(insertData, c))
                {
                    insertUser.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbName.Text.Trim();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(tbPassword.Text.Trim());
                    insertUser.Parameters.Add("@Password", SqlDbType.VarChar).Value = hashedPassword;

                    // insertUser.Parameters.Add("@Password", SqlDbType.VarChar).Value = tbPassword.Text.Trim();
                    insertUser.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbEmail.Text.Trim();
                    insertUser.Parameters.Add("@Date_Create", SqlDbType.Date).Value = DateTime.Today;

                    // Get the new user's ID from SCOPE_IDENTITY()
                    int newUserId = Convert.ToInt32(insertUser.ExecuteScalar());

                    // 8. Set session so user is logged in immediately
                    Session.Id = newUserId;
                    Session.Name = tbName.Text.Trim();
                }

                MessageBox.Show("Account created successfully!", "Success Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 9. Open MainForm only after successful registration
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (c.State == ConnectionState.Open)
                    c.Close();
            }
        }
    }
}
