using Fis_sstTest.MathOperations;
using System;
using System.Text.RegularExpressions;

namespace Fis_sstTest
{
    public class ConsoleMessages
    {
        private static Calculations mav = new Calculations();

        public static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the calculator!");
            Console.WriteLine("Please enter what you want me to solve: \n");
        }

        public static void CalculatorResultMessage(string input)
        {
            Console.WriteLine($"\nResult: { mav.CalculateResult(input) }");
        }

        public static void DataTableResultMessage(string input)
        {
            var dtEvaluateResult = mav.EvaluateResultWithDataTable(input);
            Console.WriteLine($"\nResult using DataTable function: { mav.EvaluateResultWithDataTable(input):0.##############################}");
        }

        public static void ContinueMessage()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Write("\nDo you want to continue? Y/N ");
                keyPressed = Console.ReadKey(false).Key;
                if (keyPressed != ConsoleKey.Enter)
                    Console.WriteLine();

            }
            while (keyPressed != ConsoleKey.Y && keyPressed != ConsoleKey.N);

            string input = "";
            if (keyPressed == ConsoleKey.Y)
            {
                ClearConsole();
                do
                {
                    ClearConsole();
                    input = Console.ReadLine();
                }
                while (!Regex.IsMatch(input, "^(\\d+[-+*/])*\\d+$"));

                CalculatorResultMessage(input);
                DataTableResultMessage(input);

                ContinueMessage();
                Console.ReadLine();
            }
            else if (keyPressed == ConsoleKey.N)
                Environment.Exit(0);
        }
    }
}
