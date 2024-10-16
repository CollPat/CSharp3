internal class Program
{
    private static void Main(string[] args)
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Hangman!");

            Hangman game = new Hangman("secret", 6);


            while (game.GameInProgress)
            {

                Console.WriteLine(game.DisplayWord());
                Console.WriteLine(game.DisplayIncorrectGuesses());


                Console.Write("Enter a letter to guess: ");
                char guess = Console.ReadKey().KeyChar;
                Console.WriteLine();


                Console.WriteLine(game.Guess(guess));
            }
        }
    }
}
