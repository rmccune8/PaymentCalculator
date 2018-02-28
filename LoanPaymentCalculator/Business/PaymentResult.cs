using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator.Business
{
    public class PaymentResult : IPaymentResult
    {
        private readonly decimal m_monthlyPayment = 0m;
        private readonly decimal m_totalInterest = 0m;
        private readonly decimal m_totalPayment = 0m;

        public PaymentResult(decimal monthlyPayment, decimal totalInterest,
            decimal totalPayment)
        {
            m_monthlyPayment = monthlyPayment;
            m_totalInterest = totalInterest;
            m_totalPayment = totalPayment;
        }

        public decimal MonthlyPayment
        {
            get
            {
                return m_monthlyPayment;
            }
        }

        public decimal TotalInterest
        {
            get
            {
                return m_totalInterest;
            }
        }

        public decimal TotalPayment
        {
            get
            {
                return m_totalPayment;
            }
        }
    }
}
