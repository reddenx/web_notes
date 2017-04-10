using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Entities;

namespace WebNotesSite.Models.Persistence
{
    public class NoteData
    {
        public Guid Id;

        public string Name;
        public string Description;
        public DateTime DateCreated;
        public DateTime DateLastModified;
        public DateTime DateLastOpened;

        public Guid[] TextELementIds;
        public Guid[] LineELementIds;
        public Guid[] StrokeIds;

        public BrushColor BackgroundColor;
    }

    public class TextElementData
    {
        public Guid Id;
        public Vector2 Position;
        public int FontSize;
        public BrushColor Color;
    }

    public class LineElementData
    {
        public Guid Id;
        public Vector2 Start;
        public Vector2 End;
        public BrushColor Color;
        public BrushSize Size;
    }

    public class StrokeData
    {
        public Guid Id;
        public BrushColor Color;
        public BrushSize Size;
        public Vector2[] Path;
    }
}