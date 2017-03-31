using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class GameBoard
    {
        Destroyer myDestroyer = new Destroyer();
        Submarine mySubmarine = new Submarine();
        Battleship myBattleship = new Battleship();
        AircraftCarrier myAircraftCarrier = new AircraftCarrier();
        bool gameBoardReady = false;
        string[,] board = new string[21, 21];
        public GameBoard()
        {

            for(int x = 0; x < board.GetLength(0); x++)
            {
                for(int y = 0; y < board.GetLength(0); y++)
                {
                    if (y == 0 && x > 0) 
                    {
                        if(x < 10)
                        {
                            board[x, y] = x.ToString() + "  ";
                        }
                        else
                        {
                            board[x, y] = x.ToString() + " ";
                        }
                    }
                    else if (x == 0 && y > 0)
                    {
                        if(y < 10)
                        {
                            board[x, y] = y.ToString() + "  ";
                        }
                        else
                        {
                             board[x, y] = y.ToString() + " ";
                        }
                    }
                    else if (x == 0 && y == 0)
                    {
                        board[x, y] = "   ";
                    }
                    else
                    {
                        board[x, y] = "~  ";
                    }
                }
            }
        }
        public void PrintGameBoard(Player player)
        {
            
            Console.Clear();
            string boat = "\u2588  ";
            for (int y = board.GetLength(0)-1; y >= 0; y--)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    if (board[x, y] == "X  ")
                    {
                        Console.Write(board[x, y]);
                    }
                    else if (myDestroyer.ShipOnLocation(x,y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else if(mySubmarine.ShipOnLocation(x,y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else if (myBattleship.ShipOnLocation(x, y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else if (myAircraftCarrier.ShipOnLocation(x, y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else
                    {
                        Console.Write(board[x, y]);
                    }
                    if(x+1 == board.GetLength(0))
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
        public void PrintGameBoard(Player player, bool opponentBoard)
        {

            Console.Clear();
            string boat = "\u2588  ";
            for (int y = board.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    if (board[x, y] == "X  ")
                    {
                        Console.Write(board[x, y]);
                    }
                    else if (!opponentBoard && myDestroyer.ShipOnLocation(x, y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else if (!opponentBoard && mySubmarine.ShipOnLocation(x, y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else if (!opponentBoard && myBattleship.ShipOnLocation(x, y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else if (!opponentBoard && myAircraftCarrier.ShipOnLocation(x, y) && x != 0 && y != 0)
                    {
                        Console.Write(boat);
                    }
                    else
                    {
                        Console.Write(board[x, y]);
                    }
                    if (x + 1 == board.GetLength(0))
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        private int[,] GetShotCoordinates()
        {
            int[,] shot = new int[1,2];
            Console.WriteLine($"Where would you like to shoot?");
            Console.WriteLine($"Pleae give the x coordinate:");
            string x1 = Console.ReadLine();
            Console.WriteLine($"Pleae give the y coordinate:");
            string y1 = Console.ReadLine();
            if(CoordinateIsInteger(x1) && CoordinateIsInteger(y1))
            {
                int x = Convert.ToInt16(x1);
                int y = Convert.ToInt16(y1);
                shot[0, 0] = x;
                shot[0, 1] = y;
            }
            else
            {
                Console.WriteLine("Sorry, doesn't seem to be a valid shot.  Please try again.");
                shot = GetShotCoordinates();
            }
            return shot;
        }

        private bool IsHit(int[,] shot, GameBoard oppGameBoard)
        {
            if (oppGameBoard.myDestroyer.ShipHit(shot) ||
                oppGameBoard.mySubmarine.ShipHit(shot) ||
                oppGameBoard.myBattleship.ShipHit(shot) ||
                oppGameBoard.myAircraftCarrier.ShipHit(shot))
            {
                return true;
            }
            return false;
        }

        private string WhatWasSunk(int[,] shot, GameBoard oppGameBoard)
        {
            string ship = "Destroyer";
            if (oppGameBoard.myDestroyer.ShipHit(shot))
            {
                ship = "Destroyer";
            }
            else if(oppGameBoard.mySubmarine.ShipHit(shot))
            {
                ship = "Submarine";
            }
            else if (oppGameBoard.myBattleship.ShipHit(shot))
            {
                ship = "Battleship";
            }
            else if(oppGameBoard.myAircraftCarrier.ShipHit(shot))
            {
                ship = "Aircraft Carrier";
            }
            return ship;
        }
        private bool ApplyHit(int[,] shot, GameBoard oppGameBoard)
        {
            bool didSink = false;
            if (oppGameBoard.myDestroyer.ShipHit(shot))
            {
                didSink = oppGameBoard.myDestroyer.SetHit(shot);
            }
            else if (oppGameBoard.mySubmarine.ShipHit(shot))
            {
                didSink = oppGameBoard.mySubmarine.SetHit(shot);
            }
            else if (oppGameBoard.myBattleship.ShipHit(shot))
            {
                didSink = oppGameBoard.myBattleship.SetHit(shot);
            }
            else if (oppGameBoard.myAircraftCarrier.ShipHit(shot))
            {
                didSink = oppGameBoard.myAircraftCarrier.SetHit(shot);
            }
            return didSink;

        }


        public string FireShot(GameBoard oppGameBoard )
        {
            bool didSink = false;
            int[,] shot = new int[1, 1];
            string shotResponse = "\n|----------MISS-----------|\n";
            shot = GetShotCoordinates();
            oppGameBoard.board[shot[0, 0], shot[0, 1]] = "O  ";
            if (IsHit(shot, oppGameBoard))
            {
                shotResponse = "\nCongratulations - HIT!!!\n";
                oppGameBoard.board[shot[0, 0], shot[0, 1]] = "X  ";
                didSink = ApplyHit(shot, oppGameBoard);
                if (didSink)
                {
                    shotResponse = "\nYou sunk the: " + WhatWasSunk(shot, oppGameBoard);
                    if(DidWin(oppGameBoard))
                    {
                        shotResponse = "\nWinner, Winner, Chicken Dinner!!\n";
                    }
                }
            }
            return shotResponse;
        }
        public bool DidWin(GameBoard oppGameBoard)
        {
            if(oppGameBoard.myDestroyer.GetSunk() &&
                oppGameBoard.mySubmarine.GetSunk() &&
                oppGameBoard.myBattleship.GetSunk() &&
                oppGameBoard.myAircraftCarrier.GetSunk())
            {
                return true;
            }
            return false;
        }
        private bool CoordinateIsInteger(string coordinate)
        {        {
            try
            {
                int x = Convert.ToInt16(coordinate);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
        public void PutShipsOnBoard(Player player)
        {
            PrintGameBoard(player);
            ShipPlacementMenu();
            string response = Console.ReadLine();
            switch(response)
            {
                case "1":
                    gameBoardReady = false;
                    myDestroyer.PlaceShip(mySubmarine, myBattleship, myAircraftCarrier);
                    PutShipsOnBoard(player);
                    break;
                case "2":
                    gameBoardReady = false;
                    mySubmarine.PlaceShip(myDestroyer, myBattleship, myAircraftCarrier);
                    PutShipsOnBoard(player);
                    break;
                case "3":
                    gameBoardReady = false;
                    myBattleship.PlaceShip(myDestroyer, mySubmarine, myAircraftCarrier);
                    PutShipsOnBoard(player);
                    break;
                case "4":
                    gameBoardReady = false;
                    myAircraftCarrier.PlaceShip(myDestroyer, mySubmarine, myBattleship);
                    PutShipsOnBoard(player);
                    break;
                case "9":
                    gameBoardReady = false;
                    string res = Console.ReadLine();
                        switch(res)
                        {
                        case "1":
                            myAircraftCarrier.SetLocation(3, 3, 3, 7);
                            myBattleship.SetLocation(10, 10, 13, 10);
                            mySubmarine.SetLocation(16,16,16,18);
                            myDestroyer.SetLocation(3, 15, 3, 16);
                            break;
                        case "2":
                            myAircraftCarrier.SetLocation(10, 10, 14, 10);
                            myBattleship.SetLocation(3, 3, 3, 6);
                            mySubmarine.SetLocation(16, 16, 18, 16);
                            myDestroyer.SetLocation(18, 2, 18, 3);
                            break;
                        case "3":
                            myAircraftCarrier.SetLocation(18, 7, 18, 11);
                            myBattleship.SetLocation(3, 3, 6, 3);
                            mySubmarine.SetLocation(10, 10, 10, 12);
                            myDestroyer.SetLocation(4, 17, 4, 18);
                            break;
                    }
                    break;
                case "5":
                    break;
                default:
                    PutShipsOnBoard(player);
                    break;
            }
        }
        public void ShipPlacementMenu()
        {
            Console.WriteLine("          Which Boat?       ");
            Console.WriteLine("----------------------------");
            Console.WriteLine($"1.  Destroyer");
            Console.WriteLine($"2.  Submarine");
            Console.WriteLine($"3.  Battleship");
            Console.WriteLine($"4.  Aircraft Carrier");
            Console.WriteLine($"5.  Done");
        }
        public string VerifyGameBoardReady()
        {
            string boardReadyResponse = "Still need to set: ";
            if (!myDestroyer.GetLocationIsSet())
            {
                boardReadyResponse += "Destroyer ";
            }
            if(!mySubmarine.GetLocationIsSet())
            {
                boardReadyResponse += "Submarine ";
            }
            if (!myBattleship.GetLocationIsSet())
            {
                boardReadyResponse += "Battleship ";
            }
            if (!myAircraftCarrier.GetLocationIsSet())
            {
                boardReadyResponse += "Aircraft Carrier ";
            }
            if(myDestroyer.GetLocationIsSet() && mySubmarine.GetLocationIsSet() && myBattleship.GetLocationIsSet() && myAircraftCarrier.GetLocationIsSet())
            {
                boardReadyResponse = "All are WET and READY!";
                gameBoardReady = true;
            }
            return boardReadyResponse;
        }
        public bool IsGameBoardReady()
        {
            return gameBoardReady;
        }
        public void NuclearOption(GameBoard opponent)
        {
            int[,] subLoc = opponent.mySubmarine.GetLocation();
            int[,] batLoc = opponent.myBattleship.GetLocation();
            int[,] airLoc = opponent.myAircraftCarrier.GetLocation();
            int[,] desLoc = opponent.myDestroyer.GetLocation();
            UpdateOpponentBoard(opponent, desLoc, subLoc, batLoc, airLoc);

            opponent.myAircraftCarrier.SetAsSunk();
            opponent.myBattleship.SetAsSunk();
            opponent.mySubmarine.SetAsSunk();

            opponent.myAircraftCarrier.NuclearHits();
            opponent.myBattleship.NuclearHits();
            opponent.mySubmarine.NuclearHits();
            opponent.myDestroyer.NuclearHits();

        }

        private void UpdateOpponentBoard(GameBoard opponent, int[,] desLoc, int[,] subLoc, int[,] batLoc, int[,] airLoc)
        {
            for(int x = 0; x < subLoc.GetLength(0); x++)
            {
                opponent.board[subLoc[x, 0], subLoc[x, 1]] = "X  ";
                
            }
            for (int x = 0; x < batLoc.GetLength(0); x++)
            {
                opponent.board[batLoc[x, 0], batLoc[x, 1]] = "X  ";

            }
            for (int x = 0; x < airLoc.GetLength(0); x++)
            {
                opponent.board[airLoc[x, 0], airLoc[x, 1]] = "X  ";
            }
            opponent.board[desLoc[0, 0], desLoc[0, 1]] = "X  ";
        }

        private void UpdateOpponentShipHit(GameBoard opponent, int[,] desLoc, int[,] subLoc, int[,] batLoc, int[,] airLoc)
        {
            for (int x = 0; x < subLoc.GetLength(0); x++)
            {
                opponent.board[subLoc[x, 0], subLoc[x, 1]] = "X  ";

            }
            for (int x = 0; x < batLoc.GetLength(0); x++)
            {
                opponent.board[batLoc[x, 0], batLoc[x, 1]] = "X  ";

            }
            for (int x = 0; x < airLoc.GetLength(0); x++)
            {
                opponent.board[airLoc[x, 0], airLoc[x, 1]] = "X  ";
            }
            opponent.board[desLoc[0, 0], desLoc[0, 1]] = "X  ";
        }

    }
  
}
