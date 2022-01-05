﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magitui.Services.Interfaces;

namespace Magitui.Services
{
    public class CalculatorService : ICalculatorService
    {
        public float CalculateTotal(IEnumerable<ICalculable> items)
        {
            var total = items.Sum(x => x.Amount);
            Console.WriteLine($"TOTAL:{total}");
            return total;

        }
    }
}
