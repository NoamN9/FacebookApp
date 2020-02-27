using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FacebookApp
{
    class PostSomethingPress : ICommand
    {
        PostSomethingLogic m_PostSomething;
        
        public PostSomethingPress(PostSomethingLogic i_PostSomething)
        {
            m_PostSomething = i_PostSomething;
        }
        
        public void Execute()
        {
            m_PostSomething.ShowButtons();
        }
    }
}
