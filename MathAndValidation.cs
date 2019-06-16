using System.Linq;

namespace Fis_sstTest
{
    public static class MathAndValidation
    {
        private static int CalculateIndex(string userInput)
        {
            int ind = -2;

            if (userInput.Contains('*') && userInput.Contains('/'))
            {
                if (userInput.IndexOf('*') < userInput.IndexOf('/'))
                    ind = userInput.IndexOf('*');
                else
                    ind = userInput.IndexOf('/');
            }
            else if (userInput.Contains('*'))
                ind = userInput.IndexOf('*');
            else if (userInput.Contains('/'))
                ind = userInput.IndexOf('/');
            else if (userInput.Contains('+') && userInput.Contains('-'))
            {
                if (userInput.IndexOf('-') == 0 && userInput.Count(x => x == '-') > 1 && new string(userInput.Skip(1).ToArray()).IndexOf('-') < userInput.IndexOf('+'))
                {
                    var withoutFirstSign = new string(userInput.Skip(1).ToArray());
                    if (withoutFirstSign.IndexOf('-') < withoutFirstSign.IndexOf('+'))
                        ind = withoutFirstSign.IndexOf('-') + 1;
                }
                else if (userInput.IndexOf('-') < userInput.IndexOf('+') && userInput.IndexOf('-') != 0)
                    ind = userInput.IndexOf('-');
                else
                    ind = userInput.IndexOf('+');
            }
            else if (userInput.Contains('+'))
                ind = userInput.IndexOf('+');
            else if (userInput.Contains('-'))
            {
                if (userInput.IndexOf('-') == 0)
                {
                    var temp = new string(userInput.Skip(1).ToArray());
                    ind = new string(userInput.Skip(1).ToArray()).IndexOf('-') + 1;
                }
                else
                    ind = userInput.IndexOf('-');
            }
            return ind;
        }

        public static string Calculate(string userInput)
        {
            if ((!userInput.Contains('-') || userInput.Contains('-') && userInput.IndexOf('-') == 0 && userInput.Count(x => x == '-') == 1) 
                && !userInput.Contains('+') && !userInput.Contains('*') && !userInput.Contains('/'))
                return userInput;
            else
            {
                if(userInput.Contains('E'))
                    userInput = userInput.ScientificNotation();

                if ((!userInput.Contains('-') || userInput.Contains('-') && userInput.IndexOf('-') == 0 && userInput.Count(x => x == '-') == 1)
                && !userInput.Contains('+') && !userInput.Contains('*') && !userInput.Contains('/'))
                    return userInput;

                string leftSide = "";
                string rightSide = "";

                int ind = CalculateIndex(userInput);
                double result = 0;

                leftSide = userInput.TakeLeft(ind);
                rightSide = userInput.TakeRight(ind);

                if (userInput.First() == '-')
                    leftSide = '-' + leftSide;

                if (userInput[ind].Equals('*'))
                    result = double.Parse(leftSide) * double.Parse(rightSide); // *
                else if (userInput[ind].Equals('/'))
                {
                    if (double.Parse(rightSide) == 0)
                        return "You can't divide by 0.";
                    result = double.Parse(leftSide) / double.Parse(rightSide);  // :
                }
                else if (userInput[ind].Equals('+'))
                    result = double.Parse(leftSide) + double.Parse(rightSide);  // +
                else
                    result = double.Parse(leftSide) - double.Parse(rightSide);  // -

                string leftSideToConcat = "";

                if (leftSide.First() == '-')
                    leftSideToConcat = new string(userInput.Take(ind).ToArray());
                else
                {
                    var temp = new string(userInput.Take(ind).ToArray());
                    leftSideToConcat = new string(temp.Take(temp.LastIndexOfAny(new char[] { '-', '+', '*', '/' })+1).ToArray());
                }

                if (!leftSideToConcat.Contains('*') && !leftSideToConcat.Contains('/') && !leftSideToConcat.Contains('+') && !leftSideToConcat.Contains('-'))
                    leftSideToConcat = "";
                else if (leftSideToConcat.First() == '-')
                    leftSideToConcat = "";

                string rightSideToConcat = new string(userInput.Skip(ind + rightSide.Length + 1).ToArray());
                string resultToConcat = result.ToString();

                if (!rightSideToConcat.Contains('*') && !rightSideToConcat.Contains('/') && !rightSideToConcat.Contains('+') && !rightSideToConcat.Contains('-'))
                    rightSideToConcat = "";

                userInput = leftSideToConcat + resultToConcat + rightSideToConcat;

                return Calculate(userInput);
            }
        }
    }
}

