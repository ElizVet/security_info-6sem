using System;
using System.IO;

namespace Service.Utils
{
    public static class ConsoleHelper
    {
        public static void ShowFileSize(string pathToFile)
        {
            var sourceFileSize = new FileInfo(pathToFile).Length;
            Console.WriteLine(sourceFileSize + " bytes");
        }

        public static string GetTargetFilePath(string[] args)
        {
            return args.Length == 5 
                ? Constants.Constants.PathToTextFolder + args[4]
                : Constants.Constants.DefaultTargetFilePath;
        }

        public static string GetSourceFilePath(string[] args) => Constants.Constants.PathToTextFolder + args[2];
    }
}