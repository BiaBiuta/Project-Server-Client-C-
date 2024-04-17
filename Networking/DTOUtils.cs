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
            if (orgdto.Id != null)
            {
                Organizing org=new Organizing(username,password);
                org.Id=int.Parse(orgdto.Id);
                return org;
            }
            return new Organizing(username, password);
        }
        public  static  OrganizingDTO getDTO(Organizing org){
            String username=org.Username;
            String password=org.Password;
            String name=org.Name;
            if(org.Id!=0) {
                String id=org.Id.ToString();
                return new OrganizingDTO(id, username, password);
            }
            else {
                return new OrganizingDTO(username, password);
            }
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
            return new RegistrationDTO(childId, sampleId,sample.Sample.SampleCategory.GetCategoryName(),sample.Sample.AgeCategory.GetCategoryName());
        }
        public static Registration getFromDTO(RegistrationDTO sampleDTO) {
            Sample sampleId=new Sample(sampleDTO.SampleId);
            if(sampleDTO.SampleCategory!=null && sampleDTO.AgeCategory!=null) {
                sampleId.SampleCategory=SampleCategoryExtensions.FromString(sampleDTO.SampleCategory);
                sampleId.AgeCategory=AgeCategoryExtensions.FromString(sampleDTO.AgeCategory);
            }
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
        if(sample.SampleCategory.GetCategoryName()==null|| sample.AgeCategory.GetCategoryName()==null){
            return new SamplesDTO(id);
        }
        String sampleCategory=sample.SampleCategory.GetCategoryName();
        String ageCategory=sample.AgeCategory.GetCategoryName();
        return new SamplesDTO(id,sampleCategory,ageCategory);
}
    public static Child getFromDTO(ChildDTO sampleDTO) {
        if (sampleDTO == null)
        {
            return null;
        }
        String id=sampleDTO.Id;
        String name = sampleDTO.Name;
        if (sampleDTO.Age==null)
            return new Child(name,0);
        int age = int.Parse(sampleDTO.Age);
        Child child= new Child(name, age);
        if(id!=null)
            child.Id=int.Parse(id);

        if (sampleDTO.NumberOfSamples == null)
        {
            child.NumberOfSamples = 0;
        }
        else
        {
            child.NumberOfSamples = int.Parse(sampleDTO.NumberOfSamples);
        }

        return child;
    }
    public static  ChildDTO getDTO(Child sample){
        if (sample == null)
        {
            return null;
        }
        String name=sample.Name;
        String age=sample.Age.ToString();
        String numberOfSamples=sample.NumberOfSamples.ToString();
        if(sample.Id==0)
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