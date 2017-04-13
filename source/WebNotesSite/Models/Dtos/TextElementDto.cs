using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Dtos
{
    public class TextElementDto
    {
        private TextElementData textElementData;

        public TextElementDto(TextElementData textElementData)
        {
            this.textElementData = textElementData;
        }
    }
}