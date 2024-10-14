using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.WebApi.Properties
{
    public class HangmanClass
    {
        private string secretWord;
        private HashSet<char> correctGuesses;
        private HashSet<char> incorrectGuesses;
        private int maxIncorrectGuesses;
        private bool gameInProgress;

        public Hangman(string secretWord, int maxIncorrectGuesses)
        {
            this.secretWord = secretWord.ToUpper();
            this.maxIncorrectGuesses = maxIncorrectGuesses;
            correctGuesses = new HashSet<char>();
            incorrectGuesses = new HashSet<char>();
            gameInProgress = true;
        }
    }
}
