using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public interface IOrganizingRepository:IRepository<int,Organizing>
{
    Organizing FindByName(string username,string password);
}