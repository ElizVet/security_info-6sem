using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enigm
{
    internal class Enigma
    {
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        private string rotorLeft = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        private string rotorMedium = "FSOKANUERHMBTIYCWLQPZXVGJD";
        private string rotorRight = "VZBRGITYUPSDNHLXAWMJQOFECK";
        private string reflectorCDunn = "ARBDCOEJFNGTHKIVLMPWQZSXUY";
        
        private Random random = new Random();

        private string rotorPosition;
        public string RotorPosition { get => rotorPosition; set => rotorPosition = value; }


        public static string Shift(string s, int count)
        {
            return s.Remove(0, count) + s.Substring(0, count);
        }

        public void ChangeRotorPosition()
        {
            rotorLeft = Shift(rotorLeft, random.Next(0, 25));
            rotorRight = Shift(rotorLeft, random.Next(0, 25));
            rotorMedium = Shift(rotorLeft, random.Next(0, 25));
        }

        public void ShowRotors()
        {
            Console.WriteLine("alphabet:\t" + alphabet);
            Console.WriteLine("rotorRight:\t" + rotorRight);
            Console.WriteLine("rotorMedium:\t" + rotorMedium);
            Console.WriteLine("rotorLeft:\t" + rotorLeft);
            Console.WriteLine("reflector:\t" + reflectorCDunn);
        }


        public string EnigmaCrypt(string message)
        {
            char cryptedSymbol;
            int index = 0;
            int counterForMediumRotor = 0;
            
            StringBuilder cryptedMessage = new StringBuilder(message.Length);
            
            for (int currentIndexOfMessage = 0; currentIndexOfMessage < message.Length; currentIndexOfMessage++)
            {
                rotorRight = Shift(rotorRight, 2);

                if ((currentIndexOfMessage + 1) % 26 == 0)
                {
                    rotorMedium = Shift(rotorMedium, 1);
                    counterForMediumRotor++;

                    if (counterForMediumRotor % 26 == 0)
                        rotorLeft = Shift(rotorLeft, 1);
                }

                cryptedSymbol = message[currentIndexOfMessage]; // берем символ один из сообщения
                index = alphabet.IndexOf(cryptedSymbol);        // находим его индекс в алфавите

                cryptedSymbol = rotorRight[index];              // находим индекс в правом роторе, берем символ - 1 шаг есть
                index = alphabet.IndexOf(cryptedSymbol);        // ищем символ в алфавите, берем индекс


                cryptedSymbol = rotorMedium[index];               // from rot 2
                index = alphabet.IndexOf(cryptedSymbol);

                cryptedSymbol = rotorLeft[index];                 // from rot 3
                index = reflectorCDunn.IndexOf(cryptedSymbol);     //from refl

                if ((index + 1) % 2 == 0)
                {
                    cryptedSymbol = reflectorCDunn[index - 1];
                }
                else
                {
                    cryptedSymbol = reflectorCDunn[index + 1];
                }

                index = alphabet.IndexOf(cryptedSymbol);          // делаем тоже самое в обратную сторону
                cryptedSymbol = rotorLeft[index];

                index = alphabet.IndexOf(cryptedSymbol);
                cryptedSymbol = rotorMedium[index];

                index = alphabet.IndexOf(cryptedSymbol);
                cryptedSymbol = rotorRight[index];

                cryptedMessage.Append(cryptedSymbol);
            }
            return cryptedMessage.ToString();
        }
    }
}
