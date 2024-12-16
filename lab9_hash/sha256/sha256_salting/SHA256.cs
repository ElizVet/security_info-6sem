using System;
using System.Text;

namespace sha256_salting
{//суль алг дополнить смс колвом битов, потмо разбивается на блоки, 
    public class SHA256
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"\nХеширование SHA256\n");
            long OldTicks = DateTime.Now.Ticks;
            //алгог - 64 итерации в 1 цикле, принимает на вход смс рра до 264 бит, 
            string text = "Shimchyonok Elizaveta Konstantinovna";

            //соль
            string salt = CreateSalt(15);
            string hash = GenerateSHA256(text, salt);
           
            Console.WriteLine("M:  " + text + "\nСоль: " + salt + "\nХэш:  " + hash);
            //Console.WriteLine("{0:x2}");
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n\n");
            Console.ReadKey();
        }
        /*«соль» указанной длины – строка данных, которая позже будет передана хэш-функции вместе с входным массивом данных для вычисления хэша*/
        public static string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            //рнг возвращает случайную последовательность чисел, которую мы конвертируем в строку base64
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        //генерация хэша
        public static string GenerateSHA256(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ToHex(hash);
        }
        //После получения результата функции, преобразуем его в 16-ричное представление
        public static string ToHex(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach(byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}
