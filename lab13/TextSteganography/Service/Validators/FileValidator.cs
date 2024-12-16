using System;
using System.IO;
using System.Linq;

namespace Service.Validators
{
    public static class FileValidator
    {
        public static void ValidateByLinesCountAndThrow(string textPath, string word)
        {
            var lineCount = File.ReadLines(textPath).Count();
            if (lineCount < word.Length)
            {
                throw new ArgumentException("Too small text for encoding, please try another method");
            }
        }
    }
}