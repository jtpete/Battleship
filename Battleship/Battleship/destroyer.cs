using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Destroyer : Ship
    {
        public int[,] location = new int[2, 2];
        public Destroyer()
        {
            name = "Destroyer";
            size = 2;
        }

        public override bool ShipOnLocation(int x, int y)
        {
            if ((location[0, 0] == x && location[0, 1] == y) ||
                (location[1,0] == x && location[1,1] == y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PlaceShip(Submarine submarine, Battleship battleship, AircraftCarrier aircraftCarrier)
        {
            locationSet = false;
            location = ClearLocation(location);
            Console.WriteLine($"Where would you like to place your {name}?");
            Console.WriteLine($"This ship requires {size} spots.  Pleae give the first spots x coordinate:");
            string x1 = Console.ReadLine();
            Console.WriteLine($"Now, pleae give the first spots y coordinate:");
            string y1 = Console.ReadLine();
            Console.WriteLine($"This ship requires {size} spots.  Pleae give the second spots x coordinate:");
            string x2 = Console.ReadLine();
            Console.WriteLine($"Now, pleae give the second spots y coordinate:");
            string y2 = Console.ReadLine();

            if (!GoodCoordinates(x1, y1, x2, y2, submarine, battleship, aircraftCarrier))
            {
                Console.WriteLine($"I'm sorry, it doesn't seem like those coordinates work for the {name}.  Let's try again.");
                location = ClearLocation(location);
                PlaceShip( submarine,  battleship, aircraftCarrier);
            }
            if(!locationSet)
            {
                SetCoordinates(x1, y1, x2, y2);
            }
        }
        public bool GoodCoordinates(string stringx1, string stringy1, string stringx2, string stringy2, Submarine submarine, Battleship battleship, AircraftCarrier aircraftCarrier)
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
                        if (NoOtherShipConflict(x1, y1, x2, y2, submarine, battleship, aircraftCarrier))
                        {
                            verifyCoordinates = true;
                        }
                    }
                }
            }
            return verifyCoordinates;
        }
        public bool NoOtherShipConflict(int x1, int y1, int x2, int y2, Submarine submarine, Battleship battleship, AircraftCarrier aircraftCarrier)
        {
            if (submarine.GetLocationIsSet())
            {
                for (int i = 0; i < submarine.location.GetLength(0); i++) 
                {
                    if ((submarine.location[i,0] == x1 && submarine.location[i,1] == y1) ||
                        (submarine.location[i,0] == x2 && submarine.location[i,1] == y2))
                    {
                        return false;
                    }
                }
            }
            if (battleship.GetLocationIsSet())
            {
                for (int i = 0; i < battleship.location.GetLength(0); i++)
                {
                    if ((battleship.location[i, 0] == x1 && battleship.location[i, 1] == y1) ||
                        (battleship.location[i, 0] == x2 && battleship.location[i, 1] == y2))
                    {
                        return false;
                    }
                }
            }
            if (aircraftCarrier.GetLocationIsSet())
            {
                for (int i = 0; i < aircraftCarrier.location.GetLength(0); i++)
                {
                    if ((aircraftCarrier.location[i, 0] == x1 && aircraftCarrier.location[i, 1] == y1) ||
                        (aircraftCarrier.location[i, 0] == x2 && aircraftCarrier.location[i, 1] == y2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SetCoordinates(string locationx1, string locationy1, string locationx2, string locationy2)
        {

            int x1 = Convert.ToInt16(locationx1);
            int y1 = Convert.ToInt16(locationy1);
            int x2 = Convert.ToInt16(locationx2);
            int y2 = Convert.ToInt16(locationy2);
            if (x1 < x2 || y1 < y2)
            {
                location[0, 0] = x1;
                location[0, 1] = y1;
                location[1, 0] = x2;
                location[1, 1] = y2;
                locationSet = true;
            }
            else
            {
                location[0, 0] = x2;
                location[0, 1] = y2;
                location[1, 0] = x1;
                location[1, 1] = y1;
                locationSet = true;

            }

        }


        public bool NoConflictWithOtherShips(Submarine Submarine, Battleship battleship, AircraftCarrier aircraftCarrier)
        {

            return true;
        }
        public int[,] GetLocation()
        {
            return location;
        }
        public void SetLocation(int x1, int y1, int x2, int y2)
        {

                location[0, 0] = x1;
                location[0, 1] = y1;
                location[1, 0] = x2;
                location[1, 1] = y2;


            locationSet = true;
        }

}
}
