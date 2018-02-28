using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator.Business
{
    public interface IPaymentResult
    {
        decimal MonthlyPayment { get; }
        decimal TotalInterest { get; }
        decimal TotalPayment { get; }
    }
}
