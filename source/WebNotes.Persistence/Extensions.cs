using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotes.Persistence
{
    public static class Extensions
    {
        public static string ToBase64(this byte[] data)
        {
            return System.Text.Encoding.UTF8.GetString(data);
        }

        public static byte[] ToBase64Binary(this string data)
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }
    }
}