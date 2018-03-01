using LoanPaymentCalculator.Business;
using LoanPaymentCalculator.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // amount: 100000
            // interest: 5.5%
            // downpayment: 20000
            // term: 30
            // <blank line>

            string input = null;
            string jsonResult = null;
            List<string> arguments = new List<string>();

            try
            {
                do
                {
                    input = Console.ReadLine();
                    if (InputExists(input))
                    {
                        arguments.Add(input);
                    }
                }
                while (InputExists(input));

                jsonResult = GetJsonCalculatorService().GetLoanInfo(arguments);
            }
            catch (Exception ex)
            {
                // log error

                jsonResult = String.Format("An unexpected error occurred. Please try again.{0}{0}Error: {1}",
                    Environment.NewLine,
                    ex.Message);
            }
            Console.WriteLine(jsonResult);
            Console.WriteLine("Press any key to exit...");

            // hold open
            Console.ReadLine();
        }

        private static bool InputExists(string input)
        {
            return !String.IsNullOrWhiteSpace(input);
        }

        private static ILoanPaymentCalculatorService GetJsonCalculatorService()
        {
            return new JsonLoanPaymentCalculatorService(new AmortizationCalculator());
        }
    }
}
