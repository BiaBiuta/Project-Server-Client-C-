

namespace WindowsFormsApp1.domain;

public class Registration :Entity<int>
{
    public Registration(Child child, Sample sample)
    {
        Child = child;
        Sample = sample;
    }

    public Child Child { get; set; }
    public Sample Sample { get; set; }
    
}