using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.LinearAlgebra
{
    public class MatrixException : Exception
    {
        public MatrixException()
            : base()
        {
        }

        public MatrixException(string message)
            : base(message)
        {
        }
    }
}
