using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator.Service
{
    public interface ILoanPaymentCalculatorService
    {
        string GetLoanInfo(IList<string> arguments);
    }
}
