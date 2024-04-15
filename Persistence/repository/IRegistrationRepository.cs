using System.Collections.Generic;
using Domain.domain;

namespace Persistence.repository;

public interface IRegistrationRepository:IRepository<int,Registration>
{
    List<Child> ListChildrenForSample(Sample sample);
    int NumberOfChildrenForSample(Sample sample);
    Registration FindOneByChildAndSample(int id_chid, int id_sample);
}