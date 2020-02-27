using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class SubmitButtonPress : ICommand
    {

        TestYourRelationshipLogic m_testYourRelationship;

        public SubmitButtonPress(TestYourRelationshipLogic i_testYourRelationship)
        {
            m_testYourRelationship = i_testYourRelationship;
        }
        public void Execute()
        {
            m_testYourRelationship.m_SubmitButton_Click();
        }
    }
}
