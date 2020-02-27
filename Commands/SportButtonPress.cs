using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class SportButtonPress : ICommand
    {

        SportsMatchMakerFacade m_SportsMatchMakerFacade;

        public SportButtonPress(SportsMatchMakerFacade i_SportsMatchMakerFacade)
        {
            m_SportsMatchMakerFacade = i_SportsMatchMakerFacade;
        }
        public void Execute()
        {
            m_SportsMatchMakerFacade.ShowButtons();
        }
    }
}

