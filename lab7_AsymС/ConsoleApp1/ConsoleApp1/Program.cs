using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public static readonly int Z = 8;
        public static readonly int A = 31;
        public static readonly int N = 420;
        public static readonly int[] D = { 2, 3, 6, 13, 27, 52, 105, 210 };
        public static string M = "Shimchyonok_Elizaveta_Konstantinovna";

        static void Main(string[] args)
        {
            // Генерация сверхвозрастающей последовательности (тайного ключа)
            var r = new Knapsack();
            int[] d2 = r.Generate(8);
            Console.WriteLine($"Закрытый ключ d: {r.Str(D)}");

            // Вычисление нормальной последовательности (открытого ключа)
            int[] e = r.GetNorm(D, A, N, Z);
            Console.WriteLine($"Открытый ключ e: {r.Str(e)}\n");

            //Зашифрование ФИО
            long oldTicks = DateTime.Now.Ticks;
            int[] c = r.GetСipher(e, M, Z);
            Console.WriteLine($"\n\nЗашифрованное сообщение C: {r.Str(c)}");
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - oldTicks) / 1000} мс\n");

            //Расшифрование ФИО
            int a1 = r.a_1(A, N);

            int[] s = new int[c.Length];
            string m2 = "";

            for (int i = 0; i < c.Length; i++)
            {
                s[i] = (c[i] * a1) % N;
            }
            Console.WriteLine($"\nВектор весов S: {r.Str(s)}      a^(-1) = {a1}");

            oldTicks = DateTime.Now.Ticks;
            foreach (int si in s)
            {
                string m2I = r.Decipher(D, si, Z);  //110000
                m2 += m2I + " ";
            }
            Console.WriteLine($"\nРасшифрованное сообщение: {m2}");
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - oldTicks) / 1000} мс\n");

            m2 = m2.Replace(" ", "");
            var stringArray = Enumerable.Range(0, m2.Length / 8).Select(i => Convert.ToByte(m2.Substring(i * 8, 8), 2)).ToArray();
            var str = Encoding.UTF8.GetString(stringArray);
            Console.WriteLine(str);
        }
    }
}
