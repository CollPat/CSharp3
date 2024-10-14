var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Console.WriteLine("Welcome to Hangman!");

// Initialize a new Hangman game with a secret word and a maximum of 6 incorrect guesses
Hangman game = new Hangman("secret", 6);

// Keep playing until the game is no longer in progress
while (game.GameInProgress)
    {
         // Display the current masked word and incorrect guesses
        Console.WriteLine(game.DisplayWord());
        Console.WriteLine(game.DisplayIncorrectGuesses());

        // Ask the player for a guess
        Console.Write("Enter a letter to guess: ");
        char guess = Console.ReadKey().KeyChar;
        Console.WriteLine(); // New line for better formatting

        // Make the guess and display the result
        Console.WriteLine(game.Guess(guess));
    }

app.Run();
