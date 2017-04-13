using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.Entities;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Dtos
{
    public class NoteDto
    {
        private NoteData noteData;

        public NoteDto(NoteData noteData)
        {
            this.noteData = noteData;
        }
    }
}