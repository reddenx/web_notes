﻿using System;
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

        public AccountData ToData()
        {
            return new AccountData()
            {
                AuthToken = Credentials.AuthToken,
                DisplayUsername = Profile.DisplayUsername,
                Email = Profile.Email,
                Id = Id,
                NoteIds = Notes.Select(n => n.Id).ToArray(),
                PasswordHash = Credentials.PasswordHash,
                PasswordSalt = Credentials.PasswordSalt
            };
        }

        public Note AddNote(string name)
        {
            var newNote = new Note(name);

#warning switch to eventing list blah blah
            Notes = Notes.Concat(new Note[] { newNote }).ToArray();

            return newNote;
        }

        public void DeleteNote(Guid noteId) { throw new NotImplementedException(); }

        public void ChangePassword(string newPassword) { throw new NotImplementedException(); }

        public void UpdateProfile(AccountProfile newProfile) { throw new NotImplementedException(); }

        public Guid GenerateNewAuthToken()
        {
            var newAuthToken = Guid.NewGuid();
            Credentials = new AccountCredentials(
                passwordHash: Credentials.PasswordHash,
                authToken: newAuthToken.ToString(),
                passwordSalt: Credentials.PasswordSalt);

            return newAuthToken;
        }

    }
}