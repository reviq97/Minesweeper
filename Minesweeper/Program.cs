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
            var wall = new Table();
            wall.MineTable();
            wall.ArmTable();
            wall.Render();
            
        }
    }
}
