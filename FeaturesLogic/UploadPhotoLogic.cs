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


namespace FacebookApp
{
    class UploadPhotoLogic : Feature, IFeature
    {
        Button m_UploadButton;
        Button m_BrowserButton;
        PictureBox m_PictureToUpload;
        FacebookWrapper.ObjectModel.User m_LoggedInUser;

        public UploadPhotoLogic(Button i_UploadButton,Button i_BrowserButton,PictureBox i_PictureToUpload)
        {
            m_LoggedInUser = UserInApp.Instace.LoggedInUser; ;
            m_UploadButton = i_UploadButton;
            m_BrowserButton = i_BrowserButton;
            m_PictureToUpload = i_PictureToUpload;
            
            
        }
        public  void HideButtons()
        {
            m_UploadButton.Visible = false;
            m_BrowserButton.Visible = false;
            m_PictureToUpload.Visible = false;
            
        }

        public  void ShowButtons()
        {
            m_UploadButton.Visible = true;
            m_BrowserButton.Visible = true;
            m_PictureToUpload.Visible = true;
            
        }
        public void m_BrowserButton_Click()
        {
            string ImageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ImageLocation = dialog.FileName;
                    m_PictureToUpload.ImageLocation = ImageLocation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Thread.Sleep(2000);
            }
        }
        public void m_UploadButton_Click()
        {
            if(m_PictureToUpload.Image == null)
            {
                MessageBox.Show("Please choose a photo");
                HideButtons();
                return;
            }
            try
            {
                m_LoggedInUser.PostPhoto(m_PictureToUpload.ImageLocation);
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
