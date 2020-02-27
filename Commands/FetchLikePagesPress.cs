using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class FetchLikePagesPress : ICommand
    {
        FetchLikePagesLogic m_FetchLikePages;

        public FetchLikePagesPress(FetchLikePagesLogic i_FetchLikePages)
        {
            m_FetchLikePages = i_FetchLikePages;
        }
        public void Execute()
        {
            m_FetchLikePages.ShowButtons();
            m_FetchLikePages.FetchLikePages();
        }
    }
}
