using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string text;
        private void LoadTextFromFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                text = File.ReadAllText(openFileDialog.FileName);
                textToEncryptBox.Text = text;
            }
            if (string.IsNullOrEmpty(text)) MessageBox.Show("Файл пуст");
        }

        private void EncrypctClick(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                textToDecryptBox.Text = EncryptSpiral(textToEncryptBox.Text, 6);
                stopwatch.Stop();
                MessageBox.Show("Время шифрования (Маршрутная перестановка): " + stopwatch.Elapsed);
            }
            else
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                textToDecryptBox.Text = EncryptPermut(textToEncryptBox.Text);
                stopwatch.Stop();
                MessageBox.Show("Время шифрования (Множественная перестановка): " + stopwatch.Elapsed);
            }
        }

        private string EncryptSpiral(string text, int key)
        {
            string newText = "";
            string word = text;
            char fill = ' ';
            int width = key;
            int height = word.Length % width == 0 ? word.Length / width : (word.Length / width) + 1;
            int k;
            for (k = word.Length; k < height * width; k++)
            {
                word += fill;
            }
            char[,] array = new char[height, width];

            int counter = 0;
            int i;
            int j;
            for (i = 0; i < height; i++)
            {
                for (j = 0; j < width; j++)
                {
                    array[i, j] = word[counter];
                    counter++;
                }
            }

            int N = height, M = width;
            int iBeg = 0, iFin = 0, jBeg = 0, jFin = 0;
            k = 1;
            i = 0;
            j = 0;
            while (k <= N * M)
            {
                newText += array[i, j];
                if (i == iBeg && j < M - jFin - 1)
                    ++j;
                else if (j == M - jFin - 1 && i < N - iFin - 1)
                    ++i;
                else if (i == N - iFin - 1 && j > jBeg)
                    --j;
                else
                    --i;
                if ((i == iBeg + 1) && (j == jBeg) && (jBeg != M - iFin - 1))
                {
                    ++iBeg;
                    ++iFin;
                    ++jBeg;
                    ++jFin;
                }
                ++k;
            }
            return newText;
        }

        private void DecrypctClick(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                textToEncryptBox.Text = DecryptSpiral(textToDecryptBox.Text, 6);
                stopwatch.Stop();
                MessageBox.Show("Время расшифрования (Маршрутная перестановка): " + stopwatch.Elapsed);
            }
            else
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                textToEncryptBox.Text = DecryptPermut(textToDecryptBox.Text);
                stopwatch.Stop();
                MessageBox.Show("Время расшифрования (Множественная перестановка): " + stopwatch.Elapsed);
            }
        }

        List<CharNum> listCharNumFirst;

        List<CharNum> listCharNumSecond;
        private string EncryptPermut(string text)
        {
            string firstKey = "elizaveta";
            // Второй ключ, количество строк
            string secondKey = "shimchenok";
            // Предложение которое шифруем
            string stringUser = text;

            string fill = " ";

            for (int i = stringUser.Length; i < firstKey.Length * secondKey.Length; i++)
            {
                stringUser += fill;
            }
            string newText = "";
            // Матрица в которой производим шифрование
            char[,] matrix = new char[secondKey.Length, firstKey.Length];

            // Счетчик символов в строке
            int countSymbols = 0;

            // Переводим строки в массивы типа char
            char[] charsFirstKey = firstKey.ToCharArray();
            char[] charsSecondKey = secondKey.ToCharArray();
            char[] charStringUser = stringUser.ToCharArray();

            // Создаем списки в которых будут храниться символы и порядковые номера символов
            listCharNumFirst =
                new List<CharNum>(firstKey.Length);

            listCharNumSecond =
                new List<CharNum>(secondKey.Length);

            // Заполняем символами из ключей
            listCharNumFirst = FillListKey(charsFirstKey);
            listCharNumSecond = FillListKey(charsSecondKey);

            // Заполняем порядковыми номерами
            listCharNumFirst = FillingSerialsNumber(listCharNumFirst);
            listCharNumSecond = FillingSerialsNumber(listCharNumSecond);

            // Заполнение матрицы строкой пользователя
            for (int i = 0; i < listCharNumSecond.Count; i++)
            {
                for (int j = 0; j < listCharNumFirst.Count; j++)
                {
                    matrix[i, j] = charStringUser[countSymbols++];
                }
            }


            countSymbols = 0;
            // Заполнение матрицы с учетом шифрования. 
            // Переставляем столбцы по порядку следования в первом ключе. 
            // Затем переставляем строки по порядку следования во втором ключа. 
            for (int i = 0; i < listCharNumSecond.Count; i++)
            {
                for (int j = 0; j < listCharNumFirst.Count; j++)
                {
                    matrix[listCharNumSecond[i].NumberInWord,
                       listCharNumFirst[j].NumberInWord] = charStringUser[countSymbols++];
                }
            }

            for (int i = 0; i < listCharNumFirst.Count; i++)
            {
                for (int j = 0; j < listCharNumSecond.Count; j++)
                {
                    newText += matrix[j, i];
                }
            }
            return newText;
        }


        private string DecryptSpiral(string text, int key)
        {
            int i = 0, j = 0, k = 1;
            string newText = "";
            int width = key;
            int height = text.Length / width;
            char[,] array = new char[height, width];

            int N = height, M = width;
            int iBeg = 0, iFin = 0, jBeg = 0, jFin = 0;
            k = 1;
            i = 0;
            j = 0;
            while (k <= N * M)
            {
                array[i, j] = text[k - 1];
                if (i == iBeg && j < M - jFin - 1)
                    ++j;
                else if (j == M - jFin - 1 && i < N - iFin - 1)
                    ++i;
                else if (i == N - iFin - 1 && j > jBeg)
                    --j;
                else
                    --i;
                if ((i == iBeg + 1) && (j == jBeg) && (jBeg != M - iFin - 1))
                {
                    ++iBeg;
                    ++iFin;
                    ++jBeg;
                    ++jFin;
                }
                ++k;
            }
            for (i = 0; i < height; i++)
            {
                for (j = 0; j < width; j++)
                {
                    newText += array[i, j];
                }
            }
            return newText;
        }

        private string DecryptPermut(string text)
        {
            string firstKey = "elizaveta";
            // Второй ключ, количество строк
            string secondKey = "shimchenok";

            char[,] newMatrix = new char[secondKey.Length, firstKey.Length];
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            foreach (CharNum index in listCharNumFirst)
            {
                list1.Add(index.NumberInWord);
            }
            foreach (CharNum index in listCharNumSecond)
            {
                list2.Add(index.NumberInWord);
            }
            int k = 0;

            for (int i = 0; i < firstKey.Length; i++)
            {
                int x = list1.IndexOf(i);
                for (int j = 0; j < secondKey.Length; j++)
                {
                    int y = list2.IndexOf(j);
                    newMatrix[y, x] = text[k];
                    k++;
                }
            }

            string oldText = "";

            for (int i = 0; i < secondKey.Length; i++)
            {
                for (int j = 0; j < firstKey.Length; j++)
                {
                    oldText += newMatrix[i, j];
                }
            }
            return oldText;


        }

        public static int GetNumberInThealphabet(char s)
        {
            string str = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

            int number = str.IndexOf(s) / 2;

            return number;
        }

        /// <summary>
        /// Заполнение символами списка с ключом.
        /// </summary>
        /// <param name="chars">массив символов.</param>
        /// <returns>Список символов.</returns>
        private static List<CharNum> FillListKey(char[] chars)
        {
            List<CharNum> listKey = new List<CharNum>(chars.Length);

            for (int i = 0; i < chars.Length; i++)
            {
                CharNum charNum = new CharNum()
                {
                    Ch = chars[i],
                    NumberInWord = GetNumberInThealphabet(chars[i])
                };

                listKey.Add(charNum);
            }
            return listKey;
        }
        /// <summary>
        /// Отображение ключа.
        /// </summary>
        /// <param name="listCharNum">Список в котором содержатся символы с порядковыми номерами.</param>
        private static void ShowKey(List<CharNum> listCharNum, string message)
        {
            Console.WriteLine(message);

            foreach (var i in listCharNum)
            {
                Console.Write(i.Ch + " ");
            }
            Console.WriteLine();

            foreach (var i in listCharNum)
            {
                Console.Write(i.NumberInWord + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        /// <summary>
        /// Заполнение символов ключей, порядковыми номерами.
        /// </summary>
        /// <param name="listCharNum"></param>
        /// <returns></returns>
        private static List<CharNum> FillingSerialsNumber(List<CharNum> listCharNum)
        {
            int count = 0;

            var result = listCharNum.OrderBy(a =>
                a.NumberInWord);

            foreach (var i in result)
            {
                i.NumberInWord = count++;
            }

            return listCharNum;
        }
        /// <summary>
        /// Отображение матрицы.
        /// </summary>
        /// <param name="matrix">Матрица с символами.</param>
        public static void ShowMatrix(char[,] matrix, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void CleanBtn_Click(object sender, EventArgs e)
        {
            textToEncryptBox.Text = string.Empty;
        }

        private void ClearBtnDec_Click(object sender, EventArgs e)
        {
            textToDecryptBox.Text = string.Empty;
        }
    }
}
