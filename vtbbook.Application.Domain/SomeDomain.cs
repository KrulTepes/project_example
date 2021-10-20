using vtbbook.Core.DataAccess;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public class SomeDomain : ISomeDomain
    {
        private readonly IDataContext _dataContext;

        public SomeDomain(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbSome Add(DbSome dbSome)
        {
            if (dbSome == null)
            {
                return null;
            }

            var newDbsome = _dataContext.Insert(dbSome);

            if (newDbsome == null)
            {
                return null;
            }

            return _dataContext.Save() != 0 ? newDbsome : null;
        }
    }
}
