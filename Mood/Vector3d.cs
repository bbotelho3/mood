using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{

    public class Vector3d
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3d()
            : this(0, 0, 0)
        {
        }

        public Vector3d(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X + v2.X,
                                v1.Y + v2.Y,
                                v1.Z + v2.Z);
        }
    }
}
