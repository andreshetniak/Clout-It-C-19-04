﻿using System;

namespace ExpressionTreeCalculator
{
    class Program
    {     
        delegate double CalculatorDelegate(double a, double b, string op);

        static void Main(string[] args)
        {
            Console.WriteLine(" Enter the expression with " +
                    "'+', '-', '*', '/', '(', ')' signs");
            while (true)
            {
                string s = "";
                try
                {
                    while (true)
                    {
                        var k = Console.ReadKey();
                        if (k.KeyChar == '=')
                        {
                            Console.WriteLine(ParsingHelper.Calculate(s));
                            break;
                        }
                        else s += k.KeyChar;
                    }

                    break;
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }

           
        }

           
        
    }


}
