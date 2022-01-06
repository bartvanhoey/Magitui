using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Services.Calculator
{
    public interface ICalculatorService
    {
        public float CalculateTotal(IEnumerable<ICalculable> items);
    }
}
