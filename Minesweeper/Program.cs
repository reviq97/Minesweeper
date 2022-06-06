using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var wall = new Game();
            wall.RandomMineTable();
            wall.ArmTable();
            bool inGame = true; 

            while (inGame)
            {
                try
                {
                    Console.Clear();

                    wall.Render();
                    Console.WriteLine($"\nUsed marks: {Game.marks.Count}");

                    Console.Write("\nType coordinates in order [A-J][1-10]:");

                    string input = Console.ReadLine();

                    Console.Write("\nChoose action by typing [M] to mark/unmark [D] to discover field:");

                    string action = Console.ReadLine().ToUpper();

                    if(action == "D")
                    {
                        wall.UserInput(input);
                    }
                    if (action == "M")
                    {
                        wall.MarkTable(input);
                    }
                    inGame = wall.IsWin(input, action);

                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine($"Something is wrong, try again!\n{e.Message}");
                }

            }
            wall.Render();
            Console.WriteLine();
            wall.ShowMineTable();
            Console.ReadKey();

        }
    }
}
