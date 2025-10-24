using HangmanGameProject.Game;
using HangmanGameProject.Game.Models;


internal class Program
{
    private static void Main()
    {
        var manager = new GameManager();
        manager.Run();
    }
}