




using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository
{
    public interface IChildRepository:IRepository<int,Child>
    {
        Child Update(Child entityForUpdate);

        Child FindByName(string name);
    }
}