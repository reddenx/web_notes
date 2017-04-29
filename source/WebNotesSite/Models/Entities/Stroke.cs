using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Entities
{
    public class Stroke
    {
        public Guid Id { get; private set; }
        public Note Owner { get; private set; }

        public BrushColor Color { get; private set; }
        public BrushSize Size { get; private set; }
        public Vector2[] Path { get; private set; }

        public Stroke(Note owner, BrushColor color, BrushSize size, Vector2[] path)
        {
            Id = Guid.NewGuid();
            Owner = owner;

            Color = color;
            Size = size;
            Path = path;
        }

        private Stroke() { }

        public StrokeData ToData()
        {
            return new StrokeData()
            {
                Color = Color,
                Id = Id,
                Path = Path,
                Size = Size,
            };
        }

        public static Stroke FromData(StrokeData data, Note note)
        {
            return new Stroke()
            {
                Color = data.Color,
                Id = data.Id,
                Owner = note,
                Path = data.Path,
                Size = data.Size,
            };
        }
    }
}