using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.Persistence
{
    public class NoteDataContainer
    {
        public NoteData[] AllNotes;
    }

    public class NoteData
    {
        public Guid Id;
        public string Name;
    }
}