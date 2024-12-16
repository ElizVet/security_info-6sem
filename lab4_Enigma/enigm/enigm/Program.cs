using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace enigm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message = "Elizaveta Shimchyonok Konstantinovna";
            while (!Regex.IsMatch(message, @"^[a-zA-Z ]+$"))
            {
                Console.Write("Only letters A-Z is allowed, try again: ");
                message = Console.ReadLine();
            }

            Console.WriteLine("Message: " + message + "\n");

            message = message.Replace(" ", "").ToUpper();

            Enigma enigma = new Enigma();
            enigma.ShowRotors();

            Console.WriteLine("\nmessage: \t\t" + message);
            string encrypt = enigma.EnigmaCrypt(message);
            Console.WriteLine("crypted message: \t" + encrypt);

            enigma.ChangeRotorPosition();
            Console.WriteLine("\n\nсrypted message: " + enigma.EnigmaCrypt(message));

            enigma.ChangeRotorPosition();
            Console.WriteLine("сrypted message: " + enigma.EnigmaCrypt(message));

            enigma.ChangeRotorPosition();
            Console.WriteLine("сrypted message: " + enigma.EnigmaCrypt(message));

            enigma.ChangeRotorPosition();
            Console.WriteLine("сrypted message: " + enigma.EnigmaCrypt(message));

            enigma.ChangeRotorPosition();
            Console.WriteLine("сrypted message: " + enigma.EnigmaCrypt(message));

            Console.WriteLine("____________________________");
            int a = 2 ^ 17;
            Console.WriteLine(a);
            int b = a % 247;
            Console.WriteLine(b);
        }
    }
}
