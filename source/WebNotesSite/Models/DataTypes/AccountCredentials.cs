using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.DataTypes
{
    public class AccountCredentials
    {
        public readonly string AuthToken;
        public readonly string PasswordHash;
        public readonly string PasswordSalt;

        public AccountCredentials(string authToken, string passwordHash, string passwordSalt)
        {
            AuthToken = authToken;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}