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

        public LineElement(Note owner, Vector2 start, Vector2 end, BrushColor color, BrushSize size)
        {
            Id = Guid.NewGuid();
            Owner = owner;

            StartPosition = start;
            EndPosition = end;

            Color = color;
            Size = size;
        }

        private LineElement() { }

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

        public LineElementData ToData()
        {
            return new LineElementData()
            {
                Color = Color,
                End = EndPosition,
                Id = Id,
                Size = Size,
                Start = StartPosition
            };
        }
    }
}