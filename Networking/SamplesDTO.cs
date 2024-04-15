using System;

namespace Networking
{
    [Serializable]
    public class SamplesDTO
    {
        private string id;
        private string sampleCategory;
        private string agecategory;

        public SamplesDTO(string id, string sampleCategory, string agecategory)
        {
            this.id = id;
            this.sampleCategory = sampleCategory;
            this.agecategory = agecategory;
        }

        public SamplesDTO(string sampleCategory, string agecategory)
        {
            this.sampleCategory = sampleCategory;
            this.agecategory = agecategory;
        }
        public virtual string Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }
        public virtual string SampleCategory
        {
            get
            {
                return sampleCategory;
            }
            set
            {
                this.sampleCategory = value;
            }
        }
        public virtual string AgeCategory
        {
            get
            {
                return agecategory;
            }
            set
            {
                this.agecategory = value;
            }
        }
        
        
    }
}