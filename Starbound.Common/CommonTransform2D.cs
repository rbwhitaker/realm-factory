using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common
{
    public class CommonTransform2D
    {
        private double[,] values;
        private double[,] inverse;

        private CommonTransform2D(double[,] values, double[,] inverse)
        {
            this.values = values;
            this.inverse = inverse;
        }

        public Vector2 TransformVector(Vector2 v)
        {
            double x = v.X * values[0, 0] + v.Y * values[0, 1];
            double y = v.X * values[1, 0] + v.Y * values[1, 1];

            return new Vector2(x, y);
        }

        public Vector2 TransformPoint(Vector2 v)
        {
            double x = v.X * values[0, 0] + v.Y * values[0, 1] + values[0, 2];
            double y = v.X * values[1, 0] + v.Y * values[1, 1] + values[1, 2];

            return new Vector2(x, y);
        }

        public static CommonTransform2D operator *(CommonTransform2D m1, CommonTransform2D m2)
        {
            double[,] values = new double[3, 3];
            double[,] inverseValues = new double[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    double value = 0;
                    double inverse = 0;

                    for (int index = 0; index < 3; index++)
                    {
                        value += m1.values[row, index] * m2.values[index, column];
                        inverse += m2.inverse[row, index] * m1.inverse[index, column];
                    }

                    values[row, column] = value;
                    inverseValues[row, column] = inverse;
                }
            }

            return new CommonTransform2D(values, inverseValues);
        }

        public CommonTransform2D Inverse
        {
            get
            {
                return new CommonTransform2D(
                    new double[3, 3] {
                        { inverse[0, 0], inverse[0, 1], inverse[0, 2] },
                        { inverse[1, 0], inverse[1, 1], inverse[1, 2] },
                        { inverse[2, 0], inverse[2, 1], inverse[2, 2] }},
                    new double[3, 3] {
                        { values[0, 0], values[0, 1], values[0, 2] },
                        { values[1, 0], values[1, 1], values[1, 2] },
                        { values[2, 0], values[2, 1], values[2, 2] }});
            }
        }

        public static CommonTransform2D CreateTranslation(Vector2 translation)
        {
            return new CommonTransform2D(
                new double[3, 3] {
                    { 1, 0, translation.X },
                    { 0, 1, translation.Y },
                    { 0, 0, 1 }},
                new double[3, 3] {
                    { 1, 0, -translation.X },
                    { 0, 1, -translation.Y },
                    { 0, 0, 1 }});
        }

        public static CommonTransform2D CreateScale(Vector2 scale)
        {
            return new CommonTransform2D(
                new double[3, 3] {
                    { scale.X, 0, 0 },
                    { 0, scale.Y, 0 },
                    { 0, 0, 1 }},
                new double[3, 3] {
                    {1 / scale.X, 0, 0 },
                    {0, 1 / scale.Y, 0 },
                    {0, 0, 1 }});
        }

        public static CommonTransform2D CreateScale(double scale)
        {
            return new CommonTransform2D(
                new double[3, 3] {
                    { scale, 0, 0 },
                    { 0, scale, 0 },
                    { 0, 0, 1 }},
                new double[3, 3] {
                    { 1 / scale, 0, 0 },
                    { 0, 1 / scale, 0 },
                    { 0, 0, 1 }});
        }

        public static CommonTransform2D CreateRotation(double angle)
        {
            return new CommonTransform2D(
                new double[3, 3] {
                    { Math.Cos(angle), -Math.Sin(angle), 0 },
                    { Math.Sin(angle), Math.Cos(angle), 0 },
                    { 0, 0, 1 } },
                new double[3, 3] {
                    { Math.Cos(-angle), -Math.Sin(-angle), 0 },
                    { Math.Sin(-angle), Math.Cos(-angle), 0 },
                    { 0, 0, 1 }});
        }

        public static CommonTransform2D Identity = new CommonTransform2D(
            new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } },
            new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
    }
}
