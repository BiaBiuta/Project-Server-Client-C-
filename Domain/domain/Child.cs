using System;

namespace Domain.domain
{
    public class Child :Entity<Int32>
    {
        public Child(String name,Int32 age)
        {
            Name = name;
            Age = age;
            NumberOfSamples = 0;
        }
        public Child(String childId):base()
        {
            NumberOfSamples = 0;
        }

        public String Name { get; set; }
        public Int32 Age { get; set; }
        public Int32 NumberOfSamples { get; set; } = 0;
    }
}