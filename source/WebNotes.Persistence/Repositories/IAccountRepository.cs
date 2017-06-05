using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.Persistence.DataModels;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebNotes.Persistence.Repositories
{
    public interface IAccountRepository
    {
        AccountData GetAccount(int accountId);
        AccountData GetAccountByEmail(string email);
        AccountData GetAccountByUsername(string username);
    }

    public class ITestAccountRepository
    {
        public void GetStuff()
        {
            var connection = new MySqlConnection("Server=localhost;Port=3306;Database=accounts;Uid=note_website;Pwd=9b1166a2a3204a3b83400042188ba20f;");
            var results = connection.Query<AccountTableData>("select * from vw_accounts");
        }

        private class AccountTableData
        {
            public int AccountId;
            public string Username;
            public string Email;
            public byte[] PasswordHash;
            public byte[] PasswordSalt;
            public byte[] AuthToken;
            public DateTime TokenExpiration;
        }
    }
}