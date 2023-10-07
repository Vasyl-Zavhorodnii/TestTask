using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TestTaskCruxLab
{
    internal class Program
    {
        static bool IsValidPassword(string requirement, string password)
        {
            var match = Regex.Match(requirement, @"([a-z]) (\d+-\d+): (.+)");
            if (match.Success)
            {
                char character = match.Groups[1].Value[0];
                string range = match.Groups[2].Value;
                string pw = match.Groups[3].Value;

                var rangeParts = range.Split('-');
                int minCount = int.Parse(rangeParts[0]);
                int maxCount = int.Parse(rangeParts[1]);

                int charCount = pw.Split(character).Length - 1;

                return charCount >= minCount && charCount <= maxCount;
            }
            return false;
        }
        static void Main(string[] args)
        {
            string fileName = "file.txt";

            // Перевірка наявності файлу і створення, якщо він не існує
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
            int validPasswordsCount = 0;

            using (StreamReader reader = new StreamReader("file.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (IsValidPassword(line, line))
                    {
                        validPasswordsCount++;
                    }
                }
            }

            Console.WriteLine("Кількість валідних паролів: " + validPasswordsCount);
            Console.ReadKey();
        }
    }
    
}
