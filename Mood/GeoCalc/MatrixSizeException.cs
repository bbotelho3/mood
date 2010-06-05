using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood.GeoCalc
{
    public class MatrixSizeException : Exception
    {
        public override string ToString()
        {
            return "Tamanho das matrizes eram diferentes.";
        }
    }
}
