



using System;


namespace WindowsFormsApp1.domain
{
    public class Child :Entity<Int32>
    {
        public Child(String name,Int32 age)
        {
            Name = name;
            Age = age;
        }

        public String Name { get; set; }
        public Int32 Age { get; set; }
        public Int32 NumberOfSamples { get; set; } = 0;
    }
}