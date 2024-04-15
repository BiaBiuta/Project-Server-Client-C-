using System.Collections.Generic;
using Domain.domain;

namespace Persistence.repository;

public interface IRepository<ID, E> where E : Entity<ID>
{
    E FindOne(ID id);
    IEnumerable<E> FindAll();
    E Save(E entity);
}