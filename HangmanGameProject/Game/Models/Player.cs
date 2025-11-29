using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game.Models
{
    public class Player : BaseEntity
    {
        private static int nextId = 1;
        public string? Name { get;  set; }
        public int Score { get; set; }
        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Player name cannot be empty!");
            }
            this.Id = nextId++;
            this.Name = name.Trim();
            this.Score = 0;
        }

        public Player() { }
        

        public override string ToString() => $"Id: {Id}, {Name} Score: {Score}";
        
    }
}
