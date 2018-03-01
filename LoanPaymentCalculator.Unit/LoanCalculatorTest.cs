using System;
using NUnit.Framework;
using LoanPaymentCalculator.Business;

namespace LoanPaymentCalculator.Unit
{
    [TestFixture]
    public class LoanCalculatorTest
    {
        [TestCase]
        public void TestCalculateAmortizedMonthlyPayment()
        {
            // input
            const double LOAN_AMOUNT = 100000d;
            const double TERM = 30d;
            const double INTEREST = 5.5d;
            const double DOWN_PAYMENT = 20000d;

            // ouput
            const decimal MONTHLY_PAYMENT_RESULT = 454.23m;

            // perform calculation
            decimal payment = GetAmortizationCalculator().CalculateMonthlyPaymentRounded(LOAN_AMOUNT, INTEREST, TERM, DOWN_PAYMENT);

            // verify results
            Assert.That(payment == MONTHLY_PAYMENT_RESULT);
        }

        [TestCase]
        public void TestCalculateAmortizedTotalInterest()
        {
            // input
            const double LOAN_AMOUNT = 100000d;
            const double TERM = 30d;
            const double INTEREST = 5.5d;
            const double DOWN_PAYMENT = 20000d;

            // output
            const decimal TOTAL_INTEREST_RESULT = 83523.23m;

            // perform calculation
            decimal totalInterest = GetAmortizationCalculator().CalculateTotalInterestRounded(LOAN_AMOUNT, INTEREST, TERM, DOWN_PAYMENT);

            Assert.That(totalInterest == TOTAL_INTEREST_RESULT);
        }

        [TestCase]
        public void TestCalculateAmortizedTotalPaymentRounded()
        {
            // input
            const decimal ACTUAL_LOAN_AMOUNT = 80000m;
            const decimal TOTAL_INTEREST_AMOUNT = 83523.23m;

            // ouput
            const decimal TOTAL_PAYMENT_RESULT = 163523.23m;

            decimal totalPayment = GetAmortizationCalculator().CalculateTotalPaymentRounded(ACTUAL_LOAN_AMOUNT, TOTAL_INTEREST_AMOUNT);

            Assert.That(totalPayment == TOTAL_PAYMENT_RESULT);
        }

        private ILoanCalculator GetAmortizationCalculator()
        {
            return new AmortizationCalculator();
        }
    }
}
