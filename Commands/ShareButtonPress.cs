using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class ShareButtonPress : ICommand
    {

        TestYourRelationshipLogic m_testYourRelationship;

        public ShareButtonPress(TestYourRelationshipLogic i_testYourRelationship)
        {
            m_testYourRelationship = i_testYourRelationship;
        }
        public void Execute()
        {
            m_testYourRelationship.ShareButton_click();
        }
    }
}

