using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Caching;
using WebNotesSite.Models.Entities;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Framework
{
    /// <summary>
    /// to be used exclusively at the controller level.
    /// </summary>
    public static class AuthorizationHelper
    {
        private const string AUTH_COOKIE_KEY = "notes_app_auth_token";

        private static UserAccount AuthorizedUser;

        public static UserAccount GetAuthorizedUser()
        {
            return AuthorizedUser;
        }

        public static string GetAuthTokenForCredentials(string email, string password)
        {
            var repository = new DataRepository(HttpContext.Current.Cache);
            var user = repository.GetUserByEmail(email);
            if (user != null)
            {
                if (user.Credentials.PasswordHash == HashPassword(password, user.Credentials.PasswordSalt))
                {
                    return user.GenerateNewAuthToken().ToString();
                }
            }

            return null;
        }

        public static HttpCookie GetAuthCookieForToken(Cache cache, Guid authToken)
        {
            if (AuthorizeUser(cache, authToken.ToString()))
            {
                return new HttpCookie(AUTH_COOKIE_KEY, authToken.ToString())
                {
                    Expires = DateTime.Now.AddMonths(1),
                    Shareable = false,
                };
            }
            return null;
        }

        public static bool AuthorizeUserMvc(Cache cache, HttpRequestBase request)
        {
            var authToken = HttpContext.Current.Request.Cookies[AUTH_COOKIE_KEY]?.Value;
            return AuthorizeUser(cache, authToken);
        }

        public static bool AuthorizeUserWebapi(Collection<CookieHeaderValue> cookies)
        {
            var authValue = cookies.FirstOrDefault(cc => cc.Cookies.Any(c => c.Name == AUTH_COOKIE_KEY))?.Cookies.FirstOrDefault(c => c.Name == AUTH_COOKIE_KEY)?.Value;
            return AuthorizeUser(HttpContext.Current.Cache, authValue);
        }

        public static string HashPassword(string password, string salt)
        {
            var hasher = new System.Security.Cryptography.SHA256Managed();
            var buffer = Encoding.ASCII.GetBytes(password + salt);
            var hashBytes = hasher.ComputeHash(buffer);

            return string.Join("", hashBytes.Select(b => b.ToString("x2")));
        }


        private static bool AuthorizeUser(Cache cache, string authToken)
        {
            var authGuid = Guid.Empty;
            if (Guid.TryParse(authToken, out authGuid))
            {
                var repository = new DataRepository(HttpContext.Current.Cache);
                var user = repository.GetUserByAuthToken(authToken);
                if (user != null)
                {
                    AuthorizedUser = user;
                    return true;
                }
            }
            return false;
        }
    }
}