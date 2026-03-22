namespace SpendingTracker
{
    partial class SignUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUp));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox2 = new MaterialSkin.Controls.MaterialCheckBox();
            this.checkBox1 = new MaterialSkin.Controls.MaterialCheckBox();
            this.tbCPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tbPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tbEmail = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tbName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Indigo;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Name = "label9";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label6.Name = "label6";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(9)))), ((int)(((byte)(183)))));
            this.label8.Name = "label8";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.materialRaisedButton1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.tbCPassword);
            this.panel2.Controls.Add(this.tbPassword);
            this.panel2.Controls.Add(this.tbEmail);
            this.panel2.Controls.Add(this.tbName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Depth = 0;
            resources.ApplyResources(this.materialRaisedButton1, "materialRaisedButton1");
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.label5.Name = "label5";
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Depth = 0;
            this.checkBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.checkBox2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Ripple = true;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.materialCheckBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Depth = 0;
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.checkBox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Ripple = true;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.materialCheckBox1_CheckedChanged);
            // 
            // tbCPassword
            // 
            this.tbCPassword.Depth = 0;
            this.tbCPassword.Hint = "Confirm Password";
            resources.ApplyResources(this.tbCPassword, "tbCPassword");
            this.tbCPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.tbCPassword.Name = "tbCPassword";
            this.tbCPassword.PasswordChar = '\0';
            this.tbCPassword.SelectedText = "";
            this.tbCPassword.SelectionLength = 0;
            this.tbCPassword.SelectionStart = 0;
            this.tbCPassword.UseSystemPasswordChar = false;
            // 
            // tbPassword
            // 
            this.tbPassword.Depth = 0;
            this.tbPassword.Hint = "Password";
            resources.ApplyResources(this.tbPassword, "tbPassword");
            this.tbPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '\0';
            this.tbPassword.SelectedText = "";
            this.tbPassword.SelectionLength = 0;
            this.tbPassword.SelectionStart = 0;
            this.tbPassword.UseSystemPasswordChar = false;
            // 
            // tbEmail
            // 
            this.tbEmail.Depth = 0;
            this.tbEmail.Hint = "Email Adress";
            resources.ApplyResources(this.tbEmail, "tbEmail");
            this.tbEmail.MouseState = MaterialSkin.MouseState.HOVER;
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.PasswordChar = '\0';
            this.tbEmail.SelectedText = "";
            this.tbEmail.SelectionLength = 0;
            this.tbEmail.SelectionStart = 0;
            this.tbEmail.UseSystemPasswordChar = false;
            // 
            // tbName
            // 
            this.tbName.Depth = 0;
            this.tbName.Hint = "Username";
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.MouseState = MaterialSkin.MouseState.HOVER;
            this.tbName.Name = "tbName";
            this.tbName.PasswordChar = '\0';
            this.tbName.SelectedText = "";
            this.tbName.SelectionLength = 0;
            this.tbName.SelectionStart = 0;
            this.tbName.UseSystemPasswordChar = false;
            // 
            // SignUp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.Name = "SignUp";
            this.Load += new System.EventHandler(this.SignUp_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private MaterialSkin.Controls.MaterialSingleLineTextField tbName;
        private MaterialSkin.Controls.MaterialSingleLineTextField tbCPassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField tbPassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField tbEmail;
        private MaterialSkin.Controls.MaterialCheckBox checkBox2;
        private MaterialSkin.Controls.MaterialCheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

