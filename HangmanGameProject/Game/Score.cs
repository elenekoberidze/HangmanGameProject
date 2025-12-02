using HangmanGameProject.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public class Score
    {
        private static readonly string folder = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data"));

        private readonly string _scoreFile;
        public Score(string scoreFile = "scores.txt")
        {
            
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            
            _scoreFile = Path.Combine(folder, scoreFile);
        }
        public void SaveResult(Result result)
        {
            try
            {
                using var fs = new FileStream(_scoreFile, FileMode.Append, FileAccess.Write, FileShare.None);
                using var sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine($"{result.PlayerId}|{result.PlayerName}|{result.Word}|{result.Won}|{result.AttemptsLeft}|{result.Score}");
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

                    if (parts.Length < 6)
                        continue;

                    if (!int.TryParse(parts[0], out int playerId))
                        playerId = 0;

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
                    int score = int.TryParse(parts[5], out var s) ? s : 0;

                    list.Add(new Result
                    {
                        PlayerId = playerId,
                        PlayerName = playerName,
                        Word = word,
                        Won = won,
                        AttemptsLeft = attemptsLeft,
                        Score = score
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

