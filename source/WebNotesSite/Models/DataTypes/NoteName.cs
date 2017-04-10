using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.DataTypes
{
    public class NoteName
    {
        public string   Name { get; private set; }
        public string   Description { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateLastModified { get; private set; }
        public DateTime DateLastOpened { get; private set; }

        public NoteName(string name, string description, DateTime dateCreated, DateTime dateLastModified, DateTime dateLastOpened)
        {
            Name = name;
            Description = description;
            DateCreated = dateCreated;
            DateLastModified = dateLastModified;
            DateLastOpened = dateLastOpened;
        }
    }
}