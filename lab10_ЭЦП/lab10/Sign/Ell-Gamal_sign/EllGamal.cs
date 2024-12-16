using System;
using System.Numerics;

namespace Ell_Gamal_sign
{
    class EllGamal
    {
        public static int obr(int a, int n)
        {
            int res = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return (res);
        }


        static void Main(string[] args)
        {
            Console.WriteLine($" ЭЦП на основе Эль-Гамаля.\n");

            long OldTicks = DateTime.Now.Ticks;

            //Алгоритм генерации подписи заключается в следующих операциях:

            int p = 2137;      ///простое
            int g = 2127;      ///  вычисление g, причем g < p – первообразный корень по модулю p
            int x = 1116;      /// выбор x, меньшего p;
            int y = (int)BigInteger.ModPow(g, x, p); ///вычисление y = gx mod p.



/*В результате зашифрования сообщения с ЭЦП на выходе будет лишь одна пара чисел, не для каждого блока сообщения.
Далее необходимо проделать следующие операции:*/

             int k = 7;     ///k – взаимно простое число с (p-1);
            int a = (int)BigInteger.ModPow(g, k, p);  //вычислить a = gk mod p;

            Console.WriteLine($" Выбрать простое число p:\n p={p}\n");
            Console.WriteLine($" Выбрать g (g<p – первообразный корень по модулю p:\n g={g}\n");
            Console.WriteLine($" Выбрать x (x<p):\n x={x}\n");
            Console.WriteLine($" Вычислить y = g^x mod p:\n y={y}\n");
            Console.WriteLine($" Выбрать k – взаимно простое число с (p-1):");
            Console.WriteLine($" k={k}\n");
            Console.WriteLine($" Вычислить a = g^k mod p:");
            Console.WriteLine($" a={a}\n\n");
            int H = 2119;
            int m = p - 1;
            int k_1 = obr(k, p - 1);
            //вычислить b = k-1 (H(Mo) – xa) mod (p-1);
            var b = new BigInteger((k_1 * (H - (x * a) % m) % m) % m);
            Console.WriteLine($" H={H}\n k_1={k_1}\n b={b}\n\n S = {{a,b}} - ЭЦП\n S = {a},{b} \n");

            /*Пара чисел S = {a,b} и будет являться цифровой подписью. Далее получателю будет отправлено сообщение M’=Mo||S, 
             которое является конкатенацией исходного сообщения и ЭЦП.
Для верификации подлинности полученного сообщения необходимо проверить равенство ya * ab (mod p) = gh (mod p), 
            в которое подставляются все вычисленные ранее значения, h=H(Mп) – хеш-образ полученного сообщения.*/

            var timee = (DateTime.Now.Ticks - OldTicks) / 1000;


            Console.WriteLine("\n Верификация:");

            OldTicks = DateTime.Now.Ticks;

            var ya = BigInteger.ModPow(y, a, p);
            var ab = BigInteger.ModPow(a, b, p);
            var pr1 = BigInteger.ModPow(ya * ab, 1, p);
            var pr2 = BigInteger.ModPow(g, H, p);

            if (pr1 == pr2)
            {
                Console.WriteLine($" {pr1}  =  {pr2}\n Верификация пройдена успешно");
            }

            Console.WriteLine($"\n\nВремя генерации ЭЦП: {timee} мс");
            Console.WriteLine($"Время проведения верификации: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n\n");

            Console.ReadKey();
        }
    }
}


