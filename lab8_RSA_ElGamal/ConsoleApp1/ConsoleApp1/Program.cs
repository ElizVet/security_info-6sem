using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //int[] aValues = { 5, 15 }; // значения параметра a
        //int[] xValues = { 10000019, 20000023, 30000029, 40000033, 50000017 }; // значения параметра x
        //int[] nValues = { 89876543995678967785458889754646965688987654399567896778545888975464696568898765439956789677854588897546469656889876543995678967785458889754646965688987654399567896778545888975464696568898765439956789677854588897546469656889876543995678967785458889754646965688987654399567896778545888975464696568898765439956, 222 }; // значения параметра n

        //Console.WriteLine("a\t\tx\t\t\tn\t\t\t\tTime (ms)\t\ty");
        //Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");

        //for (int i = 0; i < aValues.Length; i++)
        //{
        //    for (int j = 0; j < xValues.Length; j++)
        //    {
        //        for (int k = 0; k < nValues.Length; k++)
        //        {
        //            int a = aValues[i];
        //            int x = xValues[j];
        //            int n = nValues[k];
        //            long y;

        //            Stopwatch stopwatch = Stopwatch.StartNew();

        //            // вычисление параметра у
        //            y = (long)(Math.Pow(a, x) % n);

        //            stopwatch.Stop();

        //            // вывод результатов
        //            //Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}\t\t\t{4}", a, x, Convert.ToString(n, 2), stopwatch.ElapsedMilliseconds, y);
        //        }
        //    }
        //}

        //int bitLength = 1024; // или 2048
        //byte[] buffer = new byte[bitLength / 8];
        //Random random = new Random();
        //random.NextBytes(buffer);
        //BigInteger number = new BigInteger(buffer);
        //number = BigInteger.Abs(number); // чтобы получить число положительное

        //Console.WriteLine(number);

        string mess = "Elizaveta";
        var res = Encoding.ASCII.GetBytes(mess);
        foreach(var i in res)
        {
            Console.WriteLine(i);
        }
        
    }
}

//using System;
//using System.Diagnostics;
//using System.Numerics;

//class Program
//{
//    static void Main()
//    {
//        // Задаем параметры a, x и n
//        int[] aValues = new int[] { 5, 20 };
//        BigInteger[] xValues = new BigInteger[] {
//            BigInteger.Parse("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
//            BigInteger.Parse("3000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
//            BigInteger.Parse("5000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
//            BigInteger.Parse("7000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
//            BigInteger.Parse("9000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")
//        };
//        string[] nValues = new string[] {
//            "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
//            "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111"
//        };

//        // Выводим заголовок таблицы
//        Console.WriteLine("|    a    |              x               |                n                |  y  |   Время (мс)  |");

//        // Обходим все возможные комбинации параметров a, x и n
//        foreach (int a in aValues)
//        {
//            foreach (BigInteger x in xValues)
//            {
//                foreach (string n in nValues)
//                {
//                    // Вычисляем значение параметра y
//                    Stopwatch stopwatch = new Stopwatch();
//                    stopwatch.Start();
//                    BigInteger y = BigInteger.ModPow(a, x, BigInteger.Parse(n));
//                    stopwatch.Stop();

//                    // Выводим результаты в таблицу
//                    Console.WriteLine("| {0,6} | {1,30} | {2,30} | {3,3} | {4,13} |", a, x, n, y, stopwatch.ElapsedMilliseconds);
//                }
//            }
//        }

//        // Ожидаем нажатия клавиши Enter для завершения программы
//        Console.ReadLine();
//    }
//}


////using System;
////using System.Collections.Generic;
////using System.Diagnostics;
////using System.Linq;
////using System.Numerics;
////using System.Text;
////using System.Threading.Tasks;

////namespace ConsoleApp1
////{
////    internal class Program
////    {
////        static void Main(string[] args)
////        {
////            List<int> aValues = new List<int>() { 5, 35 }; // значения a
////            List<int> xValues = Enumerable.Range(103, 100).Where(IsPrime).Take(10).ToList(); // значения x
////            List<string> nValues = new List<string>() { "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
////                "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" }; // значения n

////            Console.WriteLine("\ta\tx\tn\ttime(ms)");

////            foreach (int a in aValues)
////            {
////                foreach (int x in xValues)
////                {
////                    foreach (string n in nValues)
////                    {
////                        Stopwatch stopwatch = new Stopwatch();
////                        stopwatch.Start();
////                        BigInteger y = BigInteger.ModPow(a, x, BigInteger.Parse(n));
////                        stopwatch.Stop();
////                        Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", a, x, n.Length, stopwatch.ElapsedMilliseconds);
////                    }
////                }
////            }

////            Console.ReadLine();
////        }

////        // Функция для проверки, является ли число простым
////        static bool IsPrime(int number)
////        {
////            if (number < 2) return false;
////            if (number == 2 || number == 3) return true;
////            if (number % 2 == 0 || number % 3 == 0) return false;

////            int i = 5;
////            int w = 2;

////            while (i * i <= number)
////            {
////                if (number % i == 0) return false;

////                i += w;
////                w = 6 - w;
////            }

////            return true;
////        }
////    }
////}
