using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mood.GeoCalc;

namespace Mood
{
    public class Vector3d
    {
        #region fields

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Public Methods

        public double[,] ToArray()
        {
            return new double[,] { { this.X, this.Y, this.Z, 1 } };
        }

        public Matrix toMatrix()
        {
            return new Matrix(this.ToArray());
        }

        public override string ToString()
        {
            return String.Format("X: {0}, Y: {1}, Z: {2}", this.X, this.Y, this.Z);
        }

        public double DotProduct(Vector3d v)
        {
            return (X * Y * Z) + (v.X * v.Y * v.Z);
        }

        #endregion Public Methods

        #region Public Static Methods

        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X - v2.X,
                    v1.Y - v2.Y,
                    v1.Z - v2.Z);
        }

        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X + v2.X,
                                v1.Y + v2.Y,
                                v1.Z + v2.Z);
        }

        public static Vector3d Convert(Matrix m)
        {
            double[,] matrix = m.GetMatrix();
            if (matrix.GetLength(0) != 1 || matrix.GetLength(1) < 3)
            {
                throw new MatrixSizeException();
            }

            return new Vector3d((float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2]);
        }

        #endregion Public Static Methods
    }
}
