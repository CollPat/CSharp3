using System;
using System.Collections.Generic;
using System.Linq;

// Class to roll the dice randomly (value type)
class DiceRoller
{
    private static Random random = new Random();

    // Method to roll 'n' dice (generates random numbers between 1 and 6)
    public int[] RollDice(int numDice)
    {
        int[] dice = new int[numDice];
        for (int i = 0; i < numDice; i++)
        {
            dice[i] = random.Next(1, 7); // Generates a random number between 1 and 6
        }
        return dice; // Value type (array of integers)
    }
}

// Class to calculate the score (reference type)
class ScoreCalculator
{
    // Method to calculate score based on dice roll (List<int> as reference type)
    public int CalculateScore(List<int> dice)
    {
        int score = 0;

        // Count occurrences of each die face (1 to 6)
        int[] counts = new int[7];
        foreach (int die in dice)
        {
            counts[die]++;
        }

        // Check for a straight (1,2,3,4,5,6)
        if (dice.Count == 6 && counts.Skip(1).All(c => c == 1))
        {
            return 1200;
        }

        // Check for three pairs
        if (dice.Count == 6 && counts.Where(c => c == 2).Count() == 3)
        {
            return 800;
        }

        // Handle scoring for multiples
        for (int i = 1; i <= 6; i++)
        {
            if (counts[i] == 4)
            {
                score += i * 100 * 2; // Four-of-a-kind (multiply triple score by 2)
            }
            else if (counts[i] == 5)
            {
                score += i * 100 * 4; // Five-of-a-kind (multiply triple score by 4)
            }
            else if (counts[i] == 6)
            {
                score += i * 100 * 8; // Six-of-a-kind (multiply triple score by 8)
            }
            else if (counts[i] >= 3)
            {
                score += i * 100; // Regular triple
            }
        }

        // Handle scoring for single ones and fives (leftover after triples)
        score += counts[1] % 3 * 100; // Single ones
        score += counts[5] % 3 * 50;  // Single fives

        return score;
    }
}

// Main class to call and demonstrate the code in the console
class Program
{
    static void Main()
    {
        DiceRoller roller = new DiceRoller();
        ScoreCalculator calculator = new ScoreCalculator();

        // Simulating rolling of 5 dice
        int[] diceRoll = roller.RollDice(5);

        // Converting value type (array) to reference type (List<int>)
        List<int> diceList = diceRoll.ToList();

        // Display the rolled dice
        Console.WriteLine("Rolled dice: " + string.Join(", ", diceRoll));

        // Calculate score
        int score = calculator.CalculateScore(diceList);
        Console.WriteLine("Score: " + score);
    }
}
