using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotes.Core.DataModels
{
    public class AccountData
    {
        public int AccountId { get; set; }

        public string Username;
        public string Email;

        public string AuthToken;
        public string PasswordHash;
        public string PasswordSalt;
    }
}
