using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    interface IEnumerator
    {
        
        bool HasNext();
        void Next();
        IFeature Current();


    }
}
