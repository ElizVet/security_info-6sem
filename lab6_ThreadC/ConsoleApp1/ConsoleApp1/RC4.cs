using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class RC4
    {
            //Инициализация:   i = 0   j = 0 
            //Цикл генерации:   i = (i + 1) mod 2n   j = (j + S[i]) mod 2n 
            //Перестановка (S[i], S[j])             Результат: K = S[(S[i] + S[j]) mod 2n

            byte[] S = new byte[256]; ///массив-перестановка, сод. все байты от 0x00 до 0xFF
            int x = 0;      ///переменные счетчики
            int y = 0;


            public RC4(byte[] key)
            {
                /// Алгоритм ключевого расписания (Key-Scheduling Algorithm)
                /// для нач. иниц S ключом

                for (int i = 0; i < 256; i++)
                {
                    S[i] = (byte)i; //пополняется линейно: 0,1…255. 
                }

                int j = 0;
                for (int i = 0; i < 256; i++) //заполняется секретным ключом другой массив [256].
                                              //ключ повторяется многократно чтобы заполнить весь массив
                {
                    j = (j + S[i] + key[i % key.Length]) % 256;
                    S.Swap(i, j);    ///поменять местами        
                }
            }


            ///для каждого байта массива исх. данных запрашивается байт ключа
            ///и объединяет их при помощи xor (^)
            public byte[] Encode(byte[] dataB, int size)
            {
                byte[] data = dataB.Take(size).ToArray();
                byte[] cipher = new byte[data.Length];

                for (int m = 0; m < data.Length; m++)
                {
                    cipher[m] = (byte)(data[m] ^ keyItem());
                }
                return cipher;
            }
            
            
            /// При к. вызове отдает след. байт ключ.потока
            /// кот. мы будем объед xor'ом с байтом исх.данных
            /// Генератор ПСП
            private byte keyItem()
            {
                x = (x + 1) % 256;
                y = (y + S[x]) % 256;

                S.Swap(x, y);

                return S[(S[x] + S[y]) % 256];
            }
        }

        static class SwapExt
        {
            public static void Swap<T>(this T[] array, int index1, int index2)
            {
                T temp = array[index1];
                array[index1] = array[index2];
                array[index2] = temp;
            }
        }

}
