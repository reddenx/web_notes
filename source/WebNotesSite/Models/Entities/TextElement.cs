using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Entities
{
    public class TextElement
    {
        public Guid Id { get; private set; }
        public Note Owner { get; private set; }

        public Vector2 Position { get; private set; }
        public int FontSize { get; private set; }
        public BrushColor Color { get; private set; }

        public static TextElement FromData(TextElementData data, Note note)
        {
            return new TextElement()
            {
                Color = data.Color,
                FontSize = data.FontSize,
                Id = data.Id,
                Owner = note,
                Position = data.Position
            };
        }

        public TextElementData ToData()
        {
            return new TextElementData()
            {
                Color = Color,
                FontSize = FontSize,
                Id = Id,
                Position = Position
            };
        }
    }
}