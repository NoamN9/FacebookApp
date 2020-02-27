using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class SettingButtonPress : ICommand
    {
        SportsMatchMakerLogic m_SportMatchMaker;
        public SettingButtonPress(SportsMatchMakerLogic i_SportMatchMaker)
        {
            m_SportMatchMaker = i_SportMatchMaker;
        }
        public void Execute()
        {
            m_SportMatchMaker.ShowButtons();
        }
    }
}
