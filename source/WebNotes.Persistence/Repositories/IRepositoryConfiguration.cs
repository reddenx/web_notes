using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotes.Persistence.Repositories
{
    public interface IRepositoryConfiguration
    {
        string AccountSqlConnectionString { get; }
        string NoteSqlConnectionString { get; }
    }
}
