using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using WebNotesSite.Data;
using WebNotesSite.Framework;
using WebNotesSite.Models.Entities;

namespace WebNotesSite.Models.Persistence
{
    public class DataRepository
    {
        private readonly Cache WebCache;

        public DataRepository(Cache cache)
        {
            WebCache = cache;
        }

        public Note GetNoteById(Guid noteId)
        {
            var myNoteData = GetData<NoteData>()?.FirstOrDefault(n => n.Id == noteId);
            if (myNoteData != null)
            {
                var lines = GetData<LineElementData>().Where(l => myNoteData.LineELementIds.Contains(l.Id));
                var texts = GetData<TextElementData>().Where(t => myNoteData.TextELementIds.Contains(t.Id));
                var strokes = GetData<StrokeData>().Where(s => myNoteData.StrokeIds.Contains(s.Id));

                var note = Note.FromData(myNoteData, lines.ToArray(), texts.ToArray(), strokes.ToArray());
                return note;
            }
            return null;
        }

        public void SaveNote(Note note)
        {
            var data = note.ToData();
            var allNotes = GetData<NoteData>();
            var newAllNotes = allNotes.Where(n => n.Id != data.Id).Concat(new[] { data });
            CachedDataAccess.Save(WebCache, newAllNotes.ToArray());
        }

        public UserAccount GetUserByAuthToken(string authToken)
        {
            var allAccounts = GetData<AccountData>();
            var account = allAccounts.FirstOrDefault(a => a.AuthToken == authToken);
            if (account != null)
            {
                return GetAccountById(account.Id);
            }

            return null;
        }

        public UserAccount GetAccountById(Guid accountId)
        {
            var account = GetData<AccountData>()?.FirstOrDefault(a => a.Id == accountId);
            if (account != null)
            {
                var notes = account.NoteIds.Select(n => GetNoteById(n));

                return UserAccount.FromData(account, notes.ToArray());
            }
            return null;
        }

        public void SaveAccount(UserAccount account)
        {
            var data = account.ToData();
            var allData = GetData<AccountData>();
            var newDataList = allData.Where(d => d.Id != data.Id).Concat(new[] { data }).ToArray();
            CachedDataAccess.Save(WebCache, newDataList);
        }

        public UserAccount GetUserByEmail(string email)
        {
            var allAccounts = GetData<AccountData>();
            var account = allAccounts.FirstOrDefault(a => a.Email == email);
            if (account != null)
            {
                return GetAccountById(account.Id);
            }

            return null;
        }

        public UserAccount CreateNewUser(string email, string password)
        {
            var salt = Guid.NewGuid().ToString();
            var accountData = new AccountData()
            {
                Email = email,
                PasswordHash = AuthorizationHelper.HashPassword(password, salt),
                PasswordSalt = salt,
                AuthToken = null,
                DisplayUsername = string.Empty,
                Id = Guid.NewGuid(),
                NoteIds = new Guid[] { }
            };

            var allAccounts = GetData<AccountData>().Concat(new[] { accountData }).ToArray();
            CachedDataAccess.Save(WebCache, allAccounts);

            return GetAccountById(accountData.Id);
        }

        private T[] GetData<T>() where T : class
        {
            var allData = CachedDataAccess.Get<T[]>(WebCache);
            if (allData == null)
            {
                allData = new T[] { };
                CachedDataAccess.Save(WebCache, allData);
            }
            return allData;
        }
    }
}