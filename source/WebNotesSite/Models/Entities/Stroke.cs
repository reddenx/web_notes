using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;

namespace WebNotesSite.Models.Entities
{
    public class Stroke
    {
        public Guid Id { get; private set; }
        public Note Owner { get; private set; }

        public BrushColor Color { get; private set; }
        public BrushSize Size { get; private set; }
        public Vector2[] Path { get; private set; }
    }
}