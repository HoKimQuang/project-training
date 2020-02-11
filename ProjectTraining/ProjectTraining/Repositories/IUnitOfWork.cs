namespace ProjectTraining.Repositories
{
    /// <summary>
    /// Unit Of Work interface
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Declare entities User Repository
        /// </summary>
        IUserRepository UserRepository { get; }
        
        /// <summary>
        /// Save
        /// </summary>
        void Save();
    }
}