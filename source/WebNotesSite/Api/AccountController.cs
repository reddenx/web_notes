using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebNotesSite.Framework;
using WebNotesSite.Models.Dtos;
using WebNotesSite.Models.inputs;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Api
{
    [RoutePrefix("json/account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("")]
        public AccountDto GetAccount()
        {
            throw new NotImplementedException();
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