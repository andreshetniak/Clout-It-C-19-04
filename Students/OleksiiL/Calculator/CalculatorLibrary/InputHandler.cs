﻿using System;
using System.Collections.Generic;

namespace CalculatorLibrary
{
    public static class InputHandler
    {
        public static List<char> ReadInput(bool isInline, out List<int> inlineResults)
        {
            var input = new List<char>();
            inlineResults = new List<int>();
            while (true)
            {
                for (var i = 0; ; i++)
                {
                    var t = Console.ReadKey();
                    if ((t.KeyChar >= '0' && t.KeyChar <= '9') || t.KeyChar == '*' || t.KeyChar == '/' || t.KeyChar == '+' || t.KeyChar == '-' || t.KeyChar == '=' || t.KeyChar == '(' || t.KeyChar == ')')
                    {
                        input.Add(t.KeyChar);
                        if (t.KeyChar == '=' && isInline == false)
                        {
                            if (i >= 1 && ((input[i - 1] >= '0' && input[i - 1] <= '9') || input[i - 1] == '(' || input[i - 1] == ')'))
                            {
                                return input;
                            }

                            throw new ArgumentException("Argument Exception. Incorrect use of =");
                        }
                        if (t.KeyChar == '=' && isInline)
                        {
                            throw new ArgumentException("Argument Exception. Incorrect use of =");
                        }
                        if (t.KeyChar == '(')
                        {
                            if ((i >= 1 && (input[i - 1] == '*' || input[i - 1] == '/' || input[i - 1] == '+' || input[i - 1] == '-')) || i == 0)
                            {

                                var listChars = ReadInput(true, out var iRes);
                                ParseInput(listChars, iRes, out var numbers, out var operators);
                                var res = Count(numbers, operators);
                                inlineResults?.Add(res);
                            }
                            else
                                throw new ArgumentException("Argument Exception. Incorrect use of (");
                        }
                        if (t.KeyChar == ')' && isInline)
                        {
                            if (i >= 1 && ((input[i - 1] >= '0' && input[i - 1] <= '9') || input[i - 1] == '(' || input[i - 1] == ')'))
                            {
                                return input;
                            }

                            throw new ArgumentException("Argument Exception. Incorrect use of )");
                        }
                        if (t.KeyChar == ')' && isInline == false)
                        {
                            throw new ArgumentException("Argument Exception. Incorrect use of )");
                        }
                        if (t.KeyChar == '*' || t.KeyChar == '/' || t.KeyChar == '+' || t.KeyChar == '-')
                        {
                            if (i >= 1 && ((input[i - 1] >= '0' && input[i - 1] <= '9') || input[i - 1] == '(' || input[i - 1] == ')'))
                            {
                            }
                            else
                            {
                                throw new ArgumentException("Argument Exception. Incorrect use of operators");
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Argument Exception. Unsupported symbol");
                    }
                }
            }
        }
        public static void ParseInput(List<char> input, List<int> inlineResults, out List<int> numbers, out List<char> operators)
        {
            numbers = new List<int>();
            operators = new List<char>();
            var j = 0;
            for (var i = 0; i < input.Count; i++)
            {
                if (input[i] == '(')
                {
                    numbers.Add(inlineResults[j]);
                    j++;
                }
                else if (input[i] == '*' || input[i] == '/' || input[i] == '+' || input[i] == '-')
                {
                    operators.Add(input[i]);
                }
                else if (input[i] == '=' || input[i] == ')')
                {

                }
                else if (i >= 1 && input[i - 1] >= '0' && input[i - 1] <= '9')
                {
                    numbers[numbers.Count - 1] = int.Parse(numbers[numbers.Count - 1] + (input[i] - '0').ToString());
                }
                else
                {
                    numbers.Add(input[i] - '0');
                }
            }
        }
        public static int Count(List<int> numbers, List<char> operators)
        {
            for (var i = 0; i < operators.Count; i++)
            {
                if (operators[i] == '*' || operators[i] == '/')
                {

                    if (operators[i] == '*')
                    {
                        checked
                        {
                            numbers[i] = Operations<int>.Multiply(numbers[i], numbers[i + 1]);
                        }
                        for (var j = i; j < operators.Count - 1; j++)
                        {
                            operators[j] = operators[j + 1];
                            numbers[j + 1] = numbers[j + 2];
                        }
                        numbers.RemoveAt(numbers.Count - 1);
                        operators.RemoveAt(operators.Count - 1);
                        i--;
                    }
                    else if (operators[i] == '/')
                    {
                        numbers[i] = Operations<int>.Divide(numbers[i], numbers[i + 1]);
                        for (var j = i; j < operators.Count - 1; j++)
                        {
                            operators[j] = operators[j + 1];
                            numbers[j + 1] = numbers[j + 2];
                        }
                        numbers.RemoveAt(numbers.Count - 1);
                        operators.RemoveAt(operators.Count - 1);
                        i--;
                    }
                }
            }
            for (var i = 0; i < operators.Count; i++)
            {
                if (operators[i] == '+')
                {
                    checked
                    {
                        numbers[i] = Operations<int>.Add(numbers[i], numbers[i + 1]);
                    }
                    for (var j = i; j < operators.Count - 1; j++)
                    {
                        operators[j] = operators[j + 1];
                        numbers[j + 1] = numbers[j + 2];
                    }
                    numbers.RemoveAt(numbers.Count - 1);
                    operators.RemoveAt(operators.Count - 1);
                    i--;
                }
                else if (operators[i] == '-')
                {
                    checked
                    {
                        numbers[i] = Operations<int>.Subtract(numbers[i], numbers[i + 1]);
                    }
                    for (var j = i; j < operators.Count - 1; j++)
                    {
                        operators[j] = operators[j + 1];
                        numbers[j + 1] = numbers[j + 2];
                    }
                    numbers.RemoveAt(numbers.Count - 1);
                    operators.RemoveAt(operators.Count - 1);
                    i--;
                }
            }
            if (numbers.Count == 1)
            {
                return numbers[0];
            }

            throw new ArgumentException("Argument Exception. Something goes wrong");
        }

    }
}
