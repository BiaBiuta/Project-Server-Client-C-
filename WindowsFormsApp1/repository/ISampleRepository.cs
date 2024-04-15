using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public interface ISampleRepository:IRepository<int,Sample>
{
    Sample FindOneByCategoryAndAge(string category, string ageCategory);

}