using HangmanGameProject.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public class Easy(Player player) : HangmanGame(player)
    {
        protected override void InitializeAttempts()
        {
            AttemptsLeft = SecretWord.Length + 5;
        }
    }
}
