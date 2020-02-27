using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class FeatureIterator : IEnumerator
    {
        int m_Index;
        Dictionary<e_FormState, IFeature> m_FeatureMap;
        public FeatureIterator()
        {
           
            m_Index = 0;
        }
        public Dictionary<e_FormState, IFeature> FeatureMap
        {
            get
            {
                return this.m_FeatureMap;
            }
            set
            {
                this.m_FeatureMap = value;
            }
        }

        public IFeature Current()
        {
           return m_FeatureMap.ElementAt(m_Index).Value;
        }

        public bool HasNext()
        {
            if (m_Index >= m_FeatureMap.Count)
            {
                return false;
            }

            return true;
        }

        public void Next()
        {
            m_Index++;
            m_FeatureMap.ElementAt(m_Index);
        }

        public IFeature GetFeature(e_FormState FormState)
        {
            IFeature FeatureToReturn;
            bool GotValue;
            GotValue = m_FeatureMap.TryGetValue(FormState, out FeatureToReturn);
            if (GotValue)
            {
                return FeatureToReturn;
            }
            
            return null;
        }
    }
}
