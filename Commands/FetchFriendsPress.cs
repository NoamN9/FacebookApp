using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class FetchFriendsPress : ICommand
    {
        FetchFriendsLogic m_FetchFriends;

        public FetchFriendsPress(FetchFriendsLogic i_FetchFriends)
        {
            m_FetchFriends = i_FetchFriends;
        }
        public void Execute()
        {
            m_FetchFriends.ShowButtons();
            m_FetchFriends.FetchFriends();
        }
    }
}
