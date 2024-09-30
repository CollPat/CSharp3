// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        //existuje trida Random s metodou Next ktera ma ruzne zajimave parametry. Urcite by to slo pouzit na generaci 5 celych cisel v rozmezi 1 az 6 :)
        int[] diceRoll = { 1, 1, 1, 5, 1 };
        int score = CalculateScore(diceRoll);
        Console.WriteLine("Score: " + score);
    }

    static int CalculateScore(int[] dice)
    {
        int score = 0;


        /*dobry napad pouzivat pocet kolikrat kostka byla hozena nez pole samotnych kostek, urcite to velmi zjednodusuje logiku,
        existuji ale lepsi (mozna spise citelnejsi) zpusoby nez array - napr dictionary

        dale najdes nastrel jak vytvorit slovnik (dictionary), dale to necham na tobe jestli to vylepsis ci ne :)
        */
        int[] counts = new int[7];
        var countOfDices = new Dictionary<int, int>(); // mame prazdny slovnik kde je klic i hodnota typu int

        foreach (int diceNumber in dice)
        {
            counts[diceNumber]++;
            if (!countOfDices.ContainsKey(diceNumber)) //podivame se jestli slovnik obsahuje klik = cislo na kostce
            {
                countOfDices[diceNumber] = 1; //pokud ne tak tak tohle nam zajisti se slovnik bude obsahovat klic = cislo na konstce a hodnota bude 1
            }
            else
            {
                countOfDices[diceNumber] += 1; //pokud ano, tohle udela to ze k hodnote klice = cislo na kostce bude inkrementovane o 1
            }
        }
        foreach (var d in countOfDices)
        {
            //tady mam kontrolu ze to funguje :)
            Console.WriteLine($"dice {d.Key} is rolled {d.Value} times");
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
