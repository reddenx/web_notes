using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;

namespace WebNotesSite.Models.Entities
{
    public class LineElement
    {
        public Guid Id { get; private set; }
        public Note Owner { get; private set; }

        public Vector2 StartPosition { get; private set; }
        public Vector2 EndPosition { get; private set; }

        public BrushColor Color { get; private set; }
        public BrushSize Size { get; private set; }
    }
}