using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class MatchInfoPress : ICommand

    {
        SportsMatchMakerLogic m_SportMatchMaker;
        public MatchInfoPress(SportsMatchMakerLogic i_SportMatchMaker)
        {
            m_SportMatchMaker = i_SportMatchMaker;
        }

        public void Execute()
        {
            m_SportMatchMaker.m_GetMatchInfo_Click();
        }
    }
}
