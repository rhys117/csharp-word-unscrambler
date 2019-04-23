using System;
using System.Collections.Generic;
using System.Linq;

namespace WordUnscrambler
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueWordUnscramble = true;
            do
            {
                Console.WriteLine("Please enter option - F for file or M for manual.");
                var option = Console.ReadLine() ?? string.Empty;

                switch(option.ToUpper())
                {
                    case "F":
                        Console.Write("Enter srcambled words file name: ");
                        ExecuteScrambledWordsInFileScenario();
                        break;
                    case "M":
                        Console.Write("Enter srcambled words manually: ");
                        ExecuteScrambledWordsManualEntryScenario();
                        break;
                    default:
                        Console.Write("Option was not recognised");
                        break;
                }

                var continueDecision = string.Empty;
                do
                {
                    Console.WriteLine("Do you want to continue? (Y/N)");
                    continueDecision = (Console.ReadLine() ?? string.Empty);
                } while (
                    !continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase) && 
                    !continueDecision.Equals("N", StringComparison.OrdinalIgnoreCase));

                continueWordUnscramble = continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase);
            } while (continueWordUnscramble);
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var fileName = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(fileName);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(wordListFileName);

            List<MatchedWord> matchedWords = _wordMather.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            { 
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine("Math found for {0}: {1}", matchedWord.SrambledWord, matchedWord.word)
                }
            }
            else
            {
                Console.WriteLine("No matches have been found.");
            }
        }
    }
}
