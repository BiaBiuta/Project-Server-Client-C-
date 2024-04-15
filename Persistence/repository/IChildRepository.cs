using Domain.domain;

namespace Persistence.repository
{
    public interface IChildRepository:IRepository<int,Child>
    {
        Child Update(Child entityForUpdate);

        Child FindByName(string name);
    }
}