using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Pointer
    {


        public static void Up(int r, int c)
        {

        }

        public static void UpRight(int r, int c)
        {

        }

        public static void Right(int r, int c)
        {

        }
        public static void DownRight(int r, int c)
        {

        }
        public static void Down(int r, int c)
        {

        }
        public static void DownLeft(int r,int c)
        {
            if (Table.armedTable[r, --c] != 9) ++Table.armedTable[r, c];
        }

        public static void Left(int r, int c)
        {

        }
        public static void LeftUp(int r, int c)
        {

        }
    }
}
