using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsHW3.Interfaces;

namespace WinFormsHW3
{
    class StandartFunction : IFunction
    {
        public double Xstart { get; set; }
        public double Xend { get; set; }
        public int Dpi { get; set; }
        public StandartFunction(double xStart, double xEnd, int dpi)
        {
            Xstart = xStart;
            Xend = xEnd;
            Dpi = dpi;
        }
        public (double[], double[]) GetPoints(Func<double, double> criterion)
        {
            (double[] pointsX, double[] pointsY) pointsXY = (new double[Dpi], new double[Dpi]);
            double step = (Xend - Xstart) / (Dpi-1);
            int i = 0;
            for (double x = Xstart; i < Dpi; x+= step, i++)
            {
                pointsXY.pointsX[i] = x;
                pointsXY.pointsY[i] = criterion(x);
            }
            return pointsXY;
        }
    }
}
