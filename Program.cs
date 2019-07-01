using Fis_sstTest.MathOperations;
using Fis_sstTest.InputWork;

namespace Fis_sstTest
{
    class Program
    {
        static Calculations mav = new Calculations();

        private static void Main(string[] args)
        {
            // capturing console input
            string input = UserInputCapture.GatherUserInput();
            // returning results via console messages
            ConsoleMessages.CalculatorResultMessage(input);
            ConsoleMessages.DataTableResultMessage(input);
            ConsoleMessages.ContinueMessage();
        }
    }
}