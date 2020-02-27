using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Facebook;
using FacebookWrapper;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;


namespace FacebookApp
{
    class PostSomethingLogic : Feature, IFeature

    {
        private TextBox m_TextBox;
        Button m_SendButton;
        User m_LoggedInUser;

        public PostSomethingLogic()
        {
            m_LoggedInUser = UserInApp.Instace.LoggedInUser; ;
        }
        
        public TextBox TextBox
        {
            get { return m_TextBox; }
            set { m_TextBox = value; }
        }
         
        public Button SendButton
        {
            get { return m_SendButton; }
            set { m_SendButton = value; }
        }
        public  void HideButtons()
        {
            m_TextBox.Visible = false;
            m_SendButton.Visible = false;
           
        }

        public  void ShowButtons()
        {
            m_TextBox.Visible = true;
            m_SendButton.Visible = true;
            
        }
        public void SendButton_Click()
        {
            try
            {
                m_LoggedInUser.PostStatus(m_TextBox.Text);
                m_TextBox.Text = "Write here to post something!";
                MessageBox.Show("Posted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Thread.Sleep(2000);
            }
            
            HideButtons();
        }
    }
}
