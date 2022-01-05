using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Services.Interfaces
{
    public interface ICalculatorService
    {
        public float CalculateTotal(IEnumerable<ICalculable> items);
    }

    public interface ICalculable
    {
        public float Amount { get; set; }
        public string Currency { get; set; }
    }
}
