using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class FootballQuickMatchPress : ICommand
    {

        SportsMatchMakerFacade m_SportsMatchMakerFacade;

        public FootballQuickMatchPress(SportsMatchMakerFacade i_SportsMatchMakerFacade)
        {
            m_SportsMatchMakerFacade = i_SportsMatchMakerFacade;
        }
        public void Execute()
        {
            m_SportsMatchMakerFacade.m_PlayFootball_Click();
        }
    }
}

