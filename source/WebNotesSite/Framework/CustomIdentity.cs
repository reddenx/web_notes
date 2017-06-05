using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebNotesSite.Framework
{
    public class CustomIdentity : IIdentity
    {
        public string AuthenticationType => "Custom";
        public bool IsAuthenticated => true;
        public string Name => Username;

        public readonly string Username;
        public readonly string Email;
        public readonly string Token;

        public CustomIdentity(string username, string email, string token)
        {
            Username = username;
            Email = email;
            Token = token;
        }
    }
}