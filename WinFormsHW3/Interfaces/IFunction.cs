using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsHW3.Interfaces
{
    public interface IFunction
    {
        double Xstart { get; set; }
        double Xend { get; set; }
        int Dpi { get; set; }
        (double[], double[]) GetPoints(Func<double, double> criterion);
    }
}
