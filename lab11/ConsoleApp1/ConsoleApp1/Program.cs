using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Задание 1
            EllipticCurve ellipticCurve = new EllipticCurve(-1, 1, 751, 728);
            //Как и в случае с непрерывными ЭК, теперь важным является вычисление некоторого числа d, если мы знаем P
            //и Q для Q = dP.Это и есть задача дискретного логарифмирования для эллиптических кривых.

            Console.WriteLine("--------- ЗАДАНИЕ 1.1 ---------");
            int xmin = 621, xmax = 655;
            Dictionary<int, int> xValues = new Dictionary<int, int>();
            Dictionary<int, int> yValues = new Dictionary<int, int>();

            int tempX = 0;
            Console.WriteLine("\n Квадратичные вычеты по модулю 751");
            while (tempX <= xmax)
            {
                Console.WriteLine($"{tempX}^2 mod 751= {(tempX * tempX) % 751} ");
                yValues.Add(tempX, (tempX * tempX) % 751);
                tempX++;
            }

            tempX = xmin;
            Console.WriteLine("\n Рассчет по формуле у^2 = х^3 + aх + b (mod p)"); //у2 = х3 + aх + b - уравнением Вейерштрасса
            while (tempX <= xmax)
            {
                Console.WriteLine($"x = {tempX}, y^2 = {(tempX * tempX * tempX - tempX + 1) % 751} ");
                xValues.Add(tempX, (tempX * tempX * tempX - tempX + 1) % 751);
                tempX++;
            }

            Console.WriteLine("\n Точки, принадлежащие ЭК:");
            foreach (var xx in xValues.Keys)
            {
                xValues.TryGetValue(xx, out int func1);
                foreach (var yy2 in yValues.Values)
                {
                    if (func1 == yy2)
                    {
                        var xx1 = yValues.FirstOrDefault(p => p.Value == yy2).Key;
                        Console.WriteLine($"(x, y) = ({xx}, {xx1})");
                    }
                }
            }

            Dott dott = new Dott();
            for (int x = 621; x < 655; x++)
            {
                dott = new Dott(x, ellipticCurve);
            }

            //В криптографии на основе ЭК тайный ключ – это случайное целое d, выбранное из множества { 1, 2, ..., q – 1}, где q –
            //порядок подгруппы; открытый ключ – это точка Q, такая,
            //что Q = dG, где G – базовая точка подгруппы.

            //Console.WriteLine("Точки, принадлежащие ЭК:");
            //Console.WriteLine("(x, y) = (623, 166)");
            //Console.WriteLine("(x, y) = (628, 293)");
            //Console.WriteLine("(x, y) = (629, 348)");
            //Console.WriteLine("(x, y) = (631, 143)");
            //Console.WriteLine("(x, y) = (634, 93)");
            //Console.WriteLine("(x, y) = (636, 4)");
            //Console.WriteLine("(x, y) = (638, 131)");
            //Console.WriteLine("(x, y) = (640, 190)");
            //Console.WriteLine("(x, y) = (642, 53)");
            //Console.WriteLine("(x, y) = (643, 94)\r\n(x, y) = (644, 340)");
            //Console.WriteLine("(x, y) = (646, 45)\r\n(x, y) = (651, 191)");
            //Console.WriteLine("(x, y) = (652, 315)");
            //Console.WriteLine("(x, y) = (654, 102)");
            

            Console.WriteLine("\n--------- ЗАДАНИЕ 1.2 ---------");
            Console.WriteLine(" Выполнения операций над точками кривой");

            BigInteger k = 6;
            BigInteger l = 7;

            Dott P = new Dott(61, 622);
            Dott Q = new Dott(61, 622);
            Dott R = new Dott(90, 730);

            Console.Write("k = 6\n");
            Console.Write("l = 7\n");
            Console.Write("P = ");
            Show.Dott(P);
            Console.Write("Q = ");
            Show.Dott(Q);
            Console.Write("R = ");
            Show.Dott(R);

            Console.WriteLine("\n Промежуточные вычисления:");
            Dott k2P = Dott.DottMultiplication(P, 2, ellipticCurve);
            Console.Write("2P = ");
            Show.Dott(k2P);

            Dott k4P = Dott.DottMultiplication(k2P, 2, ellipticCurve);
            Console.Write("4P = ");
            Show.Dott(k4P);
            /*
                2P = (615,490)
                4P = (126,718)

                kP = 6P
                kP = (189,454)
            */
            Console.WriteLine("\nkP = 6P");
            Dott kP = Dott.DottMultiplication(P, k, ellipticCurve);
            Console.Write("kP = ");
            Show.Dott(kP);

            Dott PQ = Dott.Addition(P, Q, ellipticCurve);
            Console.Write("\nP + Q = ");
            Show.Dott(PQ);

            Dott kPlQ_R = Dott.Addition(Dott.Addition(Dott.DottMultiplication(P, k, ellipticCurve),
                                                 Dott.DottMultiplication(Q, l, ellipticCurve),
                                                 ellipticCurve),
                                                 Dott.GetDottNegativeByY(R, ellipticCurve),
                                                 ellipticCurve);
            Console.Write("\nkP + lQ - R = ");
            Show.Dott(kPlQ_R);

            Dott P_QR = Dott.Addition(Dott.Addition(P,
                                                    Dott.GetDottNegativeByY(Q, ellipticCurve),
                                                    ellipticCurve),
                                                    R,
                                                    ellipticCurve);
            Console.Write("\nP - Q + R =   ");
            Show.Dott(P_QR);

            Console.ReadLine();
            Console.Clear();
            #endregion

            #region Задание 2
            Console.WriteLine("--------- ЗАДАНИЕ 2 ---------");
            Console.WriteLine(" Зашифровать/расшифровать имя на основе ЭК");

            List<char> alphabet = new List<char>()
            {
                'А',
                'Б',
                'В',
                'Г',
                'Д',
                'Е',
                'Ж',
                'З',
                'И',
                'Й',
                'К',
                'Л',
                'М',
                'Н',
                'О',
                'П',
                'Р',
                'С',
                'Т',
                'У',
                'Ф',
                'Х',
                'Ц',
                'Ч',
                'Ш',
                'Щ',
                'Ъ',
                'Ы',
                'Ь',
                'Э',
                'Ю',
                'Я'
            };

            List<Dott> alphabetOfDotts = new List<Dott>()
            {
                new Dott(189, 297), //А
                new Dott(189, 454), //Б
                new Dott(192, 32),  //В
                new Dott(192, 719), //Г
                new Dott(194, 205), //Д
                new Dott(194, 546), //Е
                new Dott(197, 145), //Ж
                new Dott(197, 606), //З
                new Dott(198, 224), //И
                new Dott(198, 527), //Й
                new Dott(200, 30),  //К
                new Dott(200, 721), //Л
                new Dott(203, 324), //М
                new Dott(203, 427), //Н
                new Dott(205, 372), //О
                new Dott(205, 379), //П
                new Dott(206, 106), //Р
                new Dott(206, 645), //С
                new Dott(209, 82),  //Т
                new Dott(209, 669), //У
                new Dott(210, 31),  //Ф
                new Dott(210, 720), //Х
                new Dott(215, 247), //Ц
                new Dott(215, 504), //Ч
                new Dott(218, 150), //Ш
                new Dott(218, 601), //Щ
                new Dott(221, 138), //Ъ
                new Dott(221, 613), //Ы
                new Dott(226, 9),   //Ь
                new Dott(226, 742), //Э
                new Dott(227, 299), //Ю
                new Dott(227, 452)  //Я
            };

            string message = "ЛИЗА";
            Console.WriteLine($"Исходное сообщение: {message}");

            Dott G = new Dott(0, 1);
            BigInteger d = 51;

            Console.WriteLine($"G = (0,1)\nd = 51");

            Q = Dott.DottMultiplication(G, d, ellipticCurve);
            Console.WriteLine("Открытый ключ (Q = dG): ");
            Show.Dott(Q);
            Console.WriteLine();

            Random rand = new Random();
            Dott C1 = new Dott();
            Dott C2 = new Dott();
            string messageNew = "";

            Console.WriteLine("Зашифрованное сообщение: ");
            for (int i = 0; i < message.Length; i++)
            {
                P = alphabetOfDotts[alphabet.IndexOf(message[i])];
                //Console.WriteLine($"Is dot belongs to curve: {Dott.IsDottBelongsToCurve(P, ellipticCurve)}");
                //Console.WriteLine(Dott.IsDottBelongsToCurve(P, ellipticCurve));
                Console.WriteLine();
                k = 6;
                C1 = Dott.DottMultiplication(G, k, ellipticCurve);
                C2 = Dott.DottMultiplication(Q, k, ellipticCurve);
                C2 = Dott.Addition(P, C2, ellipticCurve);

                Show.Dott(C1);
                Show.Dott(C2);

                P = Dott.Addition(C2,
                                  Dott.GetDottNegativeByY(Dott.DottMultiplication(C1, d, ellipticCurve), 
                                  ellipticCurve),
                                  ellipticCurve);

                messageNew += alphabet[
                                       alphabetOfDotts.IndexOf(
                                            alphabetOfDotts.Find(dd => dd.x == P.x && dd.y == P.y)
                                            )
                                       ];
            }
            Console.WriteLine(messageNew);

            Console.ReadLine();
            Console.Clear();
            #endregion

            #region Задание 3
            Console.WriteLine("--------- ЗАДАНИЕ 3 ---------");
            Console.WriteLine("\nГенерация ЭЦП");

            Console.WriteLine("ЭК: у^2 = х^3 - х + 1 (mod 751)");
            Console.WriteLine($"H(M) = Буква 'Л' - 200 => H(M) = 200 mod 13 = {200 % 13}");

            BigInteger data;
            data = 200 % 13;

            Dott baseDott = new Dott(416, 55);
            Console.Write("G = ");
            Show.Dott(baseDott);

            BigInteger q = 13;
            k = 2;
            Console.WriteLine($"q = {q}\nk = {k} (1 < k < q)");

            BigInteger privateKey = 11;
            Console.WriteLine($"d = {privateKey}");

            Dott openKey = Dott.DottMultiplication(baseDott, privateKey, ellipticCurve);
            Console.Write("Открытый ключ (Q = dG): ");
            Show.Dott(openKey);

            Console.WriteLine("\nВерификация ЭЦП");
            Dott signature = ECDSA.CreateSignatureWithKandOrder(data, privateKey, baseDott, ellipticCurve, k, q);
            //Console.Write("ЭЦП(числа r и s): ");
            //Show.Dott(signature);

            bool isVerifired = ECDSA.VerifySignatureWithOrder(data, openKey, signature, baseDott, ellipticCurve, q);
            Console.WriteLine($"- Верификация прошла успешно? \n- {isVerifired}");

            Console.ReadLine();
            #endregion

        }
    }

    public static class Show
    {
        public static void Dott(Dott dott)
        {
            Console.WriteLine("({0},{1})", dott.x, dott.y);
        }
    }

}
