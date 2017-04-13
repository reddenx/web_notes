using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotesSite.Models.DataTypes;

namespace WebNotesSite.Models.inputs
{
    public class NewStrokeInput
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public float Radius;
        public Vector2[] Path;
    }
}