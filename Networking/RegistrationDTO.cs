using System;

namespace Networking
{
    [Serializable]
    public class RegistrationDTO
    {
        private string childId;
        private string sampleId;

        public RegistrationDTO(string childId, string sampleId)
        {
            this.childId = childId;
            this.sampleId = sampleId;
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