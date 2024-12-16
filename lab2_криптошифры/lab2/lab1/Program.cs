using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string mess = "доброе утро мир";
            char[] _message = GetMessage("text.txt").ToCharArray();

            char[] _keyword = "безопасность".ToCharArray();
            Console.Write("ключ: \t");
            Output(_keyword);
            Console.WriteLine();

            #region 1.Шифр Цезаря с ключевым словом, ключевое слово – безопасность

            Console.WriteLine("---ШИФР ЦЕЗАРЯ---\n");

            char[] _alphabetCaesar = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя".ToCharArray();
            Console.Write("алфавит: \t\t");
            Output(_alphabetCaesar);

            char[] _substitutionAlphabetCaesar = InsertIntoAlphabetKeyword(_alphabetCaesar, _keyword).ToCharArray();
            Console.Write("алфавит подстановки: \t");
            Output(_substitutionAlphabetCaesar);

            // зашифровка

            #region запуск времени выполнения
            Stopwatch ticksEncode = new Stopwatch();
            ticksEncode.Start();
            #endregion

            string _encryptMessageCaesar = EncryptCaesar(_message, _alphabetCaesar, _substitutionAlphabetCaesar);
            CreateFile(_encryptMessageCaesar, "caesarEncrypt.txt");

            #region остановка времени выполнения
            ticksEncode.Stop();
            #endregion

            #region запуск времени выполнения
            Stopwatch ticksDecode = new Stopwatch();
            ticksDecode.Start();
            #endregion

            // расшифровка
            string _decryptMessageCaesar = DecryptCaesar(_encryptMessageCaesar.ToCharArray(), _alphabetCaesar, _substitutionAlphabetCaesar);
            CreateFile(_decryptMessageCaesar, "caesarDecrypt.txt");

            #region остановка времени выполнения
            ticksDecode.Stop();
            #endregion


            Console.WriteLine("\nвремя зашифровки: " + ticksEncode.Elapsed);
            Console.WriteLine("время расшифровки: " + ticksDecode.Elapsed);
            Console.WriteLine("\nРезультаты были записаны в файл.");

            Console.WriteLine("\n\nПример:");
            Console.WriteLine("соо: " + mess);
            Console.WriteLine("зашифр соо: " + EncryptCaesar(mess.ToCharArray(), _alphabetCaesar, _substitutionAlphabetCaesar));
            Console.WriteLine("расшифр соо: " + DecryptCaesar(EncryptCaesar(mess.ToCharArray(), _alphabetCaesar, _substitutionAlphabetCaesar).ToCharArray(), _alphabetCaesar, _substitutionAlphabetCaesar));


            #endregion


            #region 2.Таблица Трисемуса, ключевое слово – безопасность


            Console.WriteLine("\n\n---ТАБЛИЦА ТРИСЕМУСА---\n");

            char[,] _tableTrisemus = new char[8,4]; // 8 - кол-во строк

            char[] _alphabetTrisemus = "абвгдежзийклмнопрстуфхцчшщьыъэюя".ToCharArray();
            Console.WriteLine("используется алфавит без буквы 'ё'");

            InputInTable(_tableTrisemus, _alphabetTrisemus, _keyword);

            Console.WriteLine("таблица 8х4");
            OutputMatrix(_tableTrisemus);

            #region запуск времени выполнения
            ticksEncode.Start();
            #endregion

            // зашифровка
            string _encryptMessageTrisemus = EncryptTrisemus(_message, _tableTrisemus);
            CreateFile(_encryptMessageTrisemus, "trisemusEncrypt.txt");

            #region остановка времени выполнения
            ticksEncode.Stop();
            #endregion

            #region запуск времени выполнения
            ticksDecode.Start();
            #endregion

            // расшифровка
            string _decryptMessageTrisemus = DecryptTrisemus(_encryptMessageTrisemus.ToCharArray(), _tableTrisemus);
            CreateFile(_decryptMessageTrisemus, "trisemusDecrypt.txt");

            #region остановка времени выполнения
            ticksDecode.Stop();
            #endregion

            Console.WriteLine("\nвремя зашифровки: " + ticksEncode.Elapsed);
            Console.WriteLine("время расшифровки: " + ticksDecode.Elapsed + "\n");
            Console.WriteLine("\nРезультаты были записаны в файл.");

            Console.WriteLine("\n\nПример:");
            Console.WriteLine("соо: " + mess);
            Console.WriteLine("зашифр соо: " + EncryptTrisemus(mess.ToCharArray(), _tableTrisemus));
            Console.WriteLine("расшифр соо: " + DecryptTrisemus(EncryptTrisemus(mess.ToCharArray(), _tableTrisemus).ToCharArray(), _tableTrisemus));

            #endregion
        }

        static void Output(char[] message)
        {
            foreach (char symbol in message)
            {
                Console.Write(symbol);
            }
            Console.WriteLine();
        }
        static void OutputMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }


        // шифр Цезаря
        static string InsertIntoAlphabetKeyword(char[] alphabet, char[] keyword)
        {
            string substitutionAlphabet = "";

            foreach (var item in keyword.Distinct())
            {
                substitutionAlphabet += item;
            }

            foreach (var item in alphabet.Except(keyword).ToArray())
            {
                substitutionAlphabet += item;
            }
            Console.WriteLine();

            return substitutionAlphabet;
        }
        static string EncryptCaesar(char[] message, char[] alphabet, char[] substitutionAlphabet)
        {
            string encryptMessage = "";

            foreach (char symbol in message)
            {
                for(int index = 0; index < alphabet.Length; index++)
                {
                    if (symbol == alphabet[index])
                    {
                        encryptMessage += substitutionAlphabet[index];
                    }
                }
            }

            return encryptMessage;
        }
        static string DecryptCaesar(char[] message, char[] alphabet, char[] substitutionAlphabet)
        {
            string decryptMessage = "";

            foreach (char symbol in message)
            {
                for(int index = 0; index < substitutionAlphabet.Length; index++)
                {
                    if (symbol == substitutionAlphabet[index])
                    {
                        decryptMessage += alphabet[index];
                    }
                }
            }
            return decryptMessage;
        }


        // работа с файлами
        static string GetMessage(string fileName)
        {
            string message;
            var filePath = Path.GetFullPath(fileName);

            using (StreamReader file = new StreamReader(filePath))
            {
                message = file.ReadToEnd();
            }

            return message;
        }
        static void CreateFile(string text, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(text);
            }
        }


        // таблица Трисемуса
        static void InputInTable(char[,] tableTrisemus, char[] alphabet, char[] keyword)
        {
            string substitutionAlphabet = InsertIntoAlphabetKeyword(alphabet, keyword);
            int index = 0;
            
            for (int i = 0; i < tableTrisemus.GetLength(0); i++)
            {
                for (int j = 0; j < tableTrisemus.GetLength(1); j++)
                {
                    tableTrisemus[i, j] = substitutionAlphabet[index];
                        index++;
                }
            }
        }
        static string EncryptTrisemus(char[] message, char[,] tableTrisemus)
        {
            string encryptMessage = "";
            foreach (char symbol in message)
            {
                for (int i = 0; i < tableTrisemus.GetLength(0); i++)
                {
                    for (int j = 0; j < tableTrisemus.GetLength(1); j++)
                    {
                        if (symbol == tableTrisemus[i,j])
                        {
                            if(i == 7)
                                encryptMessage += tableTrisemus[0, j];
                            else
                                encryptMessage += tableTrisemus[i+1,j];
                        }
                    }
                }
            }
            return encryptMessage;
        }
        static string DecryptTrisemus(char[] message, char[,] tableTrisemus)
        {
            string decryptMessage = "";
            foreach (char symbol in message)
            {
                for (int i = 0; i < tableTrisemus.GetLength(0); i++)
                {
                    for (int j = 0; j < tableTrisemus.GetLength(1); j++)
                    {
                        if (symbol == tableTrisemus[i,j])
                        {
                            if(i == 0)
                                decryptMessage += tableTrisemus[7, j];
                            else
                                decryptMessage += tableTrisemus[i-1,j];
                        }
                    }
                }
            }
            return decryptMessage;
        }

    }
}

