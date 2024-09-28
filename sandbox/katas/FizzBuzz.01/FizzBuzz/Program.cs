// See https://aka.ms/new-console-template for more information
using System;

class Program
{
    static void Main()
    {
        FizzBuzz();
    }

    static void FizzBuzz()
    {
        for (int i = 1; i <= 100; i++)
        {
            string output = "";

            // Check if the number contains a 3 or 5 in addition to divisibility checks
            bool containsThree = i.ToString().Contains('3');
            bool containsFive = i.ToString().Contains('5');

            if (i % 3 == 0 || containsThree)
            {
                output += "Fizz";
            }

            if (i % 5 == 0 || containsFive)
            {
                output += "Buzz";
            }

            if (output == "")
            {
                output = i.ToString();
            }

            Console.WriteLine(output);
        }
    }
}

