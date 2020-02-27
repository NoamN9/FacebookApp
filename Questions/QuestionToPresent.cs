using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FacebookApp
{
    class QuestionToPresent : IFilterQuestionControls
    {
        List<QuestionControl> m_ControlToPresnt = new List<QuestionControl>();
        int m_QuestionNumber;

        public QuestionToPresent(List<QuestionControl> i_QuestionsControlls,int i_QuestionNumber)
        {
            m_ControlToPresnt = i_QuestionsControlls;
            m_QuestionNumber = i_QuestionNumber;
        }
        public void HideButtons()
        {
            foreach (QuestionControl ControlToHide in m_ControlToPresnt)
            {
                if( m_QuestionNumber == ControlToHide.QuestionNumber)
                {
                    ControlToHide.Control.Visible = false;
                }
            }
        }

        public void ShowButtons()
        {
            foreach (QuestionControl ControlToHide in m_ControlToPresnt)
            {
                if (m_QuestionNumber == ControlToHide.QuestionNumber)
                {
                    ControlToHide.Control.Visible = true;
                }
            }
        }
    }
}
