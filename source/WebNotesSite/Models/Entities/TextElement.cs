using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;

namespace WebNotesSite.Models.Entities
{
    public class TextElement
    {
        public Guid Id { get; private set; }
        public Note Owner { get; private set; }

        public Vector2 Position { get; private set; }
        public int FontSize { get; private set; }
        public BrushColor Color { get; private set; }
    }
}