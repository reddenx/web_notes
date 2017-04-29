using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Dtos
{
    public class StrokeDto
    {
        public Guid Id;
        public Vector2[] Path;
        public BrushColor Brush;
        public BrushSize Size;

        public StrokeDto(StrokeData strokeData)
        {
            Id = strokeData.Id;
            Path = strokeData.Path;
            Brush = strokeData.Color;
            Size = strokeData.Size;
        }
    }
}