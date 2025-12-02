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
        private readonly Provider wordProvider = new();
        private readonly Score scoreManager = new();
        private Player player;

        public void Run()
        {
            
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine() ?? "Unknown";

            player = new Player(name);

            Console.WriteLine("Do you have your Player ID? (y/n)");
            var key1 = Console.ReadKey(intercept: true);
            Console.WriteLine();

            if (key1.KeyChar == 'y' || key1.KeyChar == 'Y')
            {
                Console.WriteLine("Enter your Player ID:");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                  
                    var results = scoreManager.LoadAllResults();
                    var existing = results.FirstOrDefault(r => r.PlayerId == id);

                    if (existing != null)
                    {
                        player = new Player(existing.PlayerName, existing.PlayerId, existing.Score);
                        Console.WriteLine($"\nWelcome back, {player.Name}! Your score: {player.Score}");
                    }
                    else
                    {
                        Console.WriteLine("ID not found. Creating new player.");
                        player = new Player(name);
                        Console.WriteLine($"\nNew player created. Your ID is: {player.Id}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID. Creating new player.");
                    player = new Player(name);
                    Console.WriteLine($"\nNew player created. Your ID is: {player.Id}");
                }
            }
            else
            {
               
                player = Player.LoadOrCreatePlayer(name, scoreManager);
                Console.WriteLine($"\nWelcome, {player.Name}! Your ID is: {player.Id}, Score: {player.Score}");
            }


            

            Console.WriteLine("Choose difficulty: 1) Easy 2) Hard");
            var key = Console.ReadKey(intercept: true);
            Console.WriteLine();

            HangmanGame game = (key.KeyChar == '2') ? new Hard(player) : new Easy(player);

           
            game.OnGameUpdate += msg => Console.WriteLine("[Update] " + msg);
            game.OnGameEnd += result =>
            {
                Console.WriteLine("--- Game Over ---");
                Console.WriteLine($"Player ID: {result.PlayerId}");
                Console.WriteLine($"Player: {result.PlayerName}");
                Console.WriteLine($"Word: {result.Word}");
                Console.WriteLine($"Won: {result.Won}");
                Console.WriteLine($"Attempts left: {result.AttemptsLeft}");

                if (result.Won) player.Score += 10 + result.AttemptsLeft;
                result.Score = player.Score;

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
            if (allResults.Count != 0)
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
