using LoanPaymentCalculator.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator.Service
{
    /// <summary>
    /// Gets loan payment info and returns it as JSON.
    /// </summary>
    public class JsonLoanPaymentCalculatorService : ILoanPaymentCalculatorService
    {
        private readonly ILoanCalculator m_loanCalculator = null;

        private double m_loanAmount = 0d;
        private double m_interest = 0d;
        private double m_downPayment = 0d;
        private double m_term = 0d;

        private ILoanCalculator LoanCalculator
        {
            get { return m_loanCalculator; }
        }

        public JsonLoanPaymentCalculatorService(ILoanCalculator loanCalculator)
        {
            m_loanCalculator = loanCalculator;
        }

        public string GetLoanInfo(IList<string> arguments)
        {
            // amount: 100000
            // interest: 5.5%
            // downpayment: 20000
            // term: 30
            // <blank line>

            string loanInfo = null;
            // parse each argument
            if (HasNoArguments(arguments))
            {
                loanInfo = "Please provide some arguments.";
            }
            else if (AmountArgumentInvalid(arguments))
            {
                loanInfo = "Unable to parse the 'amount' argument. Example usage: 'amount: 100000'";
            }
            else if (InterestArgumentInvalid(arguments))
            {
                loanInfo = "Unable to parse the 'interest' argument. Example usage: 'interest: 5.5%'";
            }
            else if (DownPaymentArgumentInvalid(arguments))
            {
                loanInfo = "Unable to parse the 'downpayment' argument. Example usage: 'downpayment: 20000'";
            }
            else if (TermPaymentArgumentInvalid(arguments))
            {
                loanInfo = "Unable to parse the 'term' argument. Example usage: 'term: 30'";
            }
            else
            {
                // convert to JSON
                decimal payment = LoanCalculator.CalculateMonthlyPaymentRounded(m_loanAmount, m_interest, m_term, m_downPayment);
                decimal totalInterest = LoanCalculator.CalculateTotalInterestRounded(m_loanAmount, m_interest, m_term, m_downPayment);
                decimal actualLoanAmount = (decimal)(m_loanAmount - m_downPayment);
                decimal totalPayment = LoanCalculator.CalculateTotalPaymentRounded(actualLoanAmount, totalInterest);
                IPaymentResult result = new PaymentResult(payment, totalInterest, totalPayment);
                loanInfo = JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            return loanInfo;
        }

        private bool TermPaymentArgumentInvalid(IList<string> arguments)
        {
            bool invalid = true;
            string[] parsedTerm = SplitOnColon(arguments[3]);
            if (ArgumentSyntaxIsCorrect(parsedTerm)
                && IsTermArgument(parsedTerm)
                && TermIsValid(parsedTerm))
            {
                invalid = false;
            }
            return invalid;
        }

        private bool TermIsValid(string[] parsedTerm)
        {
            return Double.TryParse(parsedTerm[1], out m_term)
                && m_term >= 0d;
        }

        private bool IsTermArgument(string[] parsedTerm)
        {
            return String.Equals(parsedTerm[0].Trim(), "term", StringComparison.OrdinalIgnoreCase);
        }

        private bool DownPaymentArgumentInvalid(IList<string> arguments)
        {
            bool invalid = true;
            string[] parsedDownPayment = SplitOnColon(arguments[2]);
            if (ArgumentSyntaxIsCorrect(parsedDownPayment)
                && IsDownPaymentArgument(parsedDownPayment)
                && DownPaymentIsValid(parsedDownPayment))
            {
                invalid = false;
            }
            return invalid;
        }

        private bool DownPaymentIsValid(string[] parsedDownPayment)
        {
            return Double.TryParse(parsedDownPayment[1].Replace(",", ""), out m_downPayment)
                && m_downPayment >= 0d;
        }

        private bool IsDownPaymentArgument(string[] parsedDownPayment)
        {
            return String.Equals(parsedDownPayment[0].Trim(), "downpayment", StringComparison.OrdinalIgnoreCase);
        }

        private bool InterestArgumentInvalid(IList<string> arguments)
        {
            bool invalid = true;
            string[] parsedInterest = SplitOnColon(arguments[1]);
            if (ArgumentSyntaxIsCorrect(parsedInterest)
                && IsInterestArgument(parsedInterest)
                && InterestIsValid(parsedInterest))
            {
                invalid = false;
            }
            return invalid;
        }

        private bool InterestIsValid(string[] parsedInterest)
        {
            return Double.TryParse(parsedInterest[1].TrimEnd(new char[] { '%' }), out m_interest)
                && m_interest >= 0d;
        }

        private bool IsInterestArgument(string[] parsedInterest)
        {
            return String.Equals(parsedInterest[0].Trim(), "interest", StringComparison.OrdinalIgnoreCase);
        }

        private bool AmountArgumentInvalid(IList<string> arguments)
        {
            bool invalid = true;
            string[] parsedAmount = SplitOnColon(arguments[0]);
            if (ArgumentSyntaxIsCorrect(parsedAmount)
                && IsAmountArgument(parsedAmount)
                && AmountIsValid(parsedAmount))
            {
                invalid = false;
            }
            return invalid;
        }

        private bool ArgumentSyntaxIsCorrect(string[] arg)
        {
            return arg.Length == 2;
        }

        private bool AmountIsValid(string[] parsedAmount)
        {
            return Double.TryParse(parsedAmount[1].Replace(",", ""), out m_loanAmount)
                && m_loanAmount > 0d;
        }

        private bool IsAmountArgument(string[] parsedAmount)
        {
            return String.Equals(parsedAmount[0].Trim(), "amount", StringComparison.OrdinalIgnoreCase);
        }

        private string[] SplitOnColon(string arg)
        {
            return arg.Split(new char[] { ':' });
        }

        private bool HasNoArguments(IList<string> arguments)
        {
            return arguments != null
                && arguments.Count == 0;
        }
    }
}
