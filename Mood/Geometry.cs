using System;

namespace Mood
{
    class Geometry
    {
        public static double DotProduct(Point3d A, Point3d B, Point3d C)
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

        // AB x AC.
        public static double CrossProduct(Point3d A, Point3d B, Point3d C)
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

        public static Point3d CrossProduct(Point3d vec1, Point3d vec2)
        {
            return new Point3d((vec1.Y * vec2.Z) - (vec2.Y * vec1.Z),
                                (vec1.Z * vec2.X) - (vec2.Z * vec1.X),
                                (vec1.X * vec2.Y) - (vec2.X * vec1.Y));

        }

        public static float VectorWidth(Point3d v)
        {
            return (float)Math.Abs(Math.Sqrt((v.X * v.X) + (v.Y * v.Y) + (v.Z * v.Z)));
        }

        public static Point3d Normalize(Point3d v)
        {
            float width = VectorWidth(v);

            return new Point3d(v.X / width, v.Y / width, v.Z / width);
        }

        // Distância de A à B.
        public static double VectorDistance(Point3d A, Point3d B)
        {
            double d1 = A.X - B.X;
            double d2 = A.Z - B.Z;
            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        // Distância de AB à C
        // Se isSegment for verdadeiro então é apenas um segmento de reta.
        public static double LinePointDistance(Point3d A, Point3d B, Point3d position, bool isSegment)
        {
            double distance = CrossProduct(A, B, position) / VectorDistance(A, B);

            if (isSegment)
            {
                double dot1 = DotProduct(A, B, position);
                if (dot1 > 0) return VectorDistance(B, position);
                double dot2 = DotProduct(B, A, position);
                if (dot2 > 0) return VectorDistance(A, position);
            }

            return Math.Abs(distance);
        }

        public static double PointDistance(Point3d A, Point3d B)
        {
            double xd = B.X - A.X;
            double yd = B.Y - A.Y;

            return Math.Abs(Math.Sqrt(xd * xd + yd * yd));
        }
    }
}
