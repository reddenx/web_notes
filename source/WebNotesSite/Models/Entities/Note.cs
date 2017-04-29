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
                TextELementIds = TextElements.Select(t => t.Id).ToArray(),
            };
        }

        public void Rename(string newName)
        {
            if (Name.Name != newName && !string.IsNullOrWhiteSpace(newName))
            {
                Name = new NoteName(newName, Name.Description, Name.DateCreated, DateTime.Now, Name.DateLastOpened);
                OnChanged.SafeInvoke(this);
            }
        }

        public void SetBackgroundColor(BrushColor newColor)
        {
            if (BackgroundColor != newColor)
            {
                BackgroundColor = newColor;
                OnChanged.SafeInvoke(this);
            }
        }

        public TextElement AddText(string text, int size, BrushColor color, Vector2 position)
        {
            if (!string.IsNullOrWhiteSpace(text) && size > 0 && color != null)
            {
                var element = new TextElement(this, position, size, color);
#warning change concat addition to lists eventually
                TextElements = TextElements.Concat(new TextElement[] { element }).ToArray();
                OnChanged.SafeInvoke(this);
                return element;
            }
            else
            {
                throw new ArgumentException("AddText input arguments not valid");
            }
        }

        public void RemoveText(Guid textId)
        {
            throw new NotImplementedException();
        }

        public LineElement AddLine(Vector2 start, Vector2 end, BrushSize size, BrushColor color)
        {
            if (start != null && end != null && size != null && color != null)
            {
                var line = new LineElement(this, start, end, color, size);
#warning change concat addition to lists eventually
                LineElements = LineElements.Concat(new LineElement[] { line }).ToArray();
                OnChanged.SafeInvoke(this);
                return line;
            }
            else
            {
                throw new ArgumentException("AddLine input arguments not valid");
            }
        }

        public void RemoveLine(Guid lineElementId)
        {
            throw new NotImplementedException();
        }

        public Stroke AddStroke(BrushColor color, BrushSize size, Vector2[] path)
        {
            if (color != null && size != null && path != null && path.Length > 1)
            {
                var stroke = new Stroke(this, color, size, path);
#warning change concat addition to lists eventually
                Strokes = Strokes.Concat(new Stroke[] { stroke }).ToArray();
                OnChanged.SafeInvoke(this);
                return stroke;
            }
            else
            {
                throw new ArgumentException("AddStroke input arguments are not valid");
            }
        }

        public void RemoveStroke(Guid strokeId) { throw new NotImplementedException(); }
    }
}