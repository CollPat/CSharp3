using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.WebApi.Properties
{
    public class HangmanMethods
    {
    // Vlastnosť na kontrolu, či hra stále prebieha
        public bool GameInProgress
        {
            get { return gameInProgress; } //tohle zije v HangmanClass, tyka se to gameInProgress,correctGuesses,incorrectGuesses,secretWord,maxIncorrectGuesses
        }

        // Metóda na spracovanie hádaného písmena
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

        // Pomocná metóda na zobrazenie aktuálneho stavu slova
        public string DisplayWord()
        {
            var display = new StringBuilder(); //chybi usign System.Text
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

        // Pomocná metóda na kontrolu, či boli uhádnuté všetky písmená
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

        // Zobrazenie zoznamu nesprávnych hádaní
        public string DisplayIncorrectGuesses()
        {
            return $"Incorrect guesses: {string.Join(", ", incorrectGuesses)}";
        }
    }
}
