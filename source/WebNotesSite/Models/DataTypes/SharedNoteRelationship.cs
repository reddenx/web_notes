using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.Entities;

namespace WebNotesSite.Models.DataTypes
{
    public class SharedNoteRelationship
    {
        public Note Note { get; private set; }
        public UserAccount SharedWithAccount { get; private set; }

        public bool CanEdit { get; private set; }
        public bool CanShare { get; private set; }
    }
}