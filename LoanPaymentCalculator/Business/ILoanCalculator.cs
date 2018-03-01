using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator.Business
{
    public interface ILoanCalculator
    {
        /// <summary>
        /// Calculates a monthly payment for loan terms. The result is not rounded.
        /// </summary>
        /// <param name="loanAmount">The loan amount (e.g., 100000).</param>
        /// <param name="interestPercentage">The interest rate expressed in percentage terms. e.g., if 5.5% you would pass 5.5 to this method.</param>
        /// <param name="termInYears">The length of the loan in years. Partial years are allowed if necessary (e.g., 27.5).</param>
        /// <param name="downPayment">Any down payment for the loan (e.g., 20000).</param>
        /// <returns></returns>
        decimal CalculateMonthlyPaymentUnrounded(double loanAmount, double interestPercentage, double termInYears, double downPayment);

        /// <summary>
        /// Calculates a monthly payment for loan terms. The result is rounded to 2 decimal places.
        /// </summary>
        /// <param name="loanAmount">The loan amount (e.g., 100000).</param>
        /// <param name="interestPercentage">The interest rate expressed in percentage terms. e.g., if 5.5% you would pass 5.5 to this method.</param>
        /// <param name="termInYears">The length of the loan in years. Partial years are allowed if necessary (e.g., 27.5).</param>
        /// <param name="downPayment">Any down payment for the loan (e.g., 20000).</param>
        /// <returns></returns>
        decimal CalculateMonthlyPaymentRounded(double loanAmount, double interestPercentage, double termInYears, double downPayment);

        /// <summary>
        /// Calculates the sum of all payments for a loan.
        /// </summary>
        /// <param name="actualLoanAmount">The loan amount, minus any down payment.</param>
        /// <param name="totalInterest">Total interest calculated for the loan.</param>
        /// <returns></returns>
        decimal CalculateTotalPaymentRounded(decimal actualLoanAmount, decimal totalInterest);

        /// <summary>
        /// Calculates the total interest that will need to be paid for loan terms. The result is rounded to 2 decimal places.
        /// </summary>
        /// <param name="loanAmount">The loan amount (e.g., 100000).</param>
        /// <param name="interestPercentage">The interest rate expressed in percentage terms. e.g., if 5.5% you would pass 5.5 to this method.</param>
        /// <param name="termInYears">The length of the loan in years. Partial years are allowed if necessary (e.g., 27.5).</param>
        /// <param name="downPayment">Any down payment for the loan (e.g., 20000).</param>
        /// <returns></returns>
        decimal CalculateTotalInterestRounded(double loanAmount, double interestPercentage, double termInYears, double downPayment);
    }
}
