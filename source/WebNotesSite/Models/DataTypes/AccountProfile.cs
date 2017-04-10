using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.DataTypes
{
    public class AccountProfile
    {
        public string DisplayUsername { get; private set; }
        public string Email { get; private set; }

        public AccountProfile(string displayUsername, string email)
        {
            DisplayUsername = displayUsername;
            Email = email;
        }
    }
}