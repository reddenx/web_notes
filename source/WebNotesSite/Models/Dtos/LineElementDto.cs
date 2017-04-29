using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Dtos
{
    public class LineElementDto
    {
        private LineElementData lineElementData;

        public LineElementDto(LineElementData lineElementData)
        {
            this.lineElementData = lineElementData;
        }
    }
}