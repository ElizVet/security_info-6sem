using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- генерация ПСП на основе линейного конгруэнтного генератора -----");

            Stopwatch sw = Stopwatch.StartNew(); // Запускаем таймер
            int a = 430, c = 2531, n = 11979;
            int seed = (int)DateTime.Now.Ticks % n; // Используем текущее время для задания начального значения

            for (int i = 0; i < 10; i++) // Генерируем 10 чисел
            {
                seed = (a * seed + c) % n;
                Console.WriteLine($"x{i+1} = " + seed);
            }
            sw.Stop();

            Console.WriteLine("Время выполнения: {0} ms", sw.ElapsedMilliseconds);



            Console.ReadKey();

            Console.WriteLine("\n\n ----- потоковый шифр RC4 -----");

            int[] ikey = { 122, 125, 48, 84, 201};
            byte[] key = new byte[ikey.Length]; // размер массива 5

            for (int i = 0; i < ikey.Length; i++)
            {
                key[i] = Convert.ToByte(ikey[i]);
            }

            RC4 rc = new RC4(key);
            RC4 rc2 = new RC4(key);
            byte[] testBytes = Encoding.ASCII.GetBytes("Shimchyonok Elizaveta");


            byte[] encrypted = rc.Encode(testBytes, testBytes.Length);
            Console.WriteLine($"Зашифрованнная строка : {Encoding.ASCII.GetString(encrypted)}");


            byte[] decrypted = rc2.Encode(encrypted, encrypted.Length);
            Console.WriteLine($"Рашифрованнная строка : {Encoding.ASCII.GetString(decrypted)}");

            Console.ReadKey();

        }
    }
}
