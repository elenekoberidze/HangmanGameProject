using HangmanGameProject.Extensions;
using HangmanGameProject.Game.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public delegate void GameUpdateHandler(string message);
    public delegate void GameEndHandler(Result result);
    public abstract class HangmanGame(Player player)
    {
        public event GameUpdateHandler? OnGameUpdate;
        public event GameEndHandler? OnGameEnd;

        protected string SecretWord { get; set; } = string.Empty;
        protected HashSet<char> GuessedLetters { get; set; } = [];
        protected int AttemptsLeft { get; set; }
        protected Player Player { get; set; } = player ?? throw new ArgumentNullException(nameof(player));

        protected ArrayList GuessHistory = [];

        public void Start(string SecretWord)
        {
            this.SecretWord = SecretWord.ToLower();
            GuessedLetters.Clear();
            GuessHistory.Clear();

            InitializeAttempts();
            OnGameUpdate?.Invoke($"Game started for {Player.Name}. Word length: {SecretWord.Length}.");
            RunMainLoop();
        }
        protected abstract void InitializeAttempts();
        protected virtual void RunMainLoop()
        {
            while (AttemptsLeft > 0 && !IsWordGuessed())
            {
                OnGameUpdate?.Invoke($"Attempts left: {AttemptsLeft}. Guessed: {string.Join(',', GuessedLetters)}");
                Console.WriteLine("Word: " + SecretWord.HideLetters(GuessedLetters));
                Console.Write("Enter letter or command ( \"quit\", \"hint\"): ");
                var input = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a letter or command.");
                    continue;
                }
                if (input == "quit")
                {
                    OnGameUpdate?.Invoke("Player chose to quit");
                    break;
                }
                


                if (input == "hint")
                {
                    ProvideHint();
                    continue;
                }


                if (input == "stats")
                {
                    OnGameUpdate?.Invoke("Requesting stats...");
                    continue;
                }
                if (input == "delete_scores")
                {
                    new Score().DeleteScores();
                    Console.WriteLine("Scores deleted (if existed).");
                    continue;
                }
                if (input.Length != 1 || !input[0].IsLetter())
                {
                    Console.WriteLine("Please enter a single alphabet letter.");
                    continue;
                }
                char letter = input[0];



                if (GuessedLetters.Contains(letter))
                {
                    Console.WriteLine("Already guessed that letter.");
                    continue;
                }

                GuessHistory.Add(letter);
                GuessedLetters.Add(letter);

                if (SecretWord.Contains(letter))
                {
                    OnGameUpdate?.Invoke($"Good! The word contains '{letter}'.");
                }
                else
                {
                    AttemptsLeft--;
                    OnGameUpdate?.Invoke($"Wrong guess '{letter}'. Attempts now {AttemptsLeft}.");

                }

            }
            var result = new Result
            {
                PlayerName = Player.Name,
                Word = SecretWord,
                Won = IsWordGuessed(),
                AttemptsLeft = AttemptsLeft,
                PlayerAt = DateTime.UtcNow
            };
            OnGameEnd?.Invoke(result);

        }
        protected bool IsWordGuessed()
        {
            return SecretWord.All(x => GuessedLetters.Contains(x));
        }
        protected void ProvideHint()
        {
            var unguessed = SecretWord.Where(x => !GuessedLetters.Contains(x));
            var unguessedList = new List<char>(unguessed);

            if (unguessedList.Count == 0)
            {
                Console.WriteLine("No hints available, word already revealed.");
                return;
            }
            var rnd = new Random();
            char hint = unguessedList[rnd.Next(0, unguessedList.Count)];
            Console.WriteLine($"Hint letter revealed: {hint}");
            AttemptsLeft = Math.Max(0, AttemptsLeft - 1);
        }
    } 
}

