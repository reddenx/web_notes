using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.DataTypes
{
    public class NoteName
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateLastModified { get; private set; }
        public DateTime DateLastOpened { get; private set; }
    }
}