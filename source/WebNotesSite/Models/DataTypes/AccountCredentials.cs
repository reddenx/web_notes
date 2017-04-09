using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.DataTypes
{
    public class AccountCredentials
    {
        public string PasswordHash { get; private set; }
        public string AuthToken { get; private set; }
    }
}