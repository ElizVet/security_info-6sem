using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Service.Utils;

namespace Service.Services
{
    public static class ReplacementService
    {
        public static void Encode(string targetFilePath, string sourceFilePath, string bits, string word)
        {
            var source = File.ReadAllText(sourceFilePath, Encoding.UTF8);
            var wordLength = word.Length;
            var helpCode = Convert.ToString(wordLength, 2).PadLeft(Constants.Constants.CodeWidth, '0');
            var helpCounter = 0;
            var wordCounter = 0;
            
            using var streamWriter = File.CreateText(targetFilePath);
            foreach (var character in source)
            {
                //write help word
                if (Constants.Constants.EnglishToRussianCharacters.Keys.Contains(character) && helpCounter < Constants.Constants.CodeWidth)
                {
                    //replace if 1 (do nothing if 0)
                    var result = helpCode[helpCounter] == '1'
                        ? Constants.Constants.EnglishToRussianCharacters[character]
                        : character;
                    streamWriter.Write(result);
                    helpCounter++;
                    continue;
                }
                   
                //write our word
                if (Constants.Constants.EnglishToRussianCharacters.Keys.Contains(character) && wordCounter < bits.Length)
                {
                    //replace if 1 (do nothing if 0)
                    var result = bits[wordCounter] == '1'
                        ? Constants.Constants.EnglishToRussianCharacters[character]
                        : character;
                    streamWriter.Write(result);
                    wordCounter++;
                    continue;
                }
                streamWriter.Write(character);
            }
        }

        public static string Decode(string pathToFile)
        {
            var source = File.ReadAllText(pathToFile, Encoding.UTF8);
            var word = string.Empty;
            var help = string.Empty;
            var wordCounter = 0;
            var helpCounter = 0;
            var wordLength = 0;

            foreach (var character in source)
            {
                if (!Constants.Constants.EnglishToRussianCharacters.Keys.Contains(character) &&
                    !Constants.Constants.EnglishToRussianCharacters.Values.Contains(character)) continue;
                
                if (helpCounter < Constants.Constants.CodeWidth)
                {
                    help += DecodeCharacter(character);
                    helpCounter++;
                    wordLength = Convert.ToInt32(help, 2) * Constants.Constants.CodeWidth;
                }
                else if (wordCounter < wordLength && wordLength != 0)
                {
                    word += DecodeCharacter(character);
                    wordCounter++;
                }
            }

            return DecodeHelper.DecodeWord(Constants.Constants.RussianCharactersToCodes, word);
        }

        private static string DecodeCharacter(char character)
        {
            //returns 1 if ru character, 0 if en
            const string pattern = @"[а-я]+";
            var match = Regex.Match(character.ToString(), pattern, RegexOptions.IgnoreCase);
            return match.Success ? "1" : "0";
        }
    }
}