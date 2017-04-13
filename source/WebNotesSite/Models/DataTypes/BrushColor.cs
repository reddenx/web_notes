using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Models.DataTypes
{
    public class BrushColor
    {
        public float Red { get; private set; }
        public float Green { get; private set; }
        public float Blue { get; private set; }
        public float Alpha { get; private set; }

        public BrushColor(float r, float g, float b, float a)
        {
            Red = r;
            Green = g;
            Blue = b;
            Alpha = a;
        }
    }
}