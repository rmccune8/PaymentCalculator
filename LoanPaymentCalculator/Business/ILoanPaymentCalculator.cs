using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator.Business
{
    public interface ILoanPaymentCalculator
    {
        IPaymentResult Calculate(decimal amount, decimal interest, decimal downPayment, decimal term);

            // input amount, interest (percentage or digit), downpayment, term, last line blank
            // output json of monthly pmt, tot interest, tot payment
    }
}
