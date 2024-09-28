// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] diceRoll = { 1, 1, 1, 5, 1 };
        int score = CalculateScore(diceRoll);
        Console.WriteLine("Score: " + score);
    }

    static int CalculateScore(int[] dice)
    {
        int score = 0;


        int[] counts = new int[7];

        foreach (int diceNumber in dice)
        {
            counts[diceNumber]++;
        }


        if (counts[1] >= 3)
        {
            score += 1000;
            counts[1] -= 3;
        }
        score += counts[1] * 100;


        if (counts[5] >= 3)
        {
            score += 500;
            counts[5] -= 3;
        }
        score += counts[5] * 50;


        for (int i = 2; i <= 6; i++)
        {
            if (counts[i] >= 3)
            {
                score += i * 100;
            }
        }

        return score;
    }
}
