using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TaskenAtividade
{
    public class App
    {
        private App instance;
        static void Main(string[] args)
        {
            App instance = new App();
            instance.Pyramid();
            instance.Compress(Console.ReadLine());
            instance.PositionNumbers();
            instance.TextRead();

        }

        private void Pyramid()
        {
            Console.WriteLine("Informe um número:");

            int value = 0;
            try
            {
                value = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (value <= 0)
                {
                    Console.WriteLine("Valores iguais ou inferiores a zero não são permitidos.");
                }

                for (int i = value; i > 0; i--)
                {
                    for (int j = i; j > 0; j--)
                    {
                        Console.Write(Math.Pow(j, 2) + " ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Compress(string value)
        {
            char current = value[0];

            List<Pair> result = new List<Pair>();

            for (int i = 1, count = 1; i < value.Length; i++, count++)
            {
                if (!current.Equals(value[i]))
                {
                    Pair temp = new Pair(current, count);
                    result.Add(temp);
                    current = value[i];
                    count = 0;
                }
                if (i == value.Length - 1)
                {
                    Pair temp = new Pair(current, count + 1);
                    result.Add(temp);
                }
            }
            foreach (Pair e in result)
            {
                Console.Write(e.Value + "" + e.Amount);
            }
            Console.WriteLine();
        }

        public void PositionNumbers()
        {
            int value = 1;
            List<float> numbers = new List<float>();
            int bigger = 0;
            int smaller = -1;

            try
            {
                while (value != 0)
                {
                    value = Int32.Parse(Console.ReadLine());
                    if (value < 0)
                    {
                        continue;
                    }
                    if (value > bigger)
                    {
                        bigger = value;
                    }
                    if (value % 2 != 0)
                    {
                        if (smaller < 0)
                        {
                            smaller = value;
                        }
                        else if (value < smaller)
                        {
                            smaller = value;
                        }
                    }
                    if (value > 0)
                    {
                        numbers.Add(value);
                    }
                }
                Console.WriteLine("Quantidade de números lidos: " + numbers.Count);
                Console.WriteLine("O maior números lido: " + bigger);
                Console.WriteLine("A media dos números lido: " + numbers.Sum() / numbers.Count);
                LeftSmallest(smaller);
                RepeatCount(numbers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LeftSmallest(int smaller)
        {
            if (smaller > 0)
            {
                Console.WriteLine("O menor números impar lido: " + smaller);
            }
            else
            {
                Console.WriteLine("Não houve ocorrencia de números impares.");
            }
        }

        private void RepeatCount(List<float> numbers)
        {
            IEnumerable<float> query = from number in numbers orderby number select number;
            float[] list = query.ToArray();
            float current = query.ToArray<float>()[0];
            for (int i = 0, count = 0; i < list.Count(); i++, count++)
            {
                if (list[i] != current)
                {
                    Console.WriteLine("O número " + current + " ocorreu " + count + " vezes.");
                    current = list[i];
                    count = 0;
                }
                if (i == list.Length - 1)
                {
                    Console.WriteLine("O número " + list[i] + " ocorreu " + (count + 1) + " vezes.");
                }
            }
        }

        public void TextRead()
        {
            try
            {
                StreamReader sr = new StreamReader(Console.ReadLine());
                string line = sr.ReadLine();
                int lineCountConsonants = 0;
                int lineCountVowels = 0;
                int moreConsonants = -1;
                int moreVowels = -1;

                for (int i =0  ; line != null; i++)
                {
                    MatchCollection consonants = Regex.Matches(line, "[^aeiou]");
                    MatchCollection vowels = Regex.Matches(line, "[aeiou]");

                    if (consonants.Count > moreConsonants)
                    {
                        moreConsonants = consonants.Count;
                        lineCountConsonants = i; 
                    }
                    if (vowels.Count > moreVowels)
                    {
                        moreVowels = vowels.Count;
                        lineCountVowels = i;
                    }


                    line = sr.ReadLine();
                }
                Console.WriteLine("A linha " + lineCountVowels + " possui mais vogais.");
                Console.WriteLine("A linha " + lineCountConsonants + " possui mais consoantes.");
                sr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    public struct Pair
    {
        public int Amount { get; set; }
        public char Value { get; set; }

        public Pair(char current, int count)
        {
            this.Value = current;
            this.Amount = count;
        }

    }

}
