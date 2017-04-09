using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using WebNotesSite.Data;
using WebNotesSite.Models.Entities;

namespace WebNotesSite.Models.Persistence
{
    public class DataRepository
    {
        private readonly Cache WebCache;

        public DataRepository(Cache cache)
        {
            WebCache = cache;
        }

        public Note GetById(Guid noteId)
        {
            var data = CachedDataAccess<NoteDataContainer>.Get(WebCache);
            
            //TODO
            return new Note();
        }

        public void SaveNote(Note note)
        {
            //TODO
            var data = new NoteData() { Id = Guid.NewGuid(), Name = "test" };
            CachedDataAccess<NoteData>.Save(WebCache, data);
        }
    }
}