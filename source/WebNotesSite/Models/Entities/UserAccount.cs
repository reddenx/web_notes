using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Entities
{
    public class UserAccount
    {
        public Guid Id { get; private set; }
        public AccountProfile Profile { get; private set; }
        public AccountCredentials Credentials { get; private set; }

        public Note[] Notes { get; private set; }
        //public SharedNoteRelationship[] SharedNoteRelationships { get; private set; }

        public Guid GenerateNewAuthToken()
        {
            var newAuthToken = Guid.NewGuid();
            Credentials = new AccountCredentials(
                passwordHash: Credentials.PasswordHash,
                authToken: newAuthToken.ToString(),
                passwordSalt: Credentials.PasswordSalt);

            return newAuthToken;
        }

        public static UserAccount FromData(AccountData data, Note[] notes)
        {
            var account = new UserAccount()
            {
                Credentials = new AccountCredentials(
                    authToken: data.AuthToken,
                    passwordHash: data.PasswordHash,
                    passwordSalt: data.PasswordSalt),
                Id = data.Id,
                Profile = new AccountProfile(
                    displayUsername: data.DisplayUsername,
                    email: data.Email),
                Notes = notes
            };

            return account;
        }
    }
}