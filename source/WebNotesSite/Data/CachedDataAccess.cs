using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace WebNotesSite.Data
{
    public static class CachedDataAccess
    {
        public static string StoragePath;

        public static void Initialize(string storagePath)
        {
            StoragePath = storagePath;
        }
    }

    public static class CachedDataAccess<T> where T : class, new()
    {
        public static T Get(Cache webCache)
        {
            string json = null;
            lock (CachedDataAccess.StoragePath)
            {
                json = GetFromCache(webCache);
                if(json == null)
                {
                    json = GetFromFile();
                }

                if(json == null)
                {
                    var newData = new T();
                    json = JsonConvert.SerializeObject(newData);
                    SaveToFile(json);
                    SaveToCache(webCache, json);
                }
            }

            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public static void Save(Cache webCache, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            lock(CachedDataAccess.StoragePath)
            {
                SaveToCache(webCache, json);
                SaveToFile(json);
            }
        }





        private static void SaveToFile(string json)
        {
            var fileName = GenerateFilename();
            File.WriteAllText(fileName, json);
        }

        private static string GetFromFile()
        {
            var fileName = GenerateFilename();
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                return json;
            }
            return null;
        }

        private static void SaveToCache(Cache webCache, string json)
        {
            var key = GenerateKey();
            webCache.Remove(key);
            webCache.Add(key, json, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), CacheItemPriority.High, null);
        }

        private static string GetFromCache(Cache webCache)
        {
            var entry = webCache.Get(GenerateKey()) as string;
            return entry;
        }





        private static string GenerateFilename()
        {
            return Path.Combine(CachedDataAccess.StoragePath, $"{GenerateKey()}.json");
        }

        private static string GenerateKey()
        {
            return $"TypeCache_{typeof(T).FullName}";
        }
    }
}