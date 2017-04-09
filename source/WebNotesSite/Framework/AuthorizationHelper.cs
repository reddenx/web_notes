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

        private static UserAccount AuthenticatedUser;

        public static UserAccount GetUser()
        {
            return AuthenticatedUser;
        }

        public static UserAccount AuthorizeUserFromApiContext(Collection<CookieHeaderValue> cookies)
        {
            var authValue = cookies.FirstOrDefault(cc => cc.Cookies.Any(c => c.Name == AUTH_COOKIE_KEY))?.Cookies.FirstOrDefault(c => c.Name == AUTH_COOKIE_KEY)?.Value;
            var user = AuthorizeUser(HttpContext.Current.Cache, authValue);
            return user;
        }

        public static bool TryAuthorizeApi(string email, string password, out CookieHeaderValue authCookie)
        {
            var repository = new DataRepository(HttpContext.Current.Cache);
            var user = repository.GetUserByEmail(email);
            if (user != null)
            {
                if (user.Credentials.PasswordHash == HashPassword(password, user.Credentials.PasswordSalt))
                {
                    var authToken = user.GenerateNewAuthToken();
                    authCookie = new CookieHeaderValue(AUTH_COOKIE_KEY, authToken.ToString());
                    return true;
                }
            }

            authCookie = null;
            return false;
        }

        private static string HashPassword(string password, string salt)
        {
            var hasher = new System.Security.Cryptography.SHA256Managed();
            var buffer = Encoding.ASCII.GetBytes(password + salt);
            var hashBytes = hasher.ComputeHash(buffer);

            return string.Join("", hashBytes.Select(b => b.ToString("x2")));
        }

        public static UserAccount AuthorizeUserFromContext(Cache cache, HttpRequestBase request)
        {
            var authToken = HttpContext.Current.Request.Cookies[AUTH_COOKIE_KEY]?.Value;
            var user = AuthorizeUser(cache, authToken);
            return user;
        }

        private static UserAccount AuthorizeUser(Cache cache, string authToken)
        {
            var authGuid = Guid.Empty;
            if (Guid.TryParse(authToken, out authGuid))
            {
                var repository = new DataRepository(HttpContext.Current.Cache);
                var user = repository.GetUserByAuthToken(authToken);
                if (user != null)
                {
                    AuthenticatedUser = user;
                    return user;
                }
            }
            return null;
        }
    }
}