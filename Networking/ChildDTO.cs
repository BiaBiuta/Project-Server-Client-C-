using System;

namespace Networking
{
    [Serializable]
    public class ChildDTO
    {
        private String id;
        private String name;
        private String age;
        private String numberOfSamples;

        public ChildDTO(string id, string name, string age, string numberOfSamples)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.numberOfSamples = numberOfSamples;
        }

        public ChildDTO(string name, string age)
        {
            this.name = name;
            this.age = age;
        }

        public ChildDTO(string name)
        {
            this.name = name;
        }

        public ChildDTO(string name, string age, string numberOfSamples)
        {
            this.name = name;
            this.age = age;
            this.numberOfSamples = numberOfSamples;
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
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name= value;
            }
        }
        public virtual string Age
        {
            get
            {
                return age;
            }
            set
            {
                this.age= value;
            }
        }
        public virtual string NumberOfSamples
        {
            get
            {
                return numberOfSamples;
            }
            set
            {
                this.numberOfSamples= value;
            }
        }
    }
}