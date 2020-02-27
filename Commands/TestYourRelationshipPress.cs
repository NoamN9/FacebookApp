using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class TestYourRelationshipPress : ICommand
    {
       
        ProxyTestYourReleationshipLogic m_ProxyTestYourReleationship;

        public TestYourRelationshipPress(ProxyTestYourReleationshipLogic i_ProxyTestYourReleationship)
        {
            m_ProxyTestYourReleationship = i_ProxyTestYourReleationship;
        }
        public void Execute()
        {
            m_ProxyTestYourReleationship.ShowButtons();
        }
    }
}
