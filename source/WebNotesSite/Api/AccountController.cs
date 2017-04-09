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

namespace WebNotesSite.Api
{
    [RoutePrefix("json/account")]
    [Authorize]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("")]
        public AccountDto GetAccount()
        {
            var user = AuthorizationHelper.GetUser();
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public HttpResponseMessage Login(LoginInput input)
        {
            CookieHeaderValue authCookie = null;
            if (AuthorizationHelper.TryAuthorizeApi(input.Email, input.Password, out authCookie))
            {
                var response = new HttpResponseMessage();
                response.Headers.AddCookies(new[] { authCookie });
                response.Content = new StringContent("true");
                return response;
            }
            else
            {
                var response = new HttpResponseMessage();
                response.Content = new StringContent("false");
                return response;
            }

        }
    }
}