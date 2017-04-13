using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public static class EventExtensions
    {
        public static void SafeInvoke(this EventHandler e, object sender)
        {
            e?.Invoke(sender, null);
        }

        public static void SafeInvoke<T>(EventHandler<T> e, T data, object sender)
        {
            e?.Invoke(sender, data);
        }

        public static void ReleaseSubscribers(this EventHandler e)
        {
            var subscribers = e.GetInvocationList();
            foreach(EventHandler subscriber in subscribers)
            {
                e -= subscriber;
            }
        }

        public static void ReleaseSubscribers<T>(this EventHandler<T> e)
        {
            var subscribers = e.GetInvocationList();
            foreach (EventHandler<T> subscriber in subscribers)
            {
                e -= subscriber;
            }
        }
    }
}