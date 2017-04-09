using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;

namespace WebNotesSite.Models.Entities
{
    public class Note
    {
        public Guid Id { get; private set; }
        public UserAccount Owner { get; private set; }

        public NoteName Name { get; private set; }

        public TextElement[] TextElements { get; private set; }
        public LineElement[] LineElements { get; private set; }
        public Stroke[] Strokes { get; private set; }
    }
}