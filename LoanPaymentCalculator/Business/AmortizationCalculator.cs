using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator.Business
{
    public class AmortizationCalculator : ILoanCalculator
    {
        private const double MONTHLY_PAYMENTS_PER_YEAR = 12d;

        /// <summary>
        /// Calculates an amortized monthly payment for loan terms. The result is not rounded.
        /// </summary>
        /// <param name="loanAmount">The loan amount (e.g., 100000).</param>
        /// <param name="interestPercentage">The interest rate expressed in percentage terms. e.g., if 5.5% you would pass 5.5 to this method.</param>
        /// <param name="termInYears">The length of the loan in years. Partial years are allowed if necessary (e.g., 27.5).</param>
        /// <param name="downPayment">Any down payment for the loan (e.g., 20000).</param>
        /// <returns></returns>
        public decimal CalculateMonthlyPaymentUnrounded(double loanAmount, double interestPercentage,
            double termInYears, double downPayment = 0d)
        {
            // Payment (P) = Amount (A) / Discount Factor (D)
            // n = Payments per year * number of years
            // i = Annual rate / # of payment periods
            // D = {[(1 + i) ^n] - 1} / [i(1 + i)^n]

            /*
                ex.: n = 360 (30 years times 12 monthly payments per year)
                     i = .005 (6 percent annually expressed as .06, divided by 12 monthly payments per year)
                     D = 166.7916 ({[(1+.005)^360] - 1} / [.005(1+.005)^360])   5.02258 / 0.03011
                     P = A / D = 100,000 / 166.7916 = 599.55
                  
            */

            double A = loanAmount - downPayment;
            double n = termInYears * MONTHLY_PAYMENTS_PER_YEAR;
            double annualRate = interestPercentage / 100d;
            double i = annualRate / MONTHLY_PAYMENTS_PER_YEAR;
            double D = (Math.Pow((1d + i), n) - 1d) / (i * Math.Pow(1d + i, n));

            double P = A / D;
            return (decimal)P;
        }

        /// <summary>
        /// Calculates a monthly payment for loan terms. The result is rounded to 2 decimal places.
        /// </summary>
        /// <param name="loanAmount">The loan amount (e.g., 100000).</param>
        /// <param name="interestPercentage">The interest rate expressed in percentage terms. e.g., if 5.5% you would pass 5.5 to this method.</param>
        /// <param name="termInYears">The length of the loan in years. Partial years are allowed if necessary (e.g., 27.5).</param>
        /// <param name="downPayment">Any down payment for the loan (e.g., 20000).</param>
        /// <returns></returns>
        public decimal CalculateMonthlyPaymentRounded(double loanAmount, double interestPercentage, 
            double termInYears, double downPayment = 0d)
        {
            decimal monthlyPayment = CalculateMonthlyPaymentUnrounded(loanAmount, interestPercentage, termInYears, downPayment);
            return Math.Round(monthlyPayment, 2);
        }

        /// <summary>
        /// Calculates the total interest that will need to be paid for loan terms. The result is rounded to 2 decimal places.
        /// </summary>
        /// <param name="loanAmount">The loan amount (e.g., 100000).</param>
        /// <param name="interestPercentage">The interest rate expressed in percentage terms. e.g., if 5.5% you would pass 5.5 to this method.</param>
        /// <param name="termInYears">The length of the loan in years. Partial years are allowed if necessary (e.g., 27.5).</param>
        /// <param name="downPayment">Any down payment for the loan (e.g., 20000).</param>
        /// <returns></returns>
        public decimal CalculateTotalInterestRounded(double loanAmount, double interestPercentage, double termInYears, double downPayment)
        {
            decimal monthlyPayment = CalculateMonthlyPaymentUnrounded(loanAmount, interestPercentage, termInYears, downPayment);
            double actualLoanAmount = loanAmount - downPayment;
            decimal interest = ((decimal)MONTHLY_PAYMENTS_PER_YEAR * monthlyPayment * (decimal)termInYears) - (decimal)actualLoanAmount;
            return Math.Round(interest, 2);
        }

        /// <summary>
        /// Calculates the sum of all payments for a loan.
        /// </summary>
        /// <param name="actualLoanAmount">The loan amount, minus any down payment.</param>
        /// <param name="totalInterest">Total interest calculated for the loan.</param>
        /// <returns></returns>
        public decimal CalculateTotalPaymentRounded(decimal actualLoanAmount, decimal totalInterest)
        {
            return actualLoanAmount + totalInterest;
        }
    }
}
