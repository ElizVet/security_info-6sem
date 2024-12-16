using System;

namespace Service.Validators
{
    public static class ConsoleValidator
    {
        public static void ValidateArgsAndThrow(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("Please set minimum 3 args");
            }

            if(args[0] == "encode" && args.Length == 3)
            {
                throw new ArgumentException("Please set minimum 4 args");
            }
        }
    }
}