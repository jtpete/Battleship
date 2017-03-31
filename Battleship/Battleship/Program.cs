using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            string response = "yes";
            do
            {
                Game myGame = new global::Battleship.Game();
                myGame.StartGame();
                if (!myGame.QuitGame())
                {
                    myGame.PlayGame();
                }
                myGame.ConcludedGame();
                Console.WriteLine("What to play again? Yes or No");
                response = Console.ReadLine().ToLower();
            } while (response == "yes");
        }
    }
}
