using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.Persistence.Repositories;

namespace WebNotes.Persistence
{
    public class PersistenceFactory
    {
        public static IAccountRepository GetAccountRepo(IRepositoryConfiguration config)
        {
            return new AccountRepository(config);
        }
    }
}
