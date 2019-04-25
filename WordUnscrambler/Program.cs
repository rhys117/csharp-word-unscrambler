using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscrambler.Data;
using WordUnscrambler.Workers;

namespace WordUnscrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private const string wordListFileName = Constants.wordListFileName;

        static void Main(string[] args)
        {
            try
            {
                bool continueWordUnscramble = true;
                do
                {
                    Console.WriteLine(Constants.OptionsOnHowToEnterScrambledWords);
                    var option = Console.ReadLine() ?? string.Empty;

                    switch (option.ToUpper())
                    {
                        case Constants.File:
                            Console.Write(Constants.EnterScrambledWordsViaFile);
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        case Constants.Manual:
                            Console.Write(Constants.EnterScrambledWordsManually);
                            ExecuteScrambledWordsManualEntryScenario();
                            break;
                        default:
                            Console.Write(Constants.EnterScrambledWordsOptionNotRecognised);
                            break;
                    }

                    var continueDecision = string.Empty;
                    do
                    {
                        Console.WriteLine(Constants.OptionsOnContinuingTheProgram);
                        continueDecision = (Console.ReadLine() ?? string.Empty);
                    } while (
                        !continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                        !continueDecision.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

                    continueWordUnscramble = continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);
                } while (continueWordUnscramble);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorProgramWillBeTerminated + ex.Message);
            }
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            try
            {
                var manualInput = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = manualInput.Split(',');
                DisplayMatchedUnscrambledWords(scrambledWords);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoading + ex.Message);
            }
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var fileName = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(fileName);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(wordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            { 
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine(Constants.MatchFound, matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.MatchNotFound);
            }
        }
    }
}
