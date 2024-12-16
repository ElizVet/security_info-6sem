using System.IO;
using System.Text;
using Service.Utils;
using Service.Validators;

namespace Service.Services
{
    public static class EndSpacesService
    {
        public static void Encode(string targetFilePath, string sourceFilePath, string word)
        {
            FileValidator.ValidateByLinesCountAndThrow(targetFilePath, word);
            var source = File.ReadAllText(sourceFilePath, Encoding.UTF8);

            using var streamWriter = File.CreateText(targetFilePath);
            var counter = 0;
            for (var i = 0; i < source.Length - 2; i++)
            {
                if (source[i] == ' ' && source[i + 1] == '\r' && source[i + 2] == '\n')
                {
                    //remove (skip) all end spaces
                    counter++;
                    continue;
                }
                if (source[i] == '\r')
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
                        //add space if 1 (do nothing if 0)
                        streamWriter.Write(" ");
                    }
                    counter++;
                }
                streamWriter.Write(source[i]);
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
                
                //detect 1
                if (source[i - 1] == ' ' && source[i] == '\r')
                {
                    word += "1";
                    continue;
                } 

                //detect 0
                if (source[i-1] != ' ' && source[i] == '\r')
                    word += "0";
            }

            return DecodeHelper.DecodeWord(Constants.Constants.RussianCharactersToCodes, word);
        }
    }
}