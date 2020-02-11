using ProjectTraining.DataAccess;
using ProjectTraining.Models;

namespace ProjectTraining.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {}
   
    /// <summary>
    /// User repository
    /// </summary>
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        /// <inheritdoc />
        /// <summary>
        /// UserRepository constructor
        /// </summary>
        /// <param name="appDbContext"></param>
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {}
    }
}