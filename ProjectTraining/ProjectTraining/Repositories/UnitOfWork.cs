using ProjectTraining.DataAccess;

namespace ProjectTraining.Repositories
{
    /// <summary>
    /// Unit Of Work
    /// </summary>
    public class UnitOfwork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private IUserRepository _userRepository;

       /// <summary>
       /// Unit OfWork constructor
       /// </summary>
       /// <param name="appDbContext"></param>
        public UnitOfwork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        #region Properties
        
        /// <inheritdoc />
        /// <summary>
        /// Check User Repository entities
        /// </summary>
        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_appDbContext); }
        }
        #endregion

        #region Methods
        
        /// <inheritdoc />
        /// <summary>
        /// Methods Save In DB Context
        /// </summary>
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
        #endregion
    }
}