﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compound_Interest_Calculator
{
    class Compounding
    {
        public double GetEndingBalance (CalculatorModel calc)
        {
            double P;
            if (calc.MonthlyDeposit != null)
            {
                P = calc.InitialAmount + (double)calc.MonthlyDeposit * 12;
            }
            else
            {
                P = calc.InitialAmount;
            }
            double r = calc.InterestRate / 100;
            double n = calc.CompoundingTimes;
            double t = calc.CalcPeriod;

            double balance = P * Math.Pow((1 + (r / n)), n*t);

            balance = Math.Round(balance, 2);

            return balance;
        }


        public double GetEndingBalanceWithMonthly(CalculatorModel calc)
        {
            double P = calc.InitialAmount;
            
            double r = calc.InterestRate / 100;
            double n = calc.CompoundingTimes;
            double t = calc.CalcPeriod;
            double PMT = (double)calc.MonthlyDeposit;
            double depTimes = 12 / n;

            double balance = P * Math.Pow((1 + (r / n)), n * t) + (PMT * depTimes * ((Math.Pow(1 + r/n, n * t) - 1)/ (r / n) ));

            balance = Math.Round(balance, 2);

            return balance;
        }
    }
}
