using Domain.domain;

namespace Persistence.repository;

public interface IOrganizingRepository:IRepository<int,Organizing>
{
    Organizing FindByName(string username,string password);
}