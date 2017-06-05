using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using WebNotes.Persistence;
using WebNotes.Persistence.Repositories;

namespace WebNotesSite.Framework
{
    public class CustomAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private const string AUTHENTICATION_COOKIE_NAME = "note_token";
        public bool AllowMultiple => true;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var request = context?.Request;
            var cookies = request?.Headers.GetCookies().Where(cookie => cookie.HttpOnly == (cookie.Secure && request.RequestUri.Scheme == Uri.UriSchemeHttps)).SelectMany(cookieBag => cookieBag.Cookies);
            var authenticationCookie = cookies?.FirstOrDefault(cookie => cookie.Name == AUTHENTICATION_COOKIE_NAME);
            var token = authenticationCookie?.Value;

            var cacheKey = BuildCacheKey(token);
            var cache = HttpContext.Current.Cache;

            var cachedUser = cache.Get(cacheKey) as CustomIdentity;
            if (token != null)
            {
                if (cachedUser == null)
                {
                    var config = new SiteConfiguration();
                    var accountRepo = PersistenceFactory.GetAccountRepo(config);
                    var user = accountRepo.GetAccountByRecentToken(token);
                    if (user != null)
                    {
                        var customIdentity = new CustomIdentity(user.Username, user.Email, user.AuthToken);
                        context.Principal = new GenericPrincipal(customIdentity, new string[] { });
                        cache.Insert(cacheKey, customIdentity);
                    }
                    else
                    {
                        context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[] { }, context.Request);
                    }
                }
            }

            if (!(context.Principal?.Identity is CustomIdentity))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[] { }, context.Request);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(0);
        }

        private string BuildCacheKey(string token)
        {
            return $"auth_token_{token}";
        }
    }
}