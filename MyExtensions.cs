using System.Linq;

namespace Fis_sstTest
{
    public static class MyExtensions
    {
        public static string TakeLeft(this string userInput, int counter)
        {
            string leftSide = "";
            int currIndex = counter - 1;

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

        public static string TakeRight(this string userInput, int counter)
        {
            string rightSide = "";
            int currIndex = counter + 1;

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

        public static string ScientificNotation(this string input)
        {
            string tail = "";
            string head = "";
            string body = "";
            string stringBeforeE = "";
            bool isMatch;

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

            tail = new string(input.Skip(input.IndexOf('E')).ToArray().Skip(3).SkipWhile(x => x != '-' && x != '+' && x != '*' && x != '/').ToArray());

            if (isMatch)
            {
                head = new string(input.Take(new string(input.TakeWhile(x => x != 'E').ToArray()).LastIndexOfAny(new char[] { '-', '+', '*', '/' }) + 1).ToArray());

                if (head != "")
                    body = new string(input.Take(input.IndexOf('E')).ToArray()).Replace(head, "");
                else
                    body = new string(input.Take(input.IndexOf('E')).ToArray());
            }
            else
            {
                body = stringBeforeE;
            }

            int dotIndex = body.IndexOf('.');
            string tempResult = "";
            string pow = new string(input.Skip(input.IndexOf('E')).Skip(2).TakeWhile(x => x != '-' && x != '+' && x != '*' && x != '/').ToArray());
            double currentPow = double.Parse(pow);
            double numberOfZeros = 0;

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

            return head + body + tail;
        }
    }
}