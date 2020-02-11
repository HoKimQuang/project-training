using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.DataAccess;

namespace ProjectTraining.Repositories
{
    /// <inheritdoc/>
    /// <summary>
    ///  Generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Properties
       private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;
        #endregion

        /// <summary>
        /// GenericRepository constructor
        /// </summary>
        /// <param name="appDbContext"></param>
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
            ObjectContext = _dbSet;
        }

        #region Implementtation
        
        /// <inheritdoc />
        /// <summary>
        /// Get all entities
        /// </summary>
        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }
        /// <inheritdoc />
        /// <summary>
        /// Get T entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Add T entity by id
        /// </summary>
        /// <param name="entityToAdd"></param>
        public void Add(T entityToAdd)
        {
            _dbSet.Add(entityToAdd);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Update T entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _appDbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Delete T entity
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            _dbSet.Remove(entityToDelete);
            _appDbContext.SaveChanges();
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Get many T entity by condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<T> GetMany(Func<T, bool> @where)
        {
            return _dbSet.AsEnumerable().Where(where);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public IQueryable<T> ObjectContext { get; set; }

        #endregion
    }
}