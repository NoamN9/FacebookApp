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

namespace FacebookApp
{
     class ProxyTestYourReleationshipLogic: Feature, ITestYourRelationshipLogic, IFeature
    {
         TestYourRelationshipLogic m_TestYourRelationshipLogic;
        public ProxyTestYourReleationshipLogic(TestYourRelationshipLogic i_TestYourRelationshipLogic)
        {
            m_TestYourRelationshipLogic = i_TestYourRelationshipLogic;
        }

      
        public void HideButtons()
        {
            m_TestYourRelationshipLogic.HideButtons();
        }

        public void ShowButtons()
        {
            if (ValidFetchData())
            {
                m_TestYourRelationshipLogic.ShowButtons(); 
            }
            else
            {
                MessageBox.Show("There is an error fetching this feature please try restart the app");
            }
        }

        public void FetchData()
        {
            m_TestYourRelationshipLogic.FetchData();
           
        }
        private bool ValidFetchData()
        {

            if (!m_TestYourRelationshipLogic.m_EnoughFriendsToPlay || !m_TestYourRelationshipLogic.m_EnoughLikePhotos)
            {
                return false;
            }

            return true;
            
        }

    }
}
