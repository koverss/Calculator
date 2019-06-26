﻿using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Fis_sstTest
{
    class Program
    {
        static MathAndValidation mav = new MathAndValidation();

        private static void Main(string[] args)
        {
            string input;
            do
            {
                ClearConsole();
                input = Console.ReadLine();
            }
            while (!Regex.IsMatch(input, @"^(\d+[-+*/])*\d+$"));

            Console.WriteLine("\nResult: " + mav.Calculate(input) + "\n");
            var dtEvaluateResult = mav.Eval(input);
            Console.WriteLine($"\nResult: {(double)new DataTable().Compute(input,null):0.##############################}\n");
            Console.WriteLine($"\nResult: {mav.Eval(input):0.##############################}\n");
            ContinueMessage();
            Console.ReadLine();
        }

        private static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the calculator!");
            Console.WriteLine("Please enter what you want me to solve: \n");
        }

        private static void ContinueMessage()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Write("Do you want to continue? Y/N ");
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
                while (!Regex.IsMatch(input, "^(\\d+[-+*/])*\\d+$")); //"^(\\d+[-+*/])+\\d+$"

                Console.WriteLine("\nResult: " + mav.Calculate(input) + "\n");

                ContinueMessage();
                Console.ReadLine();
            }
            else if (keyPressed == ConsoleKey.N)
                Environment.Exit(0);
        }        
    }
}