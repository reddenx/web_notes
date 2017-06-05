using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.Persistence.DataModels;

namespace WebNotes.Persistence.Repositories
{
    public interface INoteRepository
    {
        NoteNameData[] GetNotesForAccountId(int accountId);
        NoteData[] GetNote(int noteId);
    }
}
