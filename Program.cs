using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

// $G$ RUL-999 (-10) Wrong mail subject name 





// $G$ THE-001 (-30) your grade on diagrams document - 70. please see comments inside the document. (50% of your grade).

namespace FacebookApp
{

    // $G$ CSS-999 (-3) class must have access modifiers
    static class Program
    {

        // $G$ CSS-999 (-3) method must have access modifiers
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
