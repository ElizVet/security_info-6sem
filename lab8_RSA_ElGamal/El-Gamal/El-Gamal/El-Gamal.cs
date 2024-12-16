using System;
using System.Collections.Generic;
using System.Numerics;

namespace ConsoleApp12
{
    class Program
    {
        static int g_main;
        static BigInteger a;


        /*Definition number <g>*/
        // выбираем число g, такое что g<p и является первообразным по модулю числа p. 
        // степени числа g (gi, 1≤ i ≤ p-1) дают все возможные по модулю p остатки, которые взаимно-просты с р. 
        public static bool Search_g(int p, int g)
        {
            bool boolean = false;
            List<BigInteger> array_mod_number = new List<BigInteger>();
            
            BigInteger integer = ((BigInteger.Pow(g, 1)) % p);
            array_mod_number.Add(integer);
            
            for (int i = 2; i != p; i++)
            {
                integer = BigInteger.Pow(g, i) % p;
                for (int j = 0; j != i - 1; j++)
                {
                    if (array_mod_number[j] == integer)
                    {
                        g--;
                        array_mod_number.Clear();
                        i = 1;
                        integer = BigInteger.Pow(g, 1) % p;
                        array_mod_number.Add(integer);
                        break;
                    }

                    if ((j == i - 2) && (array_mod_number[j] != integer))
                    {
                        array_mod_number.Add(integer);
                    }
                }
            }
            g_main = g;
            boolean = true;
            return boolean;
        }
       
        
        /*Definition simple number <p>*/
        public static int Search_p()
        {
            Random random = new Random();
            int p = 0;
            Boolean boolean = false;
            do
            {
                p = random.Next(2000, 2500); //генерирует простое число p случайным образом в диапазоне

                for (int i = 2; i != p; i++)
                {
                    if (i == p - 1)
                   
                    {
                        boolean = Search_g(p, p - 1);
                        break;
                    }
                    if (p % i == 0) break;
                }
            }
            while (boolean == false);
            return p;
        }
       
        
        /*Cipher*/
        public static List<BigInteger> Cipher(string text, int p, BigInteger y)
        {
            List<BigInteger> array = new List<BigInteger>();
            Random random = new Random();
            int k = random.Next(1, p - 1);


            for (int i = 0; i != text.Length; i++)
            {
                a = BigInteger.Pow(g_main, k) % p;
                array.Add((BigInteger.Pow(y, k) * (int)text[i]) % p);
            }
            return array;
        }


        /*Расшифровка*/
        //
        public static string Cipher_RAZ(int length_text, List<BigInteger> array_number, int x, int p)
        {
            string save_text = "";
            BigInteger integer;

            for (int i = 0; i != length_text; i++)
            {//вычислим каждый блок исходного сообщения по формуле: mi = (bi *(ai)p-x-1) mod p. 
                integer = (array_number[i] * (BigInteger.Pow(a, p - 1 - x))) % p;
                save_text += (char)integer;
            }
            return save_text;
        }


        static void Main(string[] args)
        {
            int p = 0;
            Random random = new Random();


            p = Search_p();


            int x = random.Next(1, p - 1); //Генерирую закрытый ключ x
            BigInteger y = BigInteger.Pow(g_main, x) % p; //Нахожу открытый ключ y


            Console.Write($"Числа [{p}, {g_main}, {y}] ---> [p, g, y]\n");
            Console.Write($"Открытый ключ - это <y>\n");
            Console.Write($"Закрытый ключ: {x}\n");


            while (true)
            {
                //Console.Write($"Введите текст: ");
                string text = "Шимчёнок Елизавета Константиновна";//Console.ReadLine();
                Console.WriteLine();
                Console.Write(text);

                //Зашифрование
                long OldTicks = DateTime.Now.Ticks;
                List<BigInteger> array_cipher_text = new List<BigInteger>();
                array_cipher_text = Cipher(text, p, y);
                Console.WriteLine("\n\nЗашифрованное сообщение: ");
                for (int i = 0; i != text.Length; i++)
                {
                    Console.WriteLine($"{i}:[{a}, {array_cipher_text[i]}] ");
                }
                Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n\n");


                //Расшифрование
                OldTicks = DateTime.Now.Ticks;
                Console.Write($"Расшифрованное сообщение: {text = Cipher_RAZ(text.Length, array_cipher_text, x, p)}\n");
                Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n");

                Console.ReadKey();
            }

            
        }
    }
}

