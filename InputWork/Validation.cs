using System.Text.RegularExpressions;

namespace Fis_sstTest.InputWork
{
    public class Validation
    {
        // validating data provided by user
        public static bool ValidateInput(string input)
        {
            return Regex.IsMatch(input, @"^(\d+[-+*/])*\d+$");
        }
    }
}
