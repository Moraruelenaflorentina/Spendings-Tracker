using PdfSharp.Fonts;
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
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;



namespace SpendingTracker
{
    public partial class IncomeForm : UserControl
    {
        public IncomeForm()
        {
            InitializeComponent();
            DisplayCategoryList();
            DisplayIncomeList();
            CategoryForm.CategoriesChanged += DisplayCategoryList;

        }



        public void DisplayCategoryList()
        {
            cbCategory.Items.Clear();
            string selectData = "SELECT Id, category FROM category WHERE type=@type AND status=@status AND user_id=@user_id";

            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            using (SqlCommand cmd = new SqlCommand(selectData, conn))
            {
                cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Income";
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "Active";
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = Session.Id; // ← add this

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbCategory.Items.Add(new CategoryItem
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["category"].ToString()
                        });
                    }
                }
            }
            cbCategory.DisplayMember = "Name";
        }

        public void DisplayIncomeList()
        {
            string selectData = @"SELECT i.Id, c.category AS Category, i.item AS Item, 
                                 i.income AS Income, i.description AS Description,
                                 i.date_income AS DateIncome
                          FROM income i
                          INNER JOIN category c ON i.category_id = c.Id
                          WHERE i.user_id = @user_id";

            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
            using (SqlCommand cmd = new SqlCommand(selectData, conn))
            {
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = Session.Id;

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Id"].Visible = false; 
                dataGridView1.Columns["DateIncome"].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
        }
      

        public void Clear()
        {
            cbCategory.SelectedIndex = -1;
            tbItem.Text = "";
            tbIncome.Text = "";
            tbDescription.Text = "";
            dateTimePicker1.Value = DateTime.Today;
        }
        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex == -1 || tbIncome.Text == "" ||
    tbItem.Text == "" || tbDescription.Text == "")
            {
                MessageBox.Show("Please fill in all the blanks.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!decimal.TryParse(tbIncome.Text, out decimal incomeValue))
            {
                MessageBox.Show("Please enter a valid income amount.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            CategoryItem selectedCategory = (CategoryItem)cbCategory.SelectedItem;

            string insertData = @"INSERT INTO income 
                            (user_id, category_id, item, income, description, date_income, date_insert) 
                          VALUES 
                            (@user_id, @category_id, @item, @income, @description, @date_income, @date_insert)";

            try
            {
                using (SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
                using (SqlCommand cmd = new SqlCommand(insertData, c))
                {
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = Session.Id; 
                    cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = selectedCategory.Id;
                    cmd.Parameters.Add("@item", SqlDbType.NVarChar).Value = tbItem.Text.Trim();
                    cmd.Parameters.Add("@income", SqlDbType.Decimal).Value = incomeValue;
                    cmd.Parameters.Add("@description", SqlDbType.Text).Value = tbDescription.Text.Trim();
                    cmd.Parameters.Add("@date_income", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
                    cmd.Parameters.Add("@date_insert", SqlDbType.Date).Value = DateTime.Today;

                    c.Open();
                    cmd.ExecuteNonQuery();

                   // MessageBox.Show("Income entry added successfully!", "Success Message",
                       // MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Clear();
                    DisplayIncomeList(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int getID = 0;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (getID == 0)
            {
                MessageBox.Show("Please select a record to update.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (cbCategory.SelectedIndex == -1 || tbItem.Text == "" ||
                tbIncome.Text == "" || tbDescription.Text == "")
            {
                MessageBox.Show("Please fill in all the blanks.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!decimal.TryParse(tbIncome.Text, out decimal incomeValue))
            {
                MessageBox.Show("Please enter a valid income amount.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //Get selected category ID
            CategoryItem selectedCategory = (CategoryItem)cbCategory.SelectedItem;

            try
            {
                string updateData = @"UPDATE income 
                              SET category_id  = @category_id,
                                  item         = @item,
                                  income       = @income,
                                  description  = @description,
                                  date_income  = @date_income
                              WHERE Id = @Id 
                              AND user_id = @user_id"; 

                using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
                using (SqlCommand cmd = new SqlCommand(updateData, conn))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = getID;
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = Session.Id;
                    cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = selectedCategory.Id;
                    cmd.Parameters.Add("@item", SqlDbType.NVarChar).Value = tbItem.Text.Trim();
                    cmd.Parameters.Add("@income", SqlDbType.Decimal).Value = incomeValue;
                    cmd.Parameters.Add("@description", SqlDbType.Text).Value = tbDescription.Text.Trim();
                    cmd.Parameters.Add("@date_income", SqlDbType.Date).Value = dateTimePicker1.Value.Date;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                   // MessageBox.Show("Income updated successfully!", "Success Message",
                      //  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getID = 0; 
                    Clear();
                    DisplayIncomeList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (getID == 0)
            {
                MessageBox.Show("Please select a record to delete.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this income entry?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                string deleteData = "DELETE FROM income WHERE Id = @Id AND user_id = @user_id"; 

                using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;"))
                using (SqlCommand cmd = new SqlCommand(deleteData, conn))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = getID;
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = Session.Id; 

                    conn.Open();
                    cmd.ExecuteNonQuery();

                   // MessageBox.Show("Income entry deleted successfully!", "Success Message",
                     //   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getID = 0;       
                    Clear();         
                    DisplayIncomeList(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
    }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; 

            try
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = Convert.ToInt32(row.Cells["Id"].Value);

                string categoryName = row.Cells["Category"].Value.ToString();
                foreach (CategoryItem item in cbCategory.Items)
                {
                    if (item.Name == categoryName)
                    {
                        cbCategory.SelectedItem = item;
                        break;
                    }
                }

                tbItem.Text = row.Cells["Item"].Value.ToString();
                tbIncome.Text = row.Cells["Income"].Value.ToString();
                tbDescription.Text = row.Cells["Description"].Value.ToString();

                // Check for null date before converting
                if (row.Cells["DateIncome"].Value != null && row.Cells["DateIncome"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateIncome"].Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting row: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Export Expenses to Excel";
                saveDialog.FileName = $"Income_{DateTime.Today:yyyy-MM-dd}.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var workbook = new ClosedXML.Excel.XLWorkbook())
                    {
                        var ws = workbook.Worksheets.Add("Expenses");

                        //Header Row
                        string[] headers = { "Category", "Item", "Income", "Description", "Date" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            var cell = ws.Cell(1, i + 1);
                            cell.Value = headers[i];
                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromArgb(33, 150, 243); // blue header
                            cell.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                            cell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                        }

                        //Data Rows
                        int row = 2;
                        foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                        {
                            if (dgvRow.IsNewRow) continue;

                            ws.Cell(row, 1).Value = dgvRow.Cells["Category"].Value?.ToString() ?? "";
                            ws.Cell(row, 2).Value = dgvRow.Cells["Item"].Value?.ToString() ?? "";

                            if (decimal.TryParse(dgvRow.Cells["Income"].Value?.ToString(), out decimal payment))
                            {
                                ws.Cell(row, 3).Value = payment;
                                ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0.00";
                            }

                            ws.Cell(row, 4).Value = dgvRow.Cells["Description"].Value?.ToString() ?? "";

                            if (DateTime.TryParse(dgvRow.Cells["DateIncome"].Value?.ToString(), out DateTime date))
                            {
                                ws.Cell(row, 5).Value = date;
                                ws.Cell(row, 5).Style.DateFormat.Format = "MM/dd/yyyy";
                            }

                            row++;
                        }

                        //Totals Row
                        if (row > 2)
                        {
                            ws.Cell(row, 2).Value = "TOTAL";
                            ws.Cell(row, 2).Style.Font.Bold = true;
                            ws.Cell(row, 3).FormulaA1 = $"=SUM(C2:C{row - 1})";
                            ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0.00";
                            ws.Cell(row, 3).Style.Font.Bold = true;
                            ws.Cell(row, 3).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromArgb(232, 245, 233);
                        }

                        //Auto-fit columns
                        ws.Columns().AdjustToContents();

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show("Income exported successfully!", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export error: " + ex.Message, "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private class CustomFontResolver : IFontResolver
        {
            public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
            {
                if (isBold) return new FontResolverInfo("Arial#b");
                return new FontResolverInfo("Arial#r");
            }

            public byte[] GetFont(string faceName)
            {
                string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                string file = faceName == "Arial#b" ? "arialbd.ttf" : "arial.ttf";
                return File.ReadAllBytes(Path.Combine(fontsFolder, file));
            }
        }

        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            // Wrap in quotes if value contains a comma, quote, or newline
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV Files|*.csv";
                saveDialog.Title = "Export Income to CSV";
                saveDialog.FileName = $"Income_{DateTime.Today:yyyy-MM-dd}.csv";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var sb = new StringBuilder();

                    // Header row
                    sb.AppendLine("Category,Item,Income,Description,Date");

                    // Data rows
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string category = EscapeCsv(row.Cells["Category"].Value?.ToString());
                        string item = EscapeCsv(row.Cells["Item"].Value?.ToString());
                        string payment = row.Cells["Income"].Value?.ToString() ?? "";
                        string description = EscapeCsv(row.Cells["Description"].Value?.ToString());
                        string date = DateTime.TryParse(row.Cells["DateIncome"].Value?.ToString(), out DateTime d)
                                             ? d.ToString("MM/dd/yyyy") : "";

                        sb.AppendLine($"{category},{item},{payment},{description},{date}");
                    }

                    File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);

                    MessageBox.Show("Income exported successfully!", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export error: " + ex.Message, "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (GlobalFontSettings.FontResolver == null)
                GlobalFontSettings.FontResolver = new CustomFontResolver();

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF Files|*.pdf";
                saveDialog.Title = "Export Income to PDF";
                saveDialog.FileName = $"Income_{DateTime.Today:yyyy-MM-dd}.pdf";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    PdfDocument document = new PdfDocument();
                    document.Info.Title = "Income Report";

                    PdfPage page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Fonts
                    XFont titleFont = new XFont("Arial", 14);
                    XFont headerFont = new XFont("Arial", 9);
                    XFont cellFont = new XFont("Arial", 8);

                    double pageWidth = page.Width.Point;
                    double margin = 40;
                    double y = margin;

                    //Title
                    gfx.DrawString("Income Report", titleFont, XBrushes.Black,
                        new XRect(margin, y, pageWidth - margin * 2, 20), XStringFormats.TopLeft);
                    y += 25;

                    gfx.DrawString($"Generated: {DateTime.Today:MM/dd/yyyy}", cellFont, XBrushes.Gray,
                        new XRect(margin, y, pageWidth - margin * 2, 15), XStringFormats.TopLeft);
                    y += 20;

                    //Column setup
                    string[] headers = { "Category", "Item", "Income", "Description", "Date" };
                    double[] colWidths = { 120, 130, 80, 220, 80 };
                    double rowHeight = 18;

                    //  Header row 
                    double x = margin;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        XRect rect = new XRect(x, y, colWidths[i], rowHeight);
                        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(33, 150, 243)), rect);
                        gfx.DrawString(headers[i], headerFont, XBrushes.White,
                            new XRect(x + 3, y + 3, colWidths[i] - 6, rowHeight), XStringFormats.TopLeft);
                        x += colWidths[i];
                    }
                    y += rowHeight;

                    //  Data rows 
                    bool alternate = false;
                    decimal total = 0;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        //New page if needed
                        if (y + rowHeight > page.Height.Point - margin)
                        {
                            page = document.AddPage();
                            page.Orientation = PdfSharp.PageOrientation.Landscape;
                            gfx = XGraphics.FromPdfPage(page);
                            y = margin;
                        }

                        x = margin;
                        XBrush bgBrush = alternate
                            ? new XSolidBrush(XColor.FromArgb(232, 240, 254))
                            : XBrushes.White;

                        string[] values =
                        {
                    row.Cells["Category"].Value?.ToString() ?? "",
                    row.Cells["Item"].Value?.ToString() ?? "",
                    decimal.TryParse(row.Cells["Income"].Value?.ToString(), out decimal p)
                        ? p.ToString("#,##0.00") : "",
                    row.Cells["Description"].Value?.ToString() ?? "",
                    DateTime.TryParse(row.Cells["DateIncome"].Value?.ToString(), out DateTime d)
                        ? d.ToString("MM/dd/yyyy") : ""
                };

                        if (decimal.TryParse(row.Cells["Income"].Value?.ToString(), out decimal pVal))
                            total += pVal;

                        for (int i = 0; i < values.Length; i++)
                        {
                            XRect rect = new XRect(x, y, colWidths[i], rowHeight);
                            gfx.DrawRectangle(bgBrush, rect);
                            gfx.DrawRectangle(XPens.LightGray, rect);

                            //Right payment column
                            XStringFormat fmt = i == 2 ? XStringFormats.TopRight : XStringFormats.TopLeft;
                            double padding = i == 2 ? 4 : 3;
                            gfx.DrawString(values[i], cellFont, XBrushes.Black,
                                new XRect(x + padding, y + 3, colWidths[i] - padding * 2, rowHeight),
                                fmt);

                            x += colWidths[i];
                        }

                        y += rowHeight;
                        alternate = !alternate;
                    }

                    //Total row
                    y += 4;
                    x = margin + colWidths[0] + colWidths[1];
                    gfx.DrawString("TOTAL:", headerFont, XBrushes.Black,
                        new XRect(margin, y + 3, colWidths[0] + colWidths[1] - 4, rowHeight),
                        XStringFormats.TopRight);
                    gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(200, 230, 201)),
                        new XRect(x, y, colWidths[2], rowHeight));
                    gfx.DrawString(total.ToString("#,##0.00"), headerFont, XBrushes.Black,
                        new XRect(x + 4, y + 3, colWidths[2] - 8, rowHeight), XStringFormats.TopRight);

                    document.Save(saveDialog.FileName);

                    MessageBox.Show("PDF exported successfully!", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export error: " + ex.Message, "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
