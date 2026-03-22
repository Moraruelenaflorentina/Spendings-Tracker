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
    public partial class CategoryForm : UserControl
    {
        SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;");
        private int currentUserId;

        public CategoryForm()
        {
            InitializeComponent();
            displayCategoryList();


        }
        public static event Action CategoriesChanged;

        public void displayCategoryList()
        {
            CategoryData cData = new CategoryData();
            List<CategoryData> listData = cData.categoryListDatas(Session.Id); // ← Session.Id
            dataGridView1.DataSource = listData;

            dataGridView1.Columns["CategoryID"].Visible = false;
            dataGridView1.Columns["Date"].DefaultCellStyle.Format = "MM/dd/yyyy";
        }


       
        public void clear()
        {
            tbCategory.Clear();
            cbType.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbCategory.Text == "" || cbType.SelectedIndex == -1 || cbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all the fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                c.Open();
                string insertData = "INSERT INTO category (user_id, category, type, status, date_insert) " +
                                    "VALUES (@user_id, @category, @type, @status, @date_insert)";
                using (SqlCommand cmd = new SqlCommand(insertData, c))
                {
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = Session.Id;
                    cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = tbCategory.Text.Trim();
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = cbType.SelectedItem.ToString();
                    cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = cbStatus.SelectedItem.ToString();
                    cmd.Parameters.Add("@date_insert", SqlDbType.Date).Value = DateTime.Today;

                    cmd.ExecuteNonQuery();
                   // MessageBox.Show("Data added successfully!", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

            displayCategoryList();
            CategoriesChanged?.Invoke();
        }

        private int getID = 0;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbCategory.Text == "" || cbType.SelectedIndex == -1 || cbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                try
                {
                    c.Open();
                    string updateData = "UPDATE category SET category=@category, type=@type, status=@status WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(updateData, c);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", getID);
                    cmd.Parameters.AddWithValue("@category", tbCategory.Text.Trim());
                    cmd.Parameters.AddWithValue("@type", cbType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@status", cbStatus.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    clear();
                    //MessageBox.Show("Data updated successfully!", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    c.Close();
                }
            }
            displayCategoryList();
            CategoriesChanged?.Invoke();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbCategory.Text == "" || cbType.SelectedIndex == -1 || cbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item.", "Error Mesage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this entri?", "Confirmation message", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    c.Open();
                    string deleteData = "DELETE FROM category WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(deleteData, c))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = getID;


                        cmd.ExecuteNonQuery();
                        clear();
                       // MessageBox.Show("Data deleted successfully!", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    c.Close();
                }

            }
            displayCategoryList();
            CategoriesChanged?.Invoke();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();

        }

        private void tbCategory_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                getID = Convert.ToInt32(row.Cells["CategoryID"].Value); //uses hidden real ID
                tbCategory.Text = row.Cells["Category"].Value.ToString();
                cbType.SelectedItem = row.Cells["Type"].Value.ToString();
                cbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
