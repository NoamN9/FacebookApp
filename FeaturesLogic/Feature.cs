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
    abstract class Feature
    {
        
        List<Control> m_ControlToPresnt = new List<Control>();

        public IFilterQuestionControls QustionControlFilter;
        public void HideButtons(List<Control> i_ListToHide)
        {
            foreach(Control ControlToHide in i_ListToHide)
            {
                ControlToHide.Visible = false;
            }


        }
        public void ShowButtons(List<Control> i_ListToShow)
        {
            foreach (Control ControlToShow in i_ListToShow)
            {
                ControlToShow.Visible = true;
            }


        }

        public void HideQuestion()
        {
            QustionControlFilter.HideButtons();
        }
        public void ShowQuestion()
        {
            QustionControlFilter.HideButtons();
        }

    }
}
