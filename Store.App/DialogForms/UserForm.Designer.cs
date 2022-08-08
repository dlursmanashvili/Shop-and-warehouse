namespace Store.App.DialogForms
{
    partial class UserForm
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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbEmployees = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbRoles = new System.Windows.Forms.ComboBox();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.flpPassword = new System.Windows.Forms.FlowLayoutPanel();
            this.pOldPassword = new System.Windows.Forms.Panel();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtConfrimPassword = new System.Windows.Forms.TextBox();
            this.lblConfrimPassword = new System.Windows.Forms.Label();
            this.lblAssignPassword = new System.Windows.Forms.Label();
            this.flpPassword.SuspendLayout();
            this.pOldPassword.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Location = new System.Drawing.Point(103, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(269, 23);
            this.txtUsername.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "Employee";
            // 
            // cbEmployees
            // 
            this.cbEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmployees.FormattingEnabled = true;
            this.cbEmployees.Location = new System.Drawing.Point(103, 41);
            this.cbEmployees.Name = "cbEmployees";
            this.cbEmployees.Size = new System.Drawing.Size(269, 23);
            this.cbEmployees.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(216, 230);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "Role";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "Status:";
            // 
            // cbRoles
            // 
            this.cbRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoles.FormattingEnabled = true;
            this.cbRoles.Location = new System.Drawing.Point(103, 70);
            this.cbRoles.Name = "cbRoles";
            this.cbRoles.Size = new System.Drawing.Size(269, 23);
            this.cbRoles.TabIndex = 2;
            // 
            // cbIsActive
            // 
            this.cbIsActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Location = new System.Drawing.Point(57, 231);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(59, 19);
            this.cbIsActive.TabIndex = 7;
            this.cbIsActive.Text = "Active";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // flpPassword
            // 
            this.flpPassword.BackColor = System.Drawing.SystemColors.Control;
            this.flpPassword.Controls.Add(this.pOldPassword);
            this.flpPassword.Controls.Add(this.panel1);
            this.flpPassword.Controls.Add(this.panel3);
            this.flpPassword.Location = new System.Drawing.Point(12, 126);
            this.flpPassword.Name = "flpPassword";
            this.flpPassword.Size = new System.Drawing.Size(360, 87);
            this.flpPassword.TabIndex = 29;
            // 
            // pOldPassword
            // 
            this.pOldPassword.BackColor = System.Drawing.SystemColors.Control;
            this.pOldPassword.Controls.Add(this.lblOldPassword);
            this.pOldPassword.Controls.Add(this.txtOldPassword);
            this.pOldPassword.Location = new System.Drawing.Point(3, 3);
            this.pOldPassword.Name = "pOldPassword";
            this.pOldPassword.Size = new System.Drawing.Size(357, 23);
            this.pOldPassword.TabIndex = 0;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(-3, 3);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(79, 15);
            this.lblOldPassword.TabIndex = 21;
            this.lblOldPassword.Text = "Old password";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtOldPassword.Location = new System.Drawing.Point(88, 0);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.Size = new System.Drawing.Size(269, 23);
            this.txtOldPassword.TabIndex = 6;
            this.txtOldPassword.UseSystemPasswordChar = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 23);
            this.panel1.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(-2, 3);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 21;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtPassword.Location = new System.Drawing.Point(88, 0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(269, 23);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.txtConfrimPassword);
            this.panel3.Controls.Add(this.lblConfrimPassword);
            this.panel3.Location = new System.Drawing.Point(3, 61);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(357, 23);
            this.panel3.TabIndex = 2;
            // 
            // txtConfrimPassword
            // 
            this.txtConfrimPassword.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtConfrimPassword.Location = new System.Drawing.Point(88, 0);
            this.txtConfrimPassword.Name = "txtConfrimPassword";
            this.txtConfrimPassword.Size = new System.Drawing.Size(269, 23);
            this.txtConfrimPassword.TabIndex = 5;
            this.txtConfrimPassword.UseSystemPasswordChar = true;
            // 
            // lblConfrimPassword
            // 
            this.lblConfrimPassword.AutoSize = true;
            this.lblConfrimPassword.Location = new System.Drawing.Point(-3, 3);
            this.lblConfrimPassword.MaximumSize = new System.Drawing.Size(80, 0);
            this.lblConfrimPassword.Name = "lblConfrimPassword";
            this.lblConfrimPassword.Size = new System.Drawing.Size(51, 15);
            this.lblConfrimPassword.TabIndex = 23;
            this.lblConfrimPassword.Text = "Confrim";
            // 
            // lblAssignPassword
            // 
            this.lblAssignPassword.AutoSize = true;
            this.lblAssignPassword.Location = new System.Drawing.Point(12, 108);
            this.lblAssignPassword.Name = "lblAssignPassword";
            this.lblAssignPassword.Size = new System.Drawing.Size(120, 15);
            this.lblAssignPassword.TabIndex = 30;
            this.lblAssignPassword.Text = "Assign new password";
            this.lblAssignPassword.Click += new System.EventHandler(this.lblAssignPassword_Click);
            // 
            // UserForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 265);
            this.Controls.Add(this.lblAssignPassword);
            this.Controls.Add(this.flpPassword);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbRoles);
            this.Controls.Add(this.cbEmployees);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.flpPassword.ResumeLayout(false);
            this.pOldPassword.ResumeLayout(false);
            this.pOldPassword.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbEmployees;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbRoles;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.FlowLayoutPanel flpPassword;
        private System.Windows.Forms.Panel pOldPassword;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtConfrimPassword;
        private System.Windows.Forms.Label lblConfrimPassword;
        private System.Windows.Forms.Label lblAssignPassword;
    }
}