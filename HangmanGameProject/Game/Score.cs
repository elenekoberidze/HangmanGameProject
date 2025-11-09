using HangmanGameProject.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public class Score(string scoreFile = "scores.txt")
    {
        private readonly string _scoreFile = scoreFile;
        private readonly string? line;

        public void SaveResult(Result result)
        {
            try
            {
                using var fs = new FileStream(_scoreFile, FileMode.Append, FileAccess.Write, FileShare.None);
                using var sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine($"{result.PlayerName}|{result.Word}|{result.Won}|{result.AttemptsLeft}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ScoreManager] Could not write score: " + ex.Message);
            }
        }
        public List<Result> LoadAllResults()
        {
            var list = new List<Result>();

            try
            {
                if (!File.Exists(_scoreFile))
                    return list;

                using var fs = new FileStream(_scoreFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs, Encoding.UTF8);
                string? line;
                while ((line = sr.ReadLine()) != null)
                {

                    var parts = line.Split('|');

                    if (parts.Length < 5)
                        continue;


                    if (!DateTime.TryParse(parts[0], out DateTime playedAt))
                        continue;

                    var playerName = parts[1];
                    var word = parts[2];


                    bool won = false;
                    if (!bool.TryParse(parts[3], out won))
                    {

                        won = false;
                    }

                    int attemptsLeft = 0;
                    if (!int.TryParse(parts[4], out attemptsLeft))
                    {

                        attemptsLeft = 0;
                    }


                    list.Add(new Result
                    {
                        PlayerName = playerName,
                        Word = word,
                        Won = won,
                        AttemptsLeft = attemptsLeft
                    });
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("[ScoreManager] Could not read scores: " + ex.Message);
            }

            return list;
        }

        public void DeleteScores()
        {
            try
            {
                if (File.Exists(_scoreFile))
                    File.Delete(_scoreFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ScoreManager] Could not delete score file: " + ex.Message);
            }
        }
    }
}

