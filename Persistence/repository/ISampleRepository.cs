using Domain.domain;

namespace Persistence.repository;

public interface ISampleRepository:IRepository<int,Sample>
{
    Sample FindOneByCategoryAndAge(string category, string ageCategory);

}