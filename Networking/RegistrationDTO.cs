using System;

namespace Networking
{
    [Serializable]
    public class RegistrationDTO
    {
        private string childId;
        private string sampleId;
        private string sampleCategory;
        private string ageCategory;
        public RegistrationDTO(string childId, string sampleId)
        {
            this.childId = childId;
            this.sampleId = sampleId;
        }
        public RegistrationDTO(String childId, String sampleId, String sampleCategory, String ageCategory) {
            this.childId = childId;
            this.sampleId = sampleId;
            this.sampleCategory = sampleCategory;
            this.ageCategory = ageCategory;
        }

        public virtual string SampleCategory
        {
            get => sampleCategory;
            set => sampleCategory = value;
        }

        public virtual string AgeCategory
        {
            get => ageCategory;
            set => ageCategory = value;
        }

        public virtual string ChildId
        {
            get
            {
                return childId;
            }
            set
            {
                this.childId = value;
            }
        }
        public virtual string SampleId
        {
            get
            {
                return sampleId;
            }
            set
            {
                this.sampleId = value;
            }
        }
    }
}