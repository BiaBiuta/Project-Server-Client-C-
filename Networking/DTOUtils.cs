using System;
using System.Collections.Generic;
using System.Linq;
using Domain.domain;

namespace Networking
{
    public class DTOUtils
    {
        public static Organizing getFromDTO(OrganizingDTO orgdto) {
            String username = orgdto.Username;
            String password = orgdto.Password;
            return new Organizing(username, password);
        }
        public  static  OrganizingDTO getDTO(Organizing org){
            String username=org.Username;
            String password=org.Password;
            String name=org.Name;
            return new OrganizingDTO(username,password);
        }
        public static Sample getFromDTO(SamplesDTO sampleDTO) {
            String sampleCategory = sampleDTO.SampleCategory;
            String ageCategory = sampleDTO.AgeCategory;
            String id = sampleDTO.Id;
            Sample sam=new Sample(SampleCategoryExtensions.FromString(sampleCategory), AgeCategoryExtensions.FromString(ageCategory));
            if(id!=null)
                sam.Id=int.Parse(id);

            return sam;
        }
        public static  RegistrationDTO getDTO(Registration sample) {
            String sampleId = sample.Sample.Id.ToString();
            String childId = sample.Child.Id.ToString();
            return new RegistrationDTO(childId, sampleId);
        }
        public static Registration getFromDTO(RegistrationDTO sampleDTO) {
            Sample sampleId=new Sample(sampleDTO.SampleId);
            sampleId.Id=int.Parse(sampleDTO.SampleId);
            Child childId=new Child (sampleDTO.SampleId);
            childId.Id=int.Parse(sampleDTO.ChildId);
            return new Registration(childId,sampleId);
        }
        public static  SamplesDTO getDTO(Sample sample){
        if(sample.Id.Equals(default(int))){
            String sampleCategory1=sample.SampleCategory.GetCategoryName();
            String ageCategory1=sample.AgeCategory.GetCategoryName();
            return new SamplesDTO(sampleCategory1,ageCategory1);
        }
        String id=sample.Id.ToString();
        String sampleCategory=sample.SampleCategory.GetCategoryName();
        String ageCategory=sample.AgeCategory.GetCategoryName();
        return new SamplesDTO(id,sampleCategory,ageCategory);
}
    public static Child getFromDTO(ChildDTO sampleDTO) {
        String id=sampleDTO.Id;
        String name = sampleDTO.Name;
        if (sampleDTO.Age==null)
            return new Child(name,0);
        int age = int.Parse(sampleDTO.Age);
        Child child= new Child(name, age);
        if(id!=null)
            child.Id=int.Parse(id);
        child.NumberOfSamples=int.Parse(sampleDTO.NumberOfSamples);
        return child;
    }
    public static  ChildDTO getDTO(Child sample){
        String name=sample.Name;
        String age=sample.Age.ToString();
        String numberOfSamples=sample.NumberOfSamples.ToString();
        if(sample.Id==null)
            return new ChildDTO(name,age,numberOfSamples);
        String id=sample.Id.ToString();
        ChildDTO childDTO=new ChildDTO(id,name,age,numberOfSamples);
        return childDTO;
    }
    public static List<Sample> getFromDTOSamples(IEnumerable<SamplesDTO> samplesDTO) {
        List<Sample> samples=new List<Sample>();
        samples.AddRange(samplesDTO.Select(s => getFromDTO(s)));

        return samples;
    }
    public static List <SamplesDTO> getDTOSample(IEnumerable<Sample> samples) {
        List<SamplesDTO> sample=new List<SamplesDTO>();
        foreach (Sample s in samples)
        {
            sample.Add(getDTO(s));
        }
        return sample;
    }
    public static List <RegistrationDTO> getDTOReg(List<Registration> samples) {
        List<RegistrationDTO> sample=new List<RegistrationDTO>();
        for (int i=0;i<samples.Count;i++)
            sample.Add(getDTO(samples[i]));
        return sample;
    }
    public static List <ChildDTO> getDTOChild(List<Child> samples) {
        List<ChildDTO> sample=new List<ChildDTO>();
        for (int i=0;i<samples.Count;i++)
            sample.Add(getDTO(samples[i]));
        return sample;
    }
    public static List<Child> getFromDTOChild(List<ChildDTO> samplesDTO) {
        List<Child> samples=new List<Child>();
        samplesDTO.ForEach(s => samples.Add(getFromDTO(s)));
        return samples;
    }

    }
}