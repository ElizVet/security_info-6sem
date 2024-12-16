using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private const string Key = "shimliza";
        private const string FileNameSource = "SourceText.txt";
        private const string FileNameCipher = "CipherText.txt";

        static void Main(string[] args)
        {
            try
            {
                var des = DES.Create();
                var watch = new Stopwatch();

                using (var inputFileStream = File.OpenRead(FileNameSource))
                {
                    StreamReader streamReader = new StreamReader(inputFileStream);
                    string data = streamReader.ReadLine();
                    Console.WriteLine($"Input string: {data}");

                    MemoryStream memoryStream = new MemoryStream();
                    
                    watch.Start();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream,
                        des.CreateEncryptor(Encoding.ASCII.GetBytes(Key), des.IV),
                        CryptoStreamMode.Write);
                    cryptoStream.Write(Encoding.ASCII.GetBytes(data ?? string.Empty), 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    watch.Stop();
                    
                    Console.WriteLine($"Encoded string: {Encoding.ASCII.GetString(memoryStream.ToArray())}\n" +
                                      $"\tElapsed time: {watch.Elapsed}");
                    
                    var outputFileStream = File.Open(FileNameCipher, FileMode.OpenOrCreate);
                    outputFileStream.Write(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
                    outputFileStream?.Close();
                }


                using (var inputFileStream = File.Open(FileNameCipher, FileMode.OpenOrCreate))
                {
                    watch.Restart();
                    var cryptoStream = new CryptoStream(inputFileStream,
                        des.CreateDecryptor(Encoding.ASCII.GetBytes(Key), des.IV),
                        CryptoStreamMode.Read);
                    var sReader = new StreamReader(cryptoStream);
                    var data = sReader.ReadLine();
                    watch.Stop();
                    Console.WriteLine($"Decoded string: {data}\n" +
                                      $"\tElapsed time: {watch.Elapsed}");
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
