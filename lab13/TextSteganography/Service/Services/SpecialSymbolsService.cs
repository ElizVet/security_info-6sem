using System.IO;
using System.Text;
using Service.Utils;
using Service.Validators;

namespace Service.Services
{
    public static class SpecialSymbolsService
    {
        public static void Encode(string sourceFilePath, string targetFilePath,string word)
        {
            FileValidator.ValidateByLinesCountAndThrow(sourceFilePath, word);
            var source = File.ReadAllText(sourceFilePath, Encoding.UTF8);

            using var streamWriter = File.CreateText(targetFilePath);
            var counter = 0;
            foreach (var character in source)
            {
                if (character == '\r')
                {
                    if (counter == word.Length) 
                    {
                        //add space for end of word check (_\n instead of \r\n)
                        streamWriter.Write(" ");
                        counter++;
                        continue;
                    }
                    
                    if (counter < word.Length && word[counter] == '1') 
                    {
                        //skip symbol (just \n instead of \r\n)
                        counter++;
                        continue;
                    }
                    counter++;
                }
                streamWriter.Write(character);
            }
        }

        public static string Decode(string pathToFile)
        {
            var source = File.ReadAllText(pathToFile, Encoding.UTF8);
            var word = string.Empty;
            
            for (var i = 1; i < source.Length; i++)
            {
                //break when end of word
                if (source[i - 1] == ' ' && source[i] == '\n')
                    break;

                //detect 0
                if (source[i - 1] == '\r' && source[i] == '\n')
                {
                    word += "0";
                    continue;
                } 
                
                //detect 1
                if (source[i - 1] != '\r' && source[i] == '\n')
                    word += "1";
            }

            return DecodeHelper.DecodeWord(Constants.Constants.RussianCharactersToCodes, word);
        }
    }
}