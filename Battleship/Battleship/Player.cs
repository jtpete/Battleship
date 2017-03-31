using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Player
    {
        private string name;
        public GameBoard myGameBoard = new GameBoard();
        public bool turn = false;

        public Player(string person)
        {
            name = person;
        }
        public void GetNameFromPlayer()
        {
            Console.WriteLine("How would you like me to reference you throughout this game?  I.e. what is your name?");
            string response = Console.ReadLine();
            switch (response)
            {
                case " ":
                    Console.WriteLine("Please give me something to work with...");
                    GetNameFromPlayer();
                    break;
                case "":
                    Console.WriteLine("Please give me something to work with...");
                    GetNameFromPlayer();
                    break;
                default:
                    name = response;
                    break;
            }
        }
        public string GetName()
        {
            return name;
        }
        public bool IsTurn()
        {
            return turn;
        }
        public void ChangeTurn()
        {
            if (turn)
            {
                turn = false;
            }
            else
            {
                turn = true;
            }
        }
        public void SetTurn(bool turn)
        {
            this.turn = turn;
        }
    }
}
