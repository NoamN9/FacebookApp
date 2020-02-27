using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class TennisQuickMatchPress : ICommand
    {

        SportsMatchMakerFacade m_SportsMatchMakerFacade;

        public TennisQuickMatchPress(SportsMatchMakerFacade i_SportsMatchMakerFacade)
        {
            m_SportsMatchMakerFacade = i_SportsMatchMakerFacade;
        }
        public void Execute()
        {
            m_SportsMatchMakerFacade.m_PlayTennis_Click();
        }
    }
}

