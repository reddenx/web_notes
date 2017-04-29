using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace WebNotesSite.Data
{
    /// <summary>
    /// uh, this complicated mess synchronizes webcache and file data
    /// </summary>
    public static class CachedDataAccess
    {
        public static string StoragePath;

        public static void Initialize(string storagePath)
        {
            StoragePath = storagePath;
        }

        public static T Get<T>(Cache webCache) where T : class
        {
            string json = null;
            lock (CachedDataAccess.StoragePath)
            {
                json = GetFromCache<T>(webCache);
                if(json == null)
                {
                    json = GetFromFile<T>();
                }

                if(json == null)
                {
                    return null;
                }
            }

            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public static void Save<T>(Cache webCache, T data) where T : class
        {
            var json = JsonConvert.SerializeObject(data);
            lock(CachedDataAccess.StoragePath)
            {
                SaveToCache<T>(webCache, json);
                SaveToFile<T>(json);
            }
        }

        private static void SaveToFile<T>(string json) where T : class
        {
            var fileName = GenerateFilename<T>();
            File.WriteAllText(fileName, json);
        }

        private static string GetFromFile<T>() where T : class
        {
            var fileName = GenerateFilename<T>();
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                return json;
            }
            return null;
        }

        private static void SaveToCache<T>(Cache webCache, string json) where T : class
        {
            var key = GenerateKey<T>();
            webCache.Remove(key);
            webCache.Add(key, json, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), CacheItemPriority.High, null);
        }

        private static string GetFromCache<T>(Cache webCache) where T : class
        {
            var entry = webCache.Get(GenerateKey<T>()) as string;
            return entry;
        }





        private static string GenerateFilename<T>() where T : class
        {
            return Path.Combine(CachedDataAccess.StoragePath, $"{GenerateKey<T>()}.json");
        }

        private static string GenerateKey<T>() where T : class
        {
            return $"TypeCache_{typeof(T).FullName}";
        }
    }
}