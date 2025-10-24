using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public class Provider
    {
        private readonly string _filePath;
        private static readonly Random rnd = new Random();

        public Provider(string filePath = "Data/words.txt")
        {
            _filePath = filePath;
        }

        public string[] GetAllWords()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var lines = File.ReadAllLines(_filePath)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => x.Trim().ToLower())
                        .ToArray();

                    if (lines.Length > 0)
                        return lines;
                }
                else
                {
                    Console.WriteLine($"[Provider] Warning: file not found at {_filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Provider] Warning: could not read words file: " + ex.Message);
            }

            return new string[] { "elene", "pear", "apple", "banana" };
        }

        public string GetRandomWord()
        {
            var words = GetAllWords();
            return words[rnd.Next(words.Length)];
        }
    }
}