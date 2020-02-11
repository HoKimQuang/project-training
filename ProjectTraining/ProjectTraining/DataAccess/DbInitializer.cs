using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace ProjectTraining.DataAccess
{
    /// <summary>
    /// This class InitialCreate Database
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Method InitialCreate Database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public static void Initialize(AppDbContext context, IConfiguration configuration)
        {
            //Check Database exist
            if (!((RelationalDatabaseCreator) context.Database.GetService<IDatabaseCreator>()).Exists())
            {
                context.Database.EnsureCreated();
            }
            else context.Database.Migrate();
        }
    }
}