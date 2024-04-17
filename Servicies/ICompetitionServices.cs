using System;
using System.Collections.Generic;
using Domain.domain;

namespace Servicies
{
    public interface  ICompetitionServices
    {
        Organizing FindOrganizing(string username, string password);
        IEnumerable<Sample> findAllSamle();
        Sample findSample(String age, String sample);
        List<Child> listChildrenForSample(Sample sample);
        
        Registration registerChild(Child child, Sample sample);
        Child saveChild(String name, int age);
        Child FindChild(string userName);

        Organizing login(Organizing org, ICompetitionObserver oberver);
        void logout(Organizing user, ICompetitionObserver observer);
        int numberOfChildrenForSample(Sample sample);
    }
}