using System;
using Service.Services;
using Service.Utils;
using Service.Validators;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleValidator.ValidateArgsAndThrow(args);
                var sourceFilePath = ConsoleHelper.GetSourceFilePath(args);
                var method = args[1];

                switch (args[0])
                {
                    case "encode":
                    {
                        var word = args[3];
                        var wordAsBits = string.Empty;
                        foreach (var character in word)
                        {
                            wordAsBits += Constants.Constants.RussianCharactersToCodes[character];
                        }
                        var targetFilePath = ConsoleHelper.GetTargetFilePath(args);
                        HandleEncodeMode(method, sourceFilePath, targetFilePath, wordAsBits, word);
                        break;
                    }
                    case "decode":
                        HandleDecodeMode(method, sourceFilePath);
                        break;
                    default:
                        throw new ArgumentException("Wrong mode");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void HandleDecodeMode(string method, string sourceFilePath)
        {
            var word = method switch
            {
                "1" => SpecialSymbolsService.Decode(sourceFilePath),
                "2" => EndSpacesService.Decode(sourceFilePath),
                "3" => ReplacementService.Decode(sourceFilePath),
                _ => throw new ArgumentException("Wrong method")
            };

            Console.WriteLine("Word: " + word);
            ConsoleHelper.ShowFileSize(sourceFilePath);
        }

        private static void HandleEncodeMode(string method, string sourceFilePath, string targetFilePath, string wordAsBits, string word)
        {
            switch (method)
            {
                case "1":
                    SpecialSymbolsService.Encode(sourceFilePath, targetFilePath, wordAsBits);
                    break;
                case "2":
                    EndSpacesService.Encode(targetFilePath, sourceFilePath, wordAsBits);
                    break;
                case "3":
                    ReplacementService.Encode(targetFilePath, sourceFilePath, wordAsBits, word);
                    break;
                default:
                    throw new ArgumentException("Wrong method");
            }
            Console.WriteLine("Success");
            ConsoleHelper.ShowFileSize(sourceFilePath);
            ConsoleHelper.ShowFileSize(targetFilePath);
        }
    }
}