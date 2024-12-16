using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Shorn_sign
{ public static class ElGamal
    {
        public static BigInteger CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
        }
    }


    public static class Shnorr
    {
        //̶  генерация простых чисел p, q;/ вычисление(p-1) – делителя;
        public static void Do()
        {
            BigInteger p = 2267;
            BigInteger q = 103;
            //g≠1, такого что gq = 1 mod p;
            string text = File.ReadAllText(".\\Test.txt");
            BigInteger g = 354; 
            BigInteger obg = 967;
            int x = 30; //х<q – это и будет закрытый ключ;

            BigInteger y = BigInteger.ModPow(obg, x, p); //y = g–X mod p;

            /*  Для собственно подписи сообщения необходимо реализовать следующий алгоритм:
  ̶  выбор случайного числа k<q;*/

            BigInteger a = BigInteger.Pow(g, 13) % p;  //вычислить a = gk mod p;
            BigInteger hash = ElGamal.CalculateMd5Hash(text + a.ToString());


            File.WriteAllText(".\\shnorr.txt", hash.ToString());
            BigInteger b = (13 + x * hash) % q; //̶  вычислить b = (k+xh) mod q;
            BigInteger dov = BigInteger.ModPow(g, b, p);
            BigInteger X = (dov * BigInteger.ModPow(y, hash, p)) % p;
            BigInteger hash2 = ElGamal.CalculateMd5Hash((text + X.ToString()));
/*Для проверки подписи на подлинн. X = gb * yh(mod p), проверить  равенство вычислено ранее h хеш - образа и H(Мп|| X) хеш - образа полученного сообщения с вычисленным значением Х.*/

               var f = hash == hash2;
            Console.WriteLine(f);
            string text2 = File.ReadAllText(".\\FakeTest.txt");
            BigInteger hash3 = ElGamal.CalculateMd5Hash((text2 + X.ToString()));
            var f2 = hash == hash3;
            Console.WriteLine(f2);
        }
    }


    class Program
    {
        static void Main()
        {
            string Message = "Shimchyonok Elizaveta";

            Console.WriteLine($" Алгоритм Шнорра\n");
            Console.WriteLine(" Ключевая информация:");

            const int p = 2267;
            const int q = 103;
            const int g = 354;
            const int x = 13;
            const int revGmodP = 967;
            Console.WriteLine($" p={p}\n q={q}\n g={g}\n x={x}\n y={revGmodP}\n");
            Console.WriteLine($"M0 = {Message}\n");

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var y = BigInteger.ModPow(revGmodP, x, p);
            var a = BigInteger.ModPow(g, 13, p);
            var hash = CalculateShnorrHash(Message + a);
            watch.Stop();
            Console.WriteLine($"H(H0) = {hash}\n" +
                              $"Время шифрования: {watch.ElapsedMilliseconds} мс\n");
            watch.Restart();
            var b = (13 + x * hash) % q;
            var dov = BigInteger.ModPow(g, b, p);
            var X = dov * BigInteger.ModPow(y, hash, p) % p;
            var hash2 = CalculateShnorrHash(Message + X);
            Console.WriteLine($"k=13\na={a}\nb={b}\n(g^b)mod(p)={dov}\nX=(g^b)(y^h)mod(p)={X}\n");
            

            Console.WriteLine($"Проверка верификации: \nH(H0) = {hash}\nH(Hп) = {hash2}");
            if (hash == hash2)
            {
                Console.WriteLine("Документ подтвержден");
            }
            else
            {
                Console.WriteLine("Документ не является подлинным");
            }
            watch.Stop();
            Console.WriteLine($"Время верификации: {watch.ElapsedMilliseconds} мс");
        }

        public static BigInteger CalculateShnorrHash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
        }
    }
}
