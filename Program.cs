using System;

namespace SpaceShip
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameService())
                game.Run();
        }
    }
}
