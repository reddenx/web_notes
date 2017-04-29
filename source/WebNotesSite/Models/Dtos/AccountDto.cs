using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Dtos
{
    public class AccountDto
    {
        public string Username;
        public string Email;
        private AccountData accountData;

        public AccountDto(AccountData accountData)
        {
            this.accountData = accountData;
        }
    }
}