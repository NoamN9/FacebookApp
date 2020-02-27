using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class FindMatchPress : ICommand

    {
        SportsMatchMakerLogic m_SportMatchMaker;
       public FindMatchPress(SportsMatchMakerLogic i_SportMatchMaker)
        {
            m_SportMatchMaker = i_SportMatchMaker;
        }
        
        public void Execute()
        {
            m_SportMatchMaker.m_FindMatchButton_Click();
        }
    }
}
