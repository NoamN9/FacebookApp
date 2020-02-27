using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class FeatureIterable : IEnumerable
    {
        public FeatureIterator GetEnumrator()
        {
            FeatureIterator FeatureIterator = new FeatureIterator();
            return FeatureIterator;
        }
    }
}
