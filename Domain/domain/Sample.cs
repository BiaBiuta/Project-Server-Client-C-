
using System;

namespace Domain.domain;

public class Sample : Entity<int>
{
    public Sample(SampleCategory sampleCategory, AgeCategory ageCategory)
    {
        SampleCategory = sampleCategory;
        AgeCategory = ageCategory;
    }
    public Sample(String sampleId) :base(){
    }

    public  SampleCategory SampleCategory { get; set; }
    public  AgeCategory AgeCategory { get; set; }
}