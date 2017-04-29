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
        public Guid Id;
        public string Name;
        public string Description;
        
        public NoteDto(NoteData noteData)
        {
            Id = noteData.Id;
            Name = noteData.Name;
            Description = noteData.Description;
        }
    }
}