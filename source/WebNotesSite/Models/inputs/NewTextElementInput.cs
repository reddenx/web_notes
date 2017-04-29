using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;

namespace WebNotesSite.Models.inputs
{
    public class NewTextElementInput
    {
        public float R;
        public float G;
        public float B;
        public float A;
        public int FontSize;
        public string Text;
        public Vector2 Position;
    }
}