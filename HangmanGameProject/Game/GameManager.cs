using HangmanGameProject.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public class GameManager
    {
        private readonly Provider wordProvider = new Provider();
        private readonly Score scoreManager = new Score();
        private readonly Player player = new Player();

        public void Run()
        {
            
            Console.WriteLine("Enter your name:");
            player.Name = Console.ReadLine() ?? "Unknown";

            
            Console.WriteLine("Choose difficulty: 1) Easy 2) Hard");
            var key = Console.ReadKey(intercept: true);
            Console.WriteLine();

            HangmanGame game = (key.KeyChar == '2') ? new Hard(player) : new Easy(player);

           
            game.OnGameUpdate += msg => Console.WriteLine("[Update] " + msg);
            game.OnGameEnd += result =>
            {
                Console.WriteLine("--- Game Over ---");
                Console.WriteLine($"Player: {result.PlayerName}");
                Console.WriteLine($"Word: {result.Word}");
                Console.WriteLine($"Won: {result.Won}");
                Console.WriteLine($"Attempts left: {result.AttemptsLeft}");

                if (result.Won) player.Score += 10 + result.AttemptsLeft;

                scoreManager.SaveResult(result);
                Console.WriteLine($"New player score: {player.Score}");
            };

            
            bool keepPlaying = true;
            while (keepPlaying)
            {
                string secret = wordProvider.GetRandomWord();
                if (secret.Length < 3)
                {
                    var candidates = wordProvider.GetAllWords().Where(w => w.Length >= 3).ToArray();
                    if (candidates.Length > 0)
                        secret = candidates[new Random().Next(candidates.Length)];
                }

                game.Start(secret);

                Console.WriteLine("Play again? (y/n)");
                var k = Console.ReadKey(intercept: true);
                Console.WriteLine();
                keepPlaying = k.KeyChar == 'y' || k.KeyChar == 'Y';
            }

            
            var allResults = scoreManager.LoadAllResults();
            if (allResults.Any())
            {
                Console.WriteLine("\n--- Score Summary ---");

                var top = allResults
                    .GroupBy(r => r.PlayerName)
                    .Select(g => new { Player = g.Key, Wins = g.Count(r => r.Won), Games = g.Count() })
                    .OrderByDescending(x => x.Wins)
                    .Take(3);

                foreach (var t in top)
                    Console.WriteLine($"{t.Player}: Wins={t.Wins}, Games={t.Games}");
            }

            Console.WriteLine("Thanks for playing! Goodbye.");
        }
    }
}
