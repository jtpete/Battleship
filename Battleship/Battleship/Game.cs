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
        bool gameWon = false;
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
            startTime = DateTime.Now;
            player1.SetTurn(true);
            while (!quitGame && !gameWon)
            {
                if (player1.IsTurn())
                {
                    Console.Clear();
                    PlayMenu(player1, player2);
                    PlayMenuResponse(player1, player2);
                }
                else
                {
                    Console.Clear();
                    PlayMenu(player2, player1);
                    PlayMenuResponse(player2, player1);
                }
                player1.ChangeTurn();
                player2.ChangeTurn();
            }
            player1.ChangeTurn();
            player2.ChangeTurn();
        }
        public void ConcludedGame()
        {
            if (player1.IsTurn())
            {
                Console.WriteLine($"{player1.GetName()} WINS!");
            }
            else
            {
                Console.WriteLine($"{player2.GetName()} WINS!");
            }
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
        private void PlayMenu(Player currentPlayer, Player opponent)
        {
            Console.WriteLine($"{currentPlayer.GetName()}'s Turn.");
            Console.WriteLine("----------------------------");
            Console.WriteLine($"1.  Pick a point to shoot at.");
            Console.WriteLine($"2.  Look at {currentPlayer.GetName()}'s Gameboard.");
            Console.WriteLine($"3.  Look at {opponent.GetName()} Gameboard.");
            Console.WriteLine($"4.  Quit");
        }

        private void PlayMenuResponse(Player currentPlayer, Player opponent)
        {
            string response = Console.ReadLine();
            switch (response)
            {
                case "1":
                    Console.WriteLine($"{currentPlayer.myGameBoard.FireShot(opponent.GetGameBoard())}");
                    if (currentPlayer.myGameBoard.DidWin(opponent.GetGameBoard()))
                    {
                        gameWon = true;
                    }
                    else
                    {
                        Console.WriteLine($"Hit Enter for {opponent.GetName()}'s turn. ");
                    }
                    Console.ReadLine();
                    break;
                case "2":
                    Console.Clear();
                    currentPlayer.myGameBoard.PrintGameBoard(currentPlayer);
                    PlayMenu(currentPlayer, opponent);
                    PlayMenuResponse(currentPlayer, opponent);
                    break;
                case "3":
                    Console.Clear();
                    opponent.myGameBoard.PrintGameBoard(opponent, true);
                    PlayMenu(currentPlayer, opponent);
                    PlayMenuResponse(currentPlayer, opponent);
                    break;
                case "4":
                    quitGame = true;
                    break;
                case "9":
                    currentPlayer.myGameBoard.NuclearOption(opponent.GetGameBoard());
                    Console.Clear();
                    opponent.myGameBoard.PrintGameBoard(opponent, true);
                    PlayMenu(currentPlayer, opponent);
                    PlayMenuResponse(currentPlayer, opponent);
                    break;
                default:
                    Console.Clear();
                    PlayMenu(currentPlayer, opponent);
                    PlayMenuResponse(currentPlayer, opponent);
                    break;
            }
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
