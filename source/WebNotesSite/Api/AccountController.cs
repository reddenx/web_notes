using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebNotes.Persistence;
using WebNotes.Persistence.Repositories;
using WebNotesSite.Framework;
using WebNotesSite.Models.Dtos;
using WebNotesSite.Models.inputs;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Api
{
    [RoutePrefix("json/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountRepository AccountRepo;

        public AccountController()
        {
            AccountRepo = PersistenceFactory.GetAccountRepo(new SiteConfiguration());
        }

        [HttpGet]
        [Route("test")]
        [AllowAnonymous]
        public HttpResponseMessage Test(string email)
        {
            var user = AccountRepo.GetAccountByEmail(email);

            var cookie = new CookieHeaderValue("note_token", user.AuthToken)
            {
                Expires = DateTimeOffset.Now.AddMinutes(2),
            };

            var currentPrincipal = this.RequestContext.Principal;

            var result = new HttpResponseMessage();
            result.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            return result;
        }

        [HttpGet]
        [Route("")]
        public AccountDto GetAccount()
        {
            var user = AuthorizationHelper.GetAuthorizedUser();
            return new AccountDto(user.ToData());
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public string GetAuthToken(LoginInput input)
        {
            var token = AuthorizationHelper.GetAuthTokenForCredentials(input.Email, input.Password);
            return token;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public bool CreateUser(RegisterInput input)
        {
            var repo = new DataRepository(HttpContext.Current.Cache);
            
            //existing user check
            var user = repo.GetUserByEmail(input.Email);
            if (user == null)
            {
                user = repo.CreateNewUser(input.Email, input.Password);
                return user != null;
            }

            return false;
        }
    }
}