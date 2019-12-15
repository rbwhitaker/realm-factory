using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.LinearAlgebra
{
    public class Matrix
    {
        private double[,] values;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public Matrix(int rows, int columns)
        {
            values = new double[rows, columns];
            Rows = rows;
            Columns = columns;
        }

        public double this[int row, int column]
        {
            get
            {
                return values[row, column];
            }
            set
            {
                values[row, column] = value;
            }
        }

        public Matrix Transpose()
        {
            Matrix transposed = new Matrix(Columns, Rows);

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    transposed[column, row] = this[row, column];
                }
            }
            
            return transposed;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows) { throw new MatrixException("incompatible matrix multiplication"); }

            Matrix result = new Matrix(m1.Rows, m2.Columns);

            for (int row = 0; row < result.Rows; row++)
            {
                for (int column = 0; column < result.Columns; column++)
                {
                    double sum = 0;

                    for (int index = 0; index < m1.Columns; index++)
                    {
                        sum += m1[row, index] * m2[index, column];
                    }

                    result[row, column] = sum;
                }
            }

            return result;
        }

        /// <summary>
        /// THIS METHOD HAS NOT BEEN TESTED.  IT LIKELY HAS PROBLEMS.
        /// </summary>
        /// <returns></returns>
        public double[] CalculateEigenValues()
        {
            if (Rows != Columns) { throw new MatrixException("cannot calculate eigenvalues for a non-square matrix"); }
            if (Rows != 3) { throw new MatrixException("this method corrently only knows how to calculate eigenvalues for 3x3 matrices."); }

            double a = this[0, 0];
            double b = this[0, 1];
            double c = this[0, 2];
            double d = this[1, 0];
            double e = this[1, 1];
            double f = this[1, 2];
            double g = this[2, 0];
            double h = this[2, 1];
            double i = this[2, 2];

            double A = 1;
            double B = (-i - e - a);
            double C = (a * e + a * i - b * d - c * g + e * i - f * h);
            double D = (-a * e * i + a * f * h + b * d * i - b * f * g - c * d * h + c * e * g);

            double x = (3 * C / A) - (B * B / A * A) / 3;
            double y = (2 * B * B * B / A * A * A - 9 * B * C / A * A + 27D / A) / 27;
            double z = y * y / 4 + x * x * x / 27;

            //Define I, j, k, m, n, p (so equations are not so cluttered)
            double I = Math.Sqrt(y * y / 4 - z);
            double J = Math.Pow(-I, 1/3f);
            double K = Math.Acos(-(y / 2 * I));
            double M = Math.Cos(K / 3);
            double N = Math.Sqrt(3) * Math.Sin(K / 3);
            double P = B / (3 * A);

            // Determine eigenvalues
            double Eigenvalue1 = 2 * J * M + P;
            double Eigenvalue2 = -J * (M + N) + P;
            double Eigenvalue3 = -J * (M - N) + P;

            return new double[3] { Eigenvalue1, Eigenvalue2, Eigenvalue3 };
        }
    }
}
