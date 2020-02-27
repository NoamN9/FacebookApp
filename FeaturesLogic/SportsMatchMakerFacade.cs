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
using System.Collections.Generic;
using System.Globalization;

namespace FacebookApp
{
    class SportsMatchMakerFacade: Feature, IFeature
    {
        GroupBox m_GenralGroup;
        Label m_QuickMatchLabel;
        Button m_PlayFootball;
        Button m_PlayTennis;
        Label m_CustomizeSearch;
        Button m_SetSearch;

        private SportsMatchMakerQuickPicks m_QuickPicks;

        public SportsMatchMakerFacade(GroupBox i_GeneralGroup,Label i_QuickMatchLabel,Button i_PlayFootball, Button i_PlayTennis, Label i_CustomizeSearch,Button i_SetSearch )
        {
            m_GenralGroup = i_GeneralGroup;
            m_QuickMatchLabel = i_QuickMatchLabel;
            m_PlayFootball = i_PlayFootball;
            m_CustomizeSearch = i_CustomizeSearch;
            m_SetSearch = i_SetSearch;
            m_PlayTennis = i_PlayTennis;
            m_QuickPicks = new SportsMatchMakerQuickPicks();
        }

        public  void HideButtons()
        {
            m_GenralGroup.Visible = false;
            m_QuickMatchLabel.Visible = false;
            m_PlayFootball.Visible = false;
            m_PlayTennis.Visible = false;
            m_CustomizeSearch.Visible = false;
            m_SetSearch.Visible = false;
        }

        public  void ShowButtons()
        {
            m_GenralGroup.Visible = true;
            m_QuickMatchLabel.Visible = true;
            m_PlayFootball.Visible = true;
            m_CustomizeSearch.Visible = true;
            m_SetSearch.Visible = true;
            m_PlayTennis.Visible = true;
        }

        public void m_PlayFootball_Click()
        {
            m_QuickPicks.FirstQuickPick();
        }

        public void m_PlayTennis_Click()
        {
            m_QuickPicks.SecondQuickPick();
        }
    }
}
