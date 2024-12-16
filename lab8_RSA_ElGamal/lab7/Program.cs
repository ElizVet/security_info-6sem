using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BigInteger = System.Numerics.BigInteger;

namespace lab8
{
    class Program
    {
        private static readonly Random Random = new Random();
        private const string Message = "Simakovich Vladislav Vital'evich";
        private const uint P = 811;
        private const uint G = 3;

        static void Main()
        {
            // Task1();

            Console.WriteLine($"Message: {Message}");
            var watch = new Stopwatch();

            Console.WriteLine("RSA encryption:");
            using (var rsa = new RSACryptoServiceProvider())
            {
                watch.Start();
                var encryptedData = rsa.Encrypt(Encoding.Unicode.GetBytes(Message), true);
                watch.Stop();
                Console.WriteLine($"Encrypted data: {Encoding.Unicode.GetString(encryptedData)}\n" +
                                  $"\tElapsed time: {watch.ElapsedMilliseconds}ms");

                watch.Restart();
                var decryptedData = rsa.Decrypt(encryptedData, true);
                watch.Stop();
                Console.WriteLine($"Decrypted data: {Encoding.Unicode.GetString(decryptedData)}\n" +
                                  $"\tElapsed time: {watch.ElapsedMilliseconds}ms");
            }

            Console.WriteLine("\nElGamal encryption:");
            var x = (uint)Random.Next(2, (int)P - 1);
            {
                watch.Restart();
                var encryptedData = EncryptElGamal(Encoding.Unicode.GetBytes(Message), P, G, x);
                watch.Stop();
                var encryptedDataAsString = string.Join("", encryptedData.Select(n => n.ToString()));
                Console.WriteLine($"Encrypted data: {encryptedDataAsString}\n" +
                                  $"\tElapsed time: {watch.ElapsedMilliseconds}ms");

                watch.Restart();
                var decryptedData = DecryptElGamal(encryptedData, P, x);
                watch.Stop();
                Console.WriteLine($"Decrypted data: {Encoding.Unicode.GetString(decryptedData)}\n" +
                                  $"\tElapsed time: {watch.ElapsedMilliseconds}ms");
            }
        }

        private static void Task1()
        {
            var a = Random.Next(5, 35);
            var x = BigInteger.Pow(10, Random.Next(3, 100));

            var n = BigInteger.One << 1024;
            for (var i = 1023; i >= 0; i--) n |= Random.Next(1) << i;

            var watch = new Stopwatch();
            watch.Start();
            var result = BigInteger.ModPow(a, x, n);
            watch.Stop();
            Console.WriteLine($"Elapsed time for a = {a}, x = {x}, n = {n} is {watch.Elapsed.Seconds}s");
        }

        private static byte[] DecryptElGamal(IReadOnlyList<BigInteger> encryptedData, uint p, uint x)
        {
            var message = new List<byte>();
            for (var i = 0; i < encryptedData.Count; i += 2)
            {
                message.Add(
                    (byte)(BigInteger.ModPow(encryptedData[i], p - x - 1, p)
                        * encryptedData[i + 1] % p));
            }

            return message.ToArray();
        }

        private static BigInteger[] EncryptElGamal(IEnumerable<byte> message, uint p, uint g, uint x)
        {
            var y = BigInteger.ModPow(g, x, p);

            var cipher = new List<BigInteger>();
            foreach (var letter in message)
            {
                var k = Random.Next(2, (int)(p - 2));
                cipher.Add(BigInteger.ModPow(g, k, p));
                cipher.Add((BigInteger.ModPow(y, k, p) * letter % p));
            }

            return cipher.ToArray();
        }
    }
}