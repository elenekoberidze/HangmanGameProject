using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Game.Models
{
    public class Player : BaseEntity
    {
        public string? Name { get;  set; }
        public int Score { get; set; }
        private static int nextId = 1;
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

        public override string ToString() => $"Id: {Id}, Name: {Name}, Score: {Score}";
    }
}
