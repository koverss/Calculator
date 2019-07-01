using System.Linq;

namespace Fis_sstTest.MathOperations
{
    public static class UserDataStringExtensions
    {
        // function that takes value that is on the left of current operator
        public static string TakeLeft(this string userInput, int counter)
        {
            string leftSide = "";
            int currIndex = counter - 1;

            // taking numbers on the left of current operator while currently considered char is not a new operator
            while (currIndex != -1 && !userInput[currIndex].Equals('+')
                && !userInput[currIndex].Equals('-')
                && !userInput[currIndex].Equals('*')
                && !userInput[currIndex].Equals('/')
                )
            {
                leftSide = userInput[currIndex] + leftSide;
                currIndex -= 1;
            }
            return leftSide;
        }

        // function that takes value that is on the right of current operator
        public static string TakeRight(this string userInput, int counter)
        {
            string rightSide = "";
            int currIndex = counter + 1;

            // taking numbers on the right of current operator while currently considered char is not a new operator
            while (currIndex < userInput.Length
                && !userInput[currIndex].Equals('+')
                        && !userInput[currIndex].Equals('-')
                        && !userInput[currIndex].Equals('*')
                        && !userInput[currIndex].Equals('/'))
            {
                rightSide = rightSide + userInput[currIndex];
                currIndex += 1;
            }

            return rightSide;
        }

        // function that removes scientific notation from string
        public static string ScientificNotation(this string input)
        {
            var tail = "";
            var head = "";
            var body = "";
            var stringBeforeE = "";
            bool isMatch;

            // checking if first value is in scientific notation
            if (input.First() != '-')
            {
                stringBeforeE = new string(input.Take(input.IndexOf('E')).ToArray());
                isMatch = stringBeforeE.Contains('-') || stringBeforeE.Contains('+') || stringBeforeE.Contains('*') || stringBeforeE.Contains('/');
            }
            else
            {
                stringBeforeE = new string(input.Take(input.IndexOf('E') - 1).ToArray());
                isMatch = stringBeforeE.Contains('-') || stringBeforeE.Contains('+') || stringBeforeE.Contains('*') || stringBeforeE.Contains('/');
            }

            // taking everything that is after number in scientific notation
            tail = new string(input.Skip(input.IndexOf('E')).ToArray().Skip(3).SkipWhile(x => x != '-' && x != '+' && x != '*' && x != '/').ToArray());

            // if there is sth before number in scientific notation then we put it to 'head' variable
            if (isMatch)
            {
                head = new string(input.Take(new string(input.TakeWhile(x => x != 'E').ToArray()).LastIndexOfAny(new char[] { '-', '+', '*', '/' }) + 1).ToArray());

                // creating 'body' as value before letter 'E'
                if (head != "")
                    body = new string(input.Take(input.IndexOf('E')).ToArray()).Replace(head, "");
                else
                    body = new string(input.Take(input.IndexOf('E')).ToArray());
            }
            else
            {
                // creating 'body' as value before letter 'E'
                body = stringBeforeE;
            }
            // index of dot in number; -1 if there is no dot
            int dotIndex = body.IndexOf('.');
            var tempResult = "";
            // parsing power from scientific notation
            string pow = new string(input.Skip(input.IndexOf('E')).Skip(2).TakeWhile(x => x != '-' && x != '+' && x != '*' && x != '/').ToArray());
            double currentPow = double.Parse(pow);
            double numberOfZeros = 0;

            // adding 0 at the end or moving dot deeper into number depending of sign in front of power
            if (input.Skip(input.IndexOf('E') + 1).First() == '-')
            {
                numberOfZeros = currentPow - dotIndex;

                while (numberOfZeros > 0)
                {
                    numberOfZeros--;
                    tempResult += '0';
                }
                body = "0." + tempResult + body.Replace(".", "");
            }
            else
            {
                var numberOfPlacesAfterDot = body.SkipWhile(x => x != '.').Skip(1).Count();
                if (currentPow >= numberOfPlacesAfterDot)
                {
                    numberOfZeros = currentPow - numberOfPlacesAfterDot;

                    while (numberOfZeros > 0)
                    {
                        numberOfZeros--;
                        body += '0';
                    }
                    body = tempResult + body.Replace(".", "");
                }
                else
                {
                    body = body.Replace(".", "");
                    numberOfZeros = body.Count() - currentPow;
                    body = body.Insert(dotIndex + (int)currentPow, ".");

                    while (numberOfZeros > 0)
                    {
                        numberOfZeros--;
                        body += '0';
                    }
                }
            }

            if (body.Contains('-'))
            {
                body = body.Replace("-", "");
                body = '-' + body;
            }

            // returning new string with new number without notation
            return head + body + tail;
        }
    }
}