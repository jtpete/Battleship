using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public abstract class Ship
    {
        public string name;
        public int hitCount;
        public int size;
        public bool sunk = false;
        public bool locationSet = false;

        public abstract bool ShipOnLocation(int x, int y);

 

        public bool CoordinateIsInt(string coordinate)
        {
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

        public bool ValidBoardCoordinate(int x, int y)
        {
            if(x <= 0 || x > 20)
            {
                return false;
            }
            else if (y <= 0 || y > 20)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidShipOnBoard(int x1, int y1, int x2, int y2)
        {
            bool valid = true;
            if(x1 == x2)
            {
                if(y1 > y2)
                {
                    if(y1-y2+1 != size)
                    {
                        valid = false;
                    }
                }
                else
                {
                    if (y2 - y1 + 1 != size)
                    {
                        valid = false;
                    }
                }
            }
            if (y1 == y2)
            {
                if (x1 > x2)
                {
                    if (x1 - x2 + 1 != size)
                    {
                        valid = false;
                    }
                }
                else
                {
                    if (x2 - x1 + 1 != size)
                    {
                        valid = false;
                    }
                }
            }
            return valid;
        }

        public bool GetLocationIsSet()
        {
            return locationSet;
        }

    }
}
