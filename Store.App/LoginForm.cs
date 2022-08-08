using System;
using Store.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Store.App
{
    public partial class LoginForm : Form
    {
        private UserService _userService = new(Program.configuration);

        public LoginForm()
        {
            InitializeComponent();
#if DEBUG
            txtUsername.Text = "user";
            txtPassword.Text = "password";
#endif
        }

        private void OldLoginForm_Shown(object sender, EventArgs e) => txtUsername.Focus();

        private void btnCancel_Click(object sender, EventArgs e) => Application.Exit();

        private void btnOk_Click(object sender, EventArgs e) => Login();

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Login();
        }

        private void Login()
        {
            try
            {
                if (txtUsername.Text == "") throw new Exception("Username is empty!");
                if (txtPassword.Text == "") throw new Exception("Password is empty!");

                if (_userService.Login(txtUsername.Text, txtPassword.Text))
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{ex.Message}",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}
