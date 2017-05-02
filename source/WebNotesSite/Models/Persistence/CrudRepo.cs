using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using WebNotesSite.Data;

namespace WebNotesSite.Models.Persistence
{
    public interface ICrudRepoDataIdentifiable
    {
        Guid Id { get; }
    }

    public interface IPersistence<T> where T : ICrudRepoDataIdentifiable
    {
        void Delete(Guid id);
        T Retreive(Guid id);
        T[] RetreiveAll();
        void Save(T item);
    }

    //to do later...
    public class CrudRepo<T> : IPersistence<T> where T : ICrudRepoDataIdentifiable
    {
        //gets by id
        public T Retreive(Guid id)
        {
            var data = RetreiveAll();
            return data.FirstOrDefault(d => d.Id == id);
        }

        //gets all data of type, creates file table if missing
        public T[] RetreiveAll()
        {
            var cache = HttpContext.Current.Cache;
            var allData = CachedDataAccess.Get<T[]>(cache);
            if (allData == null)
            {
                allData = new T[] { };
                CachedDataAccess.Save(cache, allData);
            }
            return allData;
        }

        //updates or creates the new item
        public void Save(T item)
        {
            Delete(item.Id);

            var allData = RetreiveAll();
            var newData = allData.Concat(new T[] { item });
            var cache = HttpContext.Current.Cache;
            CachedDataAccess.Save(cache, newData);
        }

        //removes entry from file table
        public void Delete(Guid id)
        {
            var allData = RetreiveAll();
            var newData = allData.Where(d => d.Id != id).ToArray();

            if (newData.Length != allData.Length)
            {
                var cache = HttpContext.Current.Cache;
                CachedDataAccess.Save(cache, newData);
            }
        }

    }
}