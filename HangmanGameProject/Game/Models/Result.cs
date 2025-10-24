using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game.Models
{
    public class Result
    {
        public string PlayerName { get; set; }
        public string Word { get; set; }
        public bool Won { get; set; }
        public int AttemptsLeft { get; set; }
        public DateTime PlayedAt { get; set; }
        public DateTime PlayerAt { get; internal set; }
    }
}
