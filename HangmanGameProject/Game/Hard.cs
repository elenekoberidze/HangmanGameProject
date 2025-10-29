using HangmanGameProject.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public class Hard : HangmanGame
    {
        public Hard(Player player): base(player) { }
        protected override void InitializeAttempts()
        {
            AttemptsLeft = Math.Max(3, SecretWord.Length + 2);
        }
    }
}
