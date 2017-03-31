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
        public bool gameBoardSet = false;
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
                    if(myDestroyer.ShipOnLocation(x,y) && x != 0 && y != 0)
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
        public void PutShipsOnBoard(Player player)
        {
            PrintGameBoard(player);
            ShipPlacementMenu();
            string response = Console.ReadLine();
            switch(response)
            {
                case "1":
                    myDestroyer.PlaceShip(mySubmarine, myBattleship, myAircraftCarrier);
                    PutShipsOnBoard(player);
                    break;
                case "2":
                    mySubmarine.PlaceShip(myDestroyer, myBattleship, myAircraftCarrier);
                    PutShipsOnBoard(player);
                    break;
                case "3":
                    myBattleship.PlaceShip(myDestroyer, mySubmarine, myAircraftCarrier);
                    PutShipsOnBoard(player);
                    break;
                case "4":
                    myAircraftCarrier.PlaceShip(myDestroyer, mySubmarine, myBattleship);
                    PutShipsOnBoard(player);
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

    }
  
}
