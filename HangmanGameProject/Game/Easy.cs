using HangmanGameProject.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game
{
    public class Easy : HangmanGame
    {
        public Easy(Player player) : base(player) { }
        protected override void initializeAttempts()
        {
            AttemptsLeft = SecretWord.Length + 5;
        }
    }
}
