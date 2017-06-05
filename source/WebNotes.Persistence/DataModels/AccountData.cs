using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotes.Persistence.DataModels
{
    public class AccountData
    {
        public int AccountId;

        public string Username;
        public string Email;

        public string AuthToken;
        public string PasswordHash;
        public string PasswordSalt;
    }
}
