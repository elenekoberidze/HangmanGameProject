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
        private static readonly string folder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data"));
        private static readonly string idFile = Path.Combine(folder, "player_id.txt");
        public static Player LoadOrCreatePlayer(string name,Score scoreManager)
        {
            var results = scoreManager.LoadAllResults();
            var latest = results
        .Where(r => r.PlayerName == name)
        .OrderByDescending(r => r.Score)   
        .FirstOrDefault();

            if (latest != null)
            {
                return new Player(latest.PlayerName, latest.PlayerId, latest.Score);
            }
            else
            {
                return new Player(name);
            }
        }
        public Player(string name, int id, int score)
        {
            this.Id = id;
            this.Name = name.Trim();
            this.Score = score;
            if (id >= nextId)
                nextId = id + 1;

            SaveNextId();
        }


        static Player()
        {

            if (!Directory.Exists(folder)) {  Directory.CreateDirectory(folder); }
               


            if (File.Exists(idFile))
            {
                if (!int.TryParse(File.ReadAllText(idFile), out nextId))
                    nextId = 1;
            }
            else
            {
                nextId = 1;
            }
        }
        private static void SaveNextId()
        {
            if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }
                

            File.WriteAllText(idFile, nextId.ToString());
        }

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Player name cannot be empty!");
                
            }
            this.Id = nextId++;
            this.Name = name.Trim();
            this.Score = 0;
            SaveNextId();
        }

     
        public override string ToString() => $"Id: {Id}, Name: {Name}, Score: {Score}";
    }
}
