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
        AccountData GetAccountByRecentToken(string token);
    }

    internal class AccountRepository : IAccountRepository
    {
        private readonly string AccountConnectionString;

        public AccountRepository(IRepositoryConfiguration config)
        {
            AccountConnectionString = config.AccountSqlConnectionString;
        }

        public AccountData GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public AccountData GetAccountByEmail(string email)
        {
            using (var connection = new MySqlConnection(AccountConnectionString))
            {
                var results = connection.Query<AccountTableData>("select * from vw_accounts a where a.Email = @Email",
                    new
                    {
                        Email = email
                    });

                return results?.FirstOrDefault()?.ToAccountData();
            }
        }

        public AccountData GetAccountByRecentToken(string token)
        {
            var bytes = token.ToBase64Binary();
            using (var connection = new MySqlConnection(AccountConnectionString))
            {
                var results = connection.Query<AccountTableData>("select * from vw_accounts where AuthToken = @AuthToken",
                    new
                    {
                        AuthToken = bytes
                    });

                return results?.FirstOrDefault()?.ToAccountData();
            }
        }

        public AccountData GetAccountByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public AccountData[] GetStuff()
        {
            using (var connection = new MySqlConnection(AccountConnectionString))
            {
                var results = connection.Query<AccountTableData>("select * from vw_accounts a where a.Email = @Email");
                return results.Select(tableData => new AccountData()
                {
                }).ToArray();
            }
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

            public AccountData ToAccountData()
            {
                return new AccountData()
                {
                    AccountId = this.AccountId,
                    AuthToken = this.AuthToken.ToBase64(),
                    Email = this.Email,
                    PasswordHash = this.PasswordHash.ToBase64(),
                    PasswordSalt = this.PasswordSalt.ToBase64(),
                };
            }
        }
    }
}