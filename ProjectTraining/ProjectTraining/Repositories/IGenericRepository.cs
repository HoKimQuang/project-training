using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTraining.Repositories
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<T>GetAll();
        
        /// <summary>
        /// Get T entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);
        
        /// <summary>
        /// Add T entity by id
        /// </summary>
        /// <param name="entityToAdd"></param>
        void Add(T entityToAdd);
        
        /// <summary>
        /// Update T entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(T entityToUpdate);
        
        /// <summary>
        /// Delete T entity
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        
        /// <summary>
        /// Get many T entity by condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Func<T, bool> where);
        
        /// <summary>
        /// IQueryble
        /// </summary>
        IQueryable<T> ObjectContext { get; set; }
    }
}