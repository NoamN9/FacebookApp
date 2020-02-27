using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FacebookApp
{
    public class QuestionControl : Control
    {
        Control m_Control;
        int m_QuestionTag;

        public QuestionControl(Control i_Control)
        {
            this.m_Control = i_Control;

        }
        public Control Control
        {
            get { return m_Control; }
            set { m_Control = value; }
        }
        public int QuestionNumber
        {
            get { return m_QuestionTag; }
            set { m_QuestionTag = value; }
        }

    }

}