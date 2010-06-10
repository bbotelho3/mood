using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    class Geometry
    {
        public static double dot(Vector3d A, Vector3d B, Vector3d C)
        {
            double[] AB = new double[2];
            double[] BC = new double[2];
            AB[0] = B.X - A.X;
            AB[1] = B.Z - A.Z;
            BC[0] = C.X - B.X;
            BC[1] = C.Z - B.Z;
            double dot = AB[0] * BC[0] + AB[1] * BC[1];
            return dot;
        }
        //Compute the cross product AB x AC
        public static double cross(Vector3d A, Vector3d B, Vector3d C)
        {
            double[] AB = new double[2];
            double[] AC = new double[2];
            AB[0] = B.X - A.X;
            AB[1] = B.Z - A.Z;
            AC[0] = C.X - A.X;
            AC[1] = C.Z - A.Z;
            double cross = AB[0] * AC[1] - AB[1] * AC[0];
            return cross;
        }
        //Compute the distance from A to B
        public static double distance(Vector3d A, Vector3d B)
        {
            double d1 = A.X - B.X;
            double d2 = A.Z - B.Z;
            return Math.Sqrt(d1 * d1 + d2 * d2);
        }
        //Compute the distance from AB to C
        //if isSegment is true, AB is a segment, not a line.
        public static double linePointDist(Vector3d A, Vector3d B, Vector3d position, bool isSegment)
        {
            double dist = cross(A, B, position) / distance(A, B);

            if (isSegment)
            {
                double dot1 = dot(A, B, position);
                if (dot1 > 0) return distance(B, position);
                double dot2 = dot(B, A, position);
                if (dot2 > 0) return distance(A, position);
            }

            return Math.Abs(dist);
        }
    }
}
