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
    class FetchFriendsLogic: Feature, IFeature
    {
        ListBox m_FriendsList;
        User m_LoggedInUser;
        Panel m_FriendDetailsPane;
        BindingSource m_userBindingSource;

        public FetchFriendsLogic(ListBox i_FriendsListBox,Panel i_FriendDetailsPane, BindingSource i_userBindingSource)
        {
            m_FriendsList = i_FriendsListBox;
            m_LoggedInUser = UserInApp.Instace.LoggedInUser;
            m_FriendDetailsPane = i_FriendDetailsPane;
            m_userBindingSource = i_userBindingSource;
        }
        public  void  HideButtons()
        {
            m_FriendsList.Visible = false;
            m_FriendDetailsPane.Visible = false;
        }

        public  void ShowButtons()
        {
            m_FriendsList.Visible = true;
            m_FriendDetailsPane.Visible = true;
        }
        public void FetchFriends()
        {
            m_userBindingSource.DataSource = m_LoggedInUser.Friends;
        }

    }
}
