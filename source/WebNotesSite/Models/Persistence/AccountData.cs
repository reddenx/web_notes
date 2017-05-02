using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.Persistence
{
    public class AccountData
    {
        public Guid Id { get; set; }

        public string DisplayUsername;
        public string Email;

        public string AuthToken;
        public string PasswordHash;
        public string PasswordSalt;

        public Guid[] NoteIds;
    }
}