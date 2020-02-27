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
using FacebookWrapper.ObjectModel;

namespace FacebookApp
{
    class FetchLikePagesLogic : Feature, IFeature
    {
        ListBox m_PageLikeListBox;
        FacebookWrapper.ObjectModel.User m_LoggedInUser;

        public FetchLikePagesLogic(ListBox i_PageLikeListBox)
        {
            m_PageLikeListBox = i_PageLikeListBox;
            m_LoggedInUser = UserInApp.Instace.LoggedInUser; ;
        }

        public  void  HideButtons()
        {
            m_PageLikeListBox.Visible = false;
        }

        public  void ShowButtons()
        {
            m_PageLikeListBox.Visible = true;
        }

        public void FetchLikePages()
        {
            m_PageLikeListBox.Items.Clear();

            m_PageLikeListBox.DisplayMember = "Name";

            try
            {
                if (m_LoggedInUser.LikedPages.Count == 0)
                {

                    MessageBox.Show("No Pages to retrieve :(");
                }
                else
                {
                    foreach (Page page in m_LoggedInUser.LikedPages)
                    {
                        m_PageLikeListBox.Items.Add(page);
                        page.ReFetch(DynamicWrapper.eLoadOptions.Full);
                    }
                }         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Thread.Sleep(2000);
                HideButtons();
            }
        }
    }
}
