using System;
using System.Data;
using System.Linq;

namespace Fis_sstTest
{
    public class MathAndValidation : ICalculations
    {
        // function that calculates index of operator that should be considered 
        // in current Calculate function go through
        public int CalculateIndex(string userInput)
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

        // recursive function that in every go through does next in order operation
        public string Calculate(string userInput)
        {
            // checking if string contains only one value, if so, it will be returned
            if ((!userInput.Contains('-') || userInput.Contains('-') && userInput.IndexOf('-') == 0 && userInput.Count(x => x == '-') == 1) 
                && !userInput.Contains('+') && !userInput.Contains('*') && !userInput.Contains('/'))
                return userInput;
            else
            {
                // checking and removing scientific notation
                if(userInput.Contains('E'))
                    userInput = userInput.ScientificNotation();

                // again checking if newly provided number can be returned
                if ((!userInput.Contains('-') || userInput.Contains('-') && userInput.IndexOf('-') == 0 && userInput.Count(x => x == '-') == 1)
                && !userInput.Contains('+') && !userInput.Contains('*') && !userInput.Contains('/'))
                    return userInput;

                var leftSide = "";
                var rightSide = "";

                // calculating index of operator that should be taken care of in first order
                int ind = CalculateIndex(userInput);
                double result = 0;

                // taking left and right value of that operator
                leftSide = userInput.TakeLeft(ind);
                rightSide = userInput.TakeRight(ind);

                if (userInput.First() == '-')
                    leftSide = '-' + leftSide;
                
                // calculating reslut of current operation
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

                // concatenating everything together
                var leftSideToConcat = "";

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
                // calling function again but with new string
                return Calculate(userInput);
            }
        }

        // function from DataTable class that calculates result (to compare)
        public double Eval(string userInput)
        {
            DataTable dt = new DataTable();
            var result = (double)dt.Compute(userInput, null);
            return result;
        }
    }
}

