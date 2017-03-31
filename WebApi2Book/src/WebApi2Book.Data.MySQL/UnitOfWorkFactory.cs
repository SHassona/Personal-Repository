using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebApi2Book.Data.MySQL
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DbContext _dbContext;

        public UnitOfWorkFactory(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace
                            .TraceInformation($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }
    }
}