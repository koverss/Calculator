using System;

namespace Fis_sstTest.InputWork
{
    public class UserInputCapture
    {
        // gathering data provided by user
        public static string GatherUserInput()
        {
            string input;

            do
            {
                ConsoleMessages.ClearConsole();
                input = Console.ReadLine();
            }
            while (!Validation.ValidateInput(input));

            return input;
        }
    }
}
