using System;
using Application.Entities.Base;
namespace Application.Dal.Contract
{
    /// <summary>
    /// TEntity: An Entity Clas used for CRUD
    /// TPk: The Primary Key, used for Search Single Record for Read, Update, and Delete
    /// in: Metans this will always be an inout parameter to a method
    /// The TENtity is having a 'Constraints' that, it will be Typed-to only Classes derined from 'Entity' base class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPk"></typeparam>
    public interface IDataAccess<TEntity, in TPk> where TEntity:Entity
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TPk id);
        TEntity Create(TEntity entity);
        TEntity Update(TPk id, TEntity entity);
        TEntity Delete(TPk id);
    }
}

