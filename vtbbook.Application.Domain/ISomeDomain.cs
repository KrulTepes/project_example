using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public interface ISomeDomain
    {
        DbSome Add(DbSome dbSome);
    }
}