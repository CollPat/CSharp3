using System;
using System.Text;
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

        public HangmanClass(string secretWord, int maxIncorrectGuesses)
        {
            this.secretWord = secretWord.ToUpper();
            this.maxIncorrectGuesses = maxIncorrectGuesses;
            correctGuesses = new HashSet<char>();
            incorrectGuesses = new HashSet<char>();
            gameInProgress = true;
        }

        public string Guess(char letter)
        {
            letter = char.ToUpper(letter);

            if (!char.IsLetter(letter))
            {
                return "Invalid guess. Please enter a letter from A-Z.";
            }

            if (correctGuesses.Contains(letter) || incorrectGuesses.Contains(letter))
            {
                return "Duplicate guess. Try a different letter.";
            }

            if (secretWord.Contains(letter))
            {
                correctGuesses.Add(letter);
                if (AllLettersGuessed())
                {
                    gameInProgress = false;
                    return $"Congratulations! You've won. The word was {secretWord}.";
                }
                return DisplayWord();
            }
            else
            {
                incorrectGuesses.Add(letter);
                if (incorrectGuesses.Count >= maxIncorrectGuesses)
                {
                    gameInProgress = false;
                    return $"Game Over! You've lost. The word was {secretWord}.";
                }
                return $"Incorrect guess. You have {maxIncorrectGuesses - incorrectGuesses.Count} guesses left.";
            }
        }


        public string DisplayWord()
        {
            var display = new StringBuilder();
            foreach (char c in secretWord)
            {
                if (correctGuesses.Contains(c))
                {
                    display.Append(c + " ");
                }
                else
                {
                    display.Append("_ ");
                }
            }
            return display.ToString().Trim();
        }

        private bool AllLettersGuessed()
        {
            foreach (char c in secretWord)
            {
                if (!correctGuesses.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        public string DisplayIncorrectGuesses()
        {
            return $"Incorrect guesses: {string.Join(", ", incorrectGuesses)}";
        }

    }
}
