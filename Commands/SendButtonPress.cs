using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class SendButtonPress : ICommand
    {
        PostSomethingLogic m_PostSomething;

        public SendButtonPress(PostSomethingLogic i_PostSomething)
        {
            m_PostSomething = i_PostSomething;
        }


        public void Execute()
        {
            m_PostSomething.SendButton_Click();
        }
    }

}
