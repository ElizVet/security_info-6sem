using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

class Program
{
    private static readonly Random Random = new Random();
    private const string Message = "Shimchyonok Elizaveta Konstantinovna";
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
            var encryptedData = rsa.Encrypt(Encoding.ASCII.GetBytes(Message), true);
            watch.Stop();
            Console.WriteLine($"Encrypted data: {Encoding.ASCII.GetString(encryptedData)}\n" +
                                $"\tElapsed time: {watch.ElapsedMilliseconds}ms");

            watch.Restart();
            var decryptedData = rsa.Decrypt(encryptedData, true);
            watch.Stop();
            Console.WriteLine($"Decrypted data: {Encoding.ASCII.GetString(decryptedData)}\n" +
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

}
