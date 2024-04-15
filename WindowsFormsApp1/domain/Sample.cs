namespace WindowsFormsApp1.domain;

public class Sample : Entity<int>
{
    public Sample(SampleCategory sampleCategory, AgeCategory ageCategory)
    {
        SampleCategory = sampleCategory;
        AgeCategory = ageCategory;
    }

    public  SampleCategory SampleCategory { get; set; }
    public  AgeCategory AgeCategory { get; set; }
}