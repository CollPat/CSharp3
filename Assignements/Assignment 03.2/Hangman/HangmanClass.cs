using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman
{
    public class HangmanClass
    {
        private string secretWord;
        private HashSet<char> correctGuesses;
        private HashSet<char> incorrectGuesses;
        private int maxIncorrectGuesses;

        /*co tohle dela, budu mit property jmenem GameInProgres ktera ma public get = dokazu se na tuto hodnotu doklikat a precist si ji
        //i.e. var game = new HangmanClass("",1);
        game.GameInProgress

        a zaroven ma privatni set = dokazu zmenit hodnotu pouze v teto tride, ne mimo ni
        i.e. game.GameInProgress = false nedokazu udelat, jelikoz to ma privatni set
        */
        public bool GameInProgress { get; private set; }

        public HangmanClass(string secretWord, int maxIncorrectGuesses)
        {
            this.secretWord = secretWord.ToUpper();
            this.maxIncorrectGuesses = maxIncorrectGuesses;
            correctGuesses = new HashSet<char>();
            incorrectGuesses = new HashSet<char>();
            GameInProgress = true;
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
                    GameInProgress = false;
                    return $"Congratulations! You've won. The word was {secretWord}.";
                }
                return DisplayWord();
            }
            else
            {
                incorrectGuesses.Add(letter);
                if (incorrectGuesses.Count >= maxIncorrectGuesses)
                {
                    GameInProgress = false;
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
