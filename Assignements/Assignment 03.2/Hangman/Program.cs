using Hangman;

Console.WriteLine("Welcome to Hangman!");

var game = new HangmanClass("secret", 6);


while (game.GameInProgress)
{

    Console.WriteLine(game.DisplayWord());
    Console.WriteLine(game.DisplayIncorrectGuesses());


    Console.Write("Enter a letter to guess: ");
    char guess = Console.ReadKey().KeyChar;
    Console.WriteLine();


    Console.WriteLine(game.Guess(guess));
}
