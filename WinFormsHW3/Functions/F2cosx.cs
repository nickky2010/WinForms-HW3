using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsHW3.Interfaces;

namespace WinFormsHW3.Functions
{
    internal class F2cosx : StandartFunction, IFunction
    {
        public F2cosx(double xStart, double xEnd, int dpi) : base(xStart, xEnd, dpi)
        {
        }
        public (double[], double[]) GetPoints(Func<double, double> criterion, int multiply)
        {
            (double[] pointsX, double[] pointsY) pointsXY = (new double[Dpi], new double[Dpi]);
            double step = (Xend - Xstart) / (Dpi - 1);
            int i = 0;
            for (double x = Xstart; i < Dpi; x += step, i++)
            {
                pointsXY.pointsX[i] = x;
                pointsXY.pointsY[i] = multiply*criterion(x);
            }
            return pointsXY;
        }
    }
}
