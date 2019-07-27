using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compound_Interest_Calculator
{
    public class CalculatorModel
    {
        public double InitialAmount { get; set; }

        public double InterestRate { get; set; }

        //holds the amount of time for the investment
        public double CalcPeriod { get; set; }

        public double ? MonthlyDeposit { get; set; }

        public double CompoundingTimes { get; set; }
    }
}
