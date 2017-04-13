using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Models.Dtos
{
    public class StrokeDto
    {
        private StrokeData strokeData;

        public StrokeDto(StrokeData strokeData)
        {
            this.strokeData = strokeData;
        }
    }
}