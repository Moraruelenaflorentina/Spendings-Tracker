namespace SpendingTracker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.logoutbtnm = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Expensebtnm = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Incomebtnm = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Categorybtnm = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Dashboardmbtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dashboardForm1 = new SpendingTracker.DashboardForm();
            this.categoryForm1 = new SpendingTracker.CategoryForm();
            this.incomeForm1 = new SpendingTracker.IncomeForm();
            this.expenseForm1 = new SpendingTracker.ExpenseForm();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.logoutbtnm);
            this.panel1.Controls.Add(this.Expensebtnm);
            this.panel1.Controls.Add(this.Incomebtnm);
            this.panel1.Controls.Add(this.Categorybtnm);
            this.panel1.Controls.Add(this.Dashboardmbtn);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // logoutbtnm
            // 
            this.logoutbtnm.Depth = 0;
            resources.ApplyResources(this.logoutbtnm, "logoutbtnm");
            this.logoutbtnm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(102)))), ((int)(((byte)(40)))));
            this.logoutbtnm.MouseState = MaterialSkin.MouseState.HOVER;
            this.logoutbtnm.Name = "logoutbtnm";
            this.logoutbtnm.Primary = true;
            this.logoutbtnm.UseVisualStyleBackColor = true;
            this.logoutbtnm.Click += new System.EventHandler(this.logoutbtnm_Click);
            // 
            // Expensebtnm
            // 
            this.Expensebtnm.Depth = 0;
            this.Expensebtnm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(102)))), ((int)(((byte)(40)))));
            resources.ApplyResources(this.Expensebtnm, "Expensebtnm");
            this.Expensebtnm.MouseState = MaterialSkin.MouseState.HOVER;
            this.Expensebtnm.Name = "Expensebtnm";
            this.Expensebtnm.Primary = true;
            this.Expensebtnm.UseVisualStyleBackColor = true;
            this.Expensebtnm.Click += new System.EventHandler(this.Expensebtnm_Click);
            // 
            // Incomebtnm
            // 
            this.Incomebtnm.Depth = 0;
            this.Incomebtnm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(102)))), ((int)(((byte)(40)))));
            resources.ApplyResources(this.Incomebtnm, "Incomebtnm");
            this.Incomebtnm.MouseState = MaterialSkin.MouseState.HOVER;
            this.Incomebtnm.Name = "Incomebtnm";
            this.Incomebtnm.Primary = true;
            this.Incomebtnm.UseVisualStyleBackColor = true;
            this.Incomebtnm.Click += new System.EventHandler(this.Incomebtnm_Click);
            // 
            // Categorybtnm
            // 
            this.Categorybtnm.Depth = 0;
            this.Categorybtnm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(102)))), ((int)(((byte)(40)))));
            resources.ApplyResources(this.Categorybtnm, "Categorybtnm");
            this.Categorybtnm.MouseState = MaterialSkin.MouseState.HOVER;
            this.Categorybtnm.Name = "Categorybtnm";
            this.Categorybtnm.Primary = true;
            this.Categorybtnm.UseVisualStyleBackColor = true;
            this.Categorybtnm.Click += new System.EventHandler(this.Categorybtnm_Click);
            // 
            // Dashboardmbtn
            // 
            this.Dashboardmbtn.Depth = 0;
            this.Dashboardmbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(102)))), ((int)(((byte)(40)))));
            resources.ApplyResources(this.Dashboardmbtn, "Dashboardmbtn");
            this.Dashboardmbtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.Dashboardmbtn.Name = "Dashboardmbtn";
            this.Dashboardmbtn.Primary = true;
            this.Dashboardmbtn.UseVisualStyleBackColor = true;
            this.Dashboardmbtn.Click += new System.EventHandler(this.Dashboardmbtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dashboardForm1);
            this.panel2.Controls.Add(this.categoryForm1);
            this.panel2.Controls.Add(this.incomeForm1);
            this.panel2.Controls.Add(this.expenseForm1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // dashboardForm1
            // 
            this.dashboardForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.dashboardForm1, "dashboardForm1");
            this.dashboardForm1.Name = "dashboardForm1";
            // 
            // categoryForm1
            // 
            resources.ApplyResources(this.categoryForm1, "categoryForm1");
            this.categoryForm1.Name = "categoryForm1";
            // 
            // incomeForm1
            // 
            resources.ApplyResources(this.incomeForm1, "incomeForm1");
            this.incomeForm1.Name = "incomeForm1";
            // 
            // expenseForm1
            // 
            resources.ApplyResources(this.expenseForm1, "expenseForm1");
            this.expenseForm1.Name = "expenseForm1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incomeToolStripMenuItem,
            this.expenseToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // incomeToolStripMenuItem
            // 
            this.incomeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.incomeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.incomeToolStripMenuItem.Name = "incomeToolStripMenuItem";
            resources.ApplyResources(this.incomeToolStripMenuItem, "incomeToolStripMenuItem");
            this.incomeToolStripMenuItem.Click += new System.EventHandler(this.incomeToolStripMenuItem_Click);
            // 
            // expenseToolStripMenuItem
            // 
            this.expenseToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.expenseToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.expenseToolStripMenuItem.Name = "expenseToolStripMenuItem";
            resources.ApplyResources(this.expenseToolStripMenuItem, "expenseToolStripMenuItem");
            this.expenseToolStripMenuItem.Click += new System.EventHandler(this.expenseToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialRaisedButton Dashboardmbtn;
        private MaterialSkin.Controls.MaterialRaisedButton logoutbtnm;
        private MaterialSkin.Controls.MaterialRaisedButton Expensebtnm;
        private MaterialSkin.Controls.MaterialRaisedButton Incomebtnm;
        private MaterialSkin.Controls.MaterialRaisedButton Categorybtnm;
        private System.Windows.Forms.Panel panel2;
        private DashboardForm dashboardForm1;
        private CategoryForm categoryForm1;
        private IncomeForm incomeForm1;
        private ExpenseForm expenseForm1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expenseToolStripMenuItem;
    }
}