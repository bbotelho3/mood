using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood.GeoCalc
{
    public class Matrix
    {
        #region Properties

        private double[,] matrix;

        #endregion Properties

        #region Constructors

        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix(int lines, int columns)
        {
            this.matrix = new double[lines, columns];
        }

        #endregion Constructors

        #region Private Static Methods

        private static Matrix Add(double[,] matrix1, double[,] matrix2)
        {
            double[,] sum = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

            if ((matrix1.GetLength(0) != matrix2.GetLength(0)) || (matrix1.GetLength(1) != matrix2.GetLength(1)))
            {
                throw new MatrixSizeException();
            }

            for (int i = 0; i < sum.GetLength(0); i++)
            {
                for (int j = 0; j < sum.GetLength(1); j++)
                {
                    sum[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return new Matrix(sum);
        }

        private static Matrix Subtract(double[,] matrix1, double[,] matrix2)
        {
            double[,] subtraction = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

            if ((matrix1.GetLength(0) != matrix2.GetLength(0)) || (matrix1.GetLength(1) != matrix2.GetLength(1)))
            {
                throw new MatrixSizeException();
            }

            for (int i = 0; i < subtraction.GetLength(0); i++)
            {
                for (int j = 0; j < subtraction.GetLength(1); j++)
                {
                    subtraction[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return new Matrix(subtraction);
        }

        private static Matrix Multiply(double[,] matrix1, double[,] matrix2)
        {
            double[,] multiplication = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    multiplication[i, j] = 0;
                    for (int k = 0; k < matrix1.GetLength(1); k++)
                    {
                        multiplication[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return new Matrix(multiplication);
        }

        private static Matrix Divide(double[,] matrix1, double[,] matrix2)
        {
            double[,] divided = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

            for (int i = 0; i < divided.GetLength(0); i++)
            {
                for (int j = 0; j < divided.GetLength(1); j++)
                {
                    divided[i, j] = matrix1[i, j] / matrix2[i, j];
                }
            }

            return new Matrix(divided);
        }

        #endregion Private Static Methods

        #region Public Operators

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return Matrix.Add(matrix1.GetMatrix(), matrix2.GetMatrix());
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return Matrix.Subtract(matrix1.GetMatrix(), matrix2.GetMatrix());
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return Matrix.Multiply(matrix1.GetMatrix(), matrix2.GetMatrix());
        }

        public static Matrix operator /(Matrix matrix1, Matrix matrix2)
        {
            return Matrix.Divide(matrix1.GetMatrix(), matrix2.GetMatrix());
        }

        #endregion Public Operators

        #region Public Methods

        public double[,] GetMatrix()
        {
            return this.matrix;
        }

        #endregion Public Methods
    }
}
