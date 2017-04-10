using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Persistence;

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

        public static LineElement FromData(LineElementData data, Note note)
        {
            return new LineElement()
            {
                Color = data.Color,
                EndPosition = data.End,
                Id = data.Id,
                Owner = note,
                Size = data.Size,
                StartPosition = data.Start,
            };
        }
    }
}