using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;

namespace WebNotesSite.Models.Entities
{
    public class UserAccount
    {
        public Guid Id { get; private set; }
        public AccountProfile Profile { get; private set; }
        public AccountCredentials Credentials { get; private set; }
    }
}