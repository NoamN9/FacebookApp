using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;
using FacebookWrapper;

namespace FacebookApp
{
    public partial class LoginForm : Form
    {
        FacebookWrapper.ObjectModel.User m_LoggedInUser;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginFacebookAndInitMainWindow();
        }
        
        private void LoginFacebookAndInitMainWindow()
        {
            {
                try
                {
                    m_LoggedInUser = UserInApp.Instace.LoggedInUser;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                if (m_LoggedInUser != null)
                {
                    this.Hide();
                    Form MainForm = new MainForm();
                    MainForm.ShowDialog();
                    this.Close();
                }

            }
        }
    }
}
