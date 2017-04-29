using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Entities
{
    public class Note
    {
        public event EventHandler OnChanged;

        public Guid Id { get; private set; }

        public NoteName Name { get; private set; }

        public TextElement[] TextElements { get; private set; }
        public LineElement[] LineElements { get; private set; }

        public Stroke[] Strokes { get; private set; }

        public BrushColor BackgroundColor { get; private set; }

        public Note(string name)
        {
            Id = Guid.NewGuid();
            Name = new NoteName(name, string.Empty, DateTime.Now, DateTime.Now, DateTime.Now);
            TextElements = new TextElement[] { };
            LineElements = new LineElement[] { };
            Strokes = new Stroke[] { };
            BackgroundColor = new BrushColor(1, 1, 1, 1);
        }

        private Note() { }

        public static Note FromData(NoteData data, LineElementData[] lines, TextElementData[] texts, StrokeData[] strokes)
        {
            var note = new Note()
            {
                BackgroundColor = data.BackgroundColor,
                Id = data.Id,
                Name = new NoteName(
                    name: data.Name,
                    dateCreated: data.DateCreated,
                    dateLastModified: data.DateLastModified,
                    dateLastOpened: data.DateLastOpened,
                    description: data.Description),
            };

            note.LineElements = lines.Select(l => LineElement.FromData(l, note)).ToArray();
            note.TextElements = texts.Select(t => TextElement.FromData(t, note)).ToArray();
            note.Strokes = strokes.Select(s => Stroke.FromData(s, note)).ToArray();

            return note;
        }

        public NoteData ToData()
        {
            return new NoteData()
            {
                BackgroundColor = BackgroundColor,
                DateCreated = Name.DateCreated,
                Name = Name.Name,
                DateLastModified = Name.DateLastModified,
                DateLastOpened = Name.DateLastOpened,
                Description = Name.Description,
                Id = Id,
                LineELementIds = LineElements.Select(l => l.Id).ToArray(),
                StrokeIds = Strokes.Select(s => s.Id).ToArray(),
                TextELementIds = TextElements.Select(t => t.Id).ToArray()
            };
        }

        public void Rename(string newName) { }

        public void SetBackgroundColor(BrushColor newColor)
        {
            if (BackgroundColor != newColor)
            {
                BackgroundColor = newColor;
                OnChanged.SafeInvoke(this);
            }
        }

        public TextElement AddText(string text, int size, BrushColor color) { throw new NotImplementedException(); }
        public void RemoveText(Guid textId) { }

        public LineElement AddLine(Vector2 start, Vector2 end, BrushSize size, BrushColor color) { throw new NotImplementedException(); }
        internal void RemoveLine(Guid lineElementId) { throw new NotImplementedException(); }

        public Stroke AddStroke(BrushColor color, BrushSize size, Vector2[] path) { throw new NotImplementedException(); }
        public void RemoveStroke(Guid strokeId) { }
    }
}