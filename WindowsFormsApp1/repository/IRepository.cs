using System.Collections.Generic;
using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public interface IRepository<ID, E> where E : Entity<ID>
{
    E FindOne(ID id);
    IEnumerable<E> FindAll();
    E Save(E entity);
}