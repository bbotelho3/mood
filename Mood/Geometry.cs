using System;

namespace Mood
{
    class Geometry
    {
        public static double DotProduct(Vector3d A, Vector3d B, Vector3d C)
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
        public static double CrossProduct(Vector3d A, Vector3d B, Vector3d C)
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

        // Distância de A à B.
        public static double VectorDistance(Vector3d A, Vector3d B)
        {
            double d1 = A.X - B.X;
            double d2 = A.Z - B.Z;
            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        // Distância de AB à C
        // Se isSegment for verdadeiro então é apenas um segmento de reta.
        public static double LinePointDistance(Vector3d A, Vector3d B, Vector3d position, bool isSegment)
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

        public static double PointDistance(Vector3d A, Vector3d B)
        {
            double xd = B.X - A.X;
            double yd = B.Y - A.Y;

            return Math.Abs(Math.Sqrt(xd * xd + yd * yd));
        }
    }
}
