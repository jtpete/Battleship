using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Game
    {
        Player player1 = new Player("Player1");
        Player player2 = new Player("Player2");
        bool quitGame = false;
        bool gameSetupComplete = false;
        private DateTime startTime;
        private DateTime endTime;

        public void StartGame()
        {
            while (!quitGame && !gameSetupComplete)
            {
                StartMenu();
                StartMenuResponse();
            }
        
        }
        public void PlayGame()
        {
            //Main Menu
        }
        public void ConcludedGame()
        {
            //Display Results - winner and time length of game
            Console.WriteLine("\nGame Done!\n");
        }
        private void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("LET'S START SOME BATTLESHIP!");
            Console.WriteLine("----------------------------");
            Console.WriteLine($"1.  Change name for {player1.GetName()}");
            Console.WriteLine($"2.  Setup ships for {player1.GetName()}");
            Console.WriteLine($"3.  Change name for {player2.GetName()}");
            Console.WriteLine($"4.  Setup ships for {player2.GetName()}");
            Console.WriteLine($"5.  Let's Play!");
            Console.WriteLine($"6.  Quit");

        }
        private void StartMenuResponse()
        {
            string response = Console.ReadLine();
            switch (response)
            {
                case "1":
                    player1.GetNameFromPlayer();
                    break;
                case "2":
                    player1.myGameBoard.PutShipsOnBoard(player1);
                    break;
                case "3":
                    player2.GetNameFromPlayer();
                    break;
                case "4":
                    player2.myGameBoard.PutShipsOnBoard(player2);
                    break;
                case "5":
                    VerifyBoards();
                    break;
                case "6":
                    quitGame = true;
                    break;
                default:
                    StartMenu();
                    break;      
            }
        }
        private void PlayMenu()
        {
            //start menu 
        }
        public bool QuitGame()
        {
            return quitGame;
        }
        private void VerifyBoards()
        {
            string messageP1 = player1.myGameBoard.VerifyGameBoardReady();
            Console.WriteLine($"\n{player1.GetName()} - {messageP1}");
            string messageP2 = player2.myGameBoard.VerifyGameBoardReady();
            Console.WriteLine($"\n{player2.GetName()} - {messageP2}");
            if (player1.myGameBoard.IsGameBoardReady() && player2.myGameBoard.IsGameBoardReady())
            {
                Console.WriteLine("Ready to begin and shoot something? yes or no" );
                if(Console.ReadLine().ToLower() == "yes")
                {
                    gameSetupComplete = true;
                }

            }
            else
            {
                Console.WriteLine("Let's get all ships in the water first. \n\nHit ENTER to continue.");
                Console.ReadLine();
            }

        }
    }   
}
