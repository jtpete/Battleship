using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Submarine : Ship
    {
        public int[,] location = new int[3, 2];
        public Submarine()
        {
            name = "Submarine";
            size = 3;
        }
        public void PlaceShip(Destroyer destroyer, Battleship battleship, AircraftCarrier aircraftCarrier)
        {
            locationSet = false;
            Console.WriteLine($"Where would you like to place your {name}?");
            Console.WriteLine($"This ship requires {size} spots.  Pleae give the first spots x coordinate:");
            string x1 = Console.ReadLine();
            Console.WriteLine($"Now, pleae give the first spots y coordinate:");
            string y1 = Console.ReadLine();
            Console.WriteLine($"This ship requires {size} spots.  Pleae give the last spots x coordinate:");
            string x2 = Console.ReadLine();
            Console.WriteLine($"Now, pleae give the last spots y coordinate:");
            string y2 = Console.ReadLine();
            if (!GoodCoordinates(x1, y1, x2, y2, destroyer, battleship, aircraftCarrier))
            {
                Console.WriteLine($"I'm sorry, it doesn't seem like those coordinates work for the {name}.  Let's try again.");
                PlaceShip(destroyer, battleship, aircraftCarrier);
            }
            SetCoordinates();
        }

        public bool NoOtherShipConflict(Destroyer destroyer, Battleship battleship, AircraftCarrier aircraftCarrier)
        {
            if (destroyer.GetLocationIsSet())
            {
                for (int i = 0; i < destroyer.location.GetLength(0); i++)
                {
                    if ((destroyer.location[i, 0] == location[0,0] && destroyer.location[i, 1] == location[0, 0]) ||
                        (destroyer.location[i, 0] == location[1, 0] && destroyer.location[i, 1] == location[1, 1]) ||
                        (destroyer.location[i, 0] == location[2, 0] && destroyer.location[i, 1] == location[2, 1]))
                    {
                        return false;
                    }
                }
            }
            if (battleship.GetLocationIsSet())
            {
                for (int i = 0; i < battleship.location.GetLength(0); i++)
                {
                    if ((battleship.location[i, 0] == location[0, 0] && battleship.location[i, 1] == location[0, 0]) ||
                        (battleship.location[i, 0] == location[1, 0] && battleship.location[i, 1] == location[1, 1]) ||
                        (battleship.location[i, 0] == location[2, 0] && battleship.location[i, 1] == location[2, 1]))
                    {
                        return false;
                    }
                }
            }
            if (aircraftCarrier.GetLocationIsSet())
            {
                for (int i = 0; i < aircraftCarrier.location.GetLength(0); i++)
                {
                    if ((aircraftCarrier.location[i, 0] == location[0, 0] && aircraftCarrier.location[i, 1] == location[0, 0]) ||
                        (aircraftCarrier.location[i, 0] == location[1, 0] && aircraftCarrier.location[i, 1] == location[1, 1]) ||
                        (aircraftCarrier.location[i, 0] == location[2, 0] && aircraftCarrier.location[i, 1] == location[2, 1]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SetCoordinates()
        {
            locationSet = true;
        }
        public bool GoodCoordinates(string stringx1, string stringy1, string stringx2, string stringy2, Destroyer destroyer, Battleship battleship, AircraftCarrier aircraftCarrier)
        {
            bool verifyCoordinates = false;
            int x1;
            int x2;
            int y1;
            int y2;
            if (CoordinateIsInt(stringx1) && CoordinateIsInt(stringx2) && CoordinateIsInt(stringy1) && CoordinateIsInt(stringy2))
            {
                x1 = Convert.ToInt32(stringx1);
                y1 = Convert.ToInt32(stringy1);
                x2 = Convert.ToInt32(stringx2);
                y2 = Convert.ToInt32(stringy2);
                if (ValidBoardCoordinate(x1, y1) && ValidBoardCoordinate(x2, y2))
                {
                    if (ValidShipOnBoard(x1, y1, x2, y2))
                    {
                        FillShipLocation(x1, y1, x2, y2);
                        if (NoOtherShipConflict(destroyer, battleship, aircraftCarrier))
                        {
                            verifyCoordinates = true;
                        }
                    }
                }
            }
            return verifyCoordinates;
        }
        public void FillShipLocation(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2)
            {
                if(y1 < y2)
                {
                    location[0, 0] = x1;
                    location[0, 1] = y1;
                    location[1, 0] = x1;
                    location[1, 1] = y1 + 1;
                    location[2, 0] = x2;
                    location[2, 1] = y2;
                }
                else
                {
                    location[0, 0] = x1;
                    location[0, 1] = y2;
                    location[1, 0] = x1;
                    location[1, 1] = y2 + 1;
                    location[2, 0] = x2;
                    location[2, 1] = y1;
                }
            }
            else
            {
                if(x1 < x2)
                {
                    location[0, 0] = x1;
                    location[0, 1] = y1;
                    location[1, 0] = x1 + 1;
                    location[1, 1] = y1;
                    location[2, 0] = x2;
                    location[2, 1] = y2;
                }
                else
                {
                    location[0, 0] = x2;
                    location[0, 1] = y1;
                    location[1, 0] = x2 + 1;
                    location[1, 1] = y1;
                    location[2, 0] = x1;
                    location[2, 1] = y2;
                }
         
                
            }
        }
        public override bool ShipOnLocation(int x, int y)
        {
            if ((location[0, 0] == x && location[0, 1] == y) ||
               (location[1, 0] == x && location[1, 1] == y) ||
               (location[2, 0] == x && location[2, 1] == y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int[,] GetLocation()
        {
            return location;
        }

    }
}
