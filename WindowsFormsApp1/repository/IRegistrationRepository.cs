using System.Collections.Generic;
using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public interface IRegistrationRepository:IRepository<int,Registration>
{
    List<Child> ListChildrenForSample(Sample sample);
    int NumberOfChildrenForSample(Sample sample);
    Registration FindOneByChildAndSample(int id_chid, int id_sample);
}