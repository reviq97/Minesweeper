using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Game
    {
        static Dictionary<string, int> alphabetToNumbers = new Dictionary<string, int>()
        {
            {"A", 0 },
            {"B", 1 },
            {"C", 2 },
            {"D", 3 },
            {"E", 4 },
            {"F", 5 },
            {"G", 6 },
            {"H", 7 },
            {"I", 8 },
            {"J", 9 },
        };

        char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        public static int[,] armedTable = new int[10,10];
        public static string[,] gameTable = new string[10, 10]
        {
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" },
            {"|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|","|=|" }
        };

        static List<Point> points = new List<Point>();
        static List<Point> mines = new List<Point>();
        static Dictionary<string,Point> discovered = new Dictionary<string, Point>();
        public static List<int> marks = new List<int>();


        //przetestować
        public bool IsWin(string input, string mark)
        {
            int bin = 0;

            int row = alphabetToNumbers[input[0].ToString().ToUpper()];
            int column = int.Parse(input[1].ToString()) - 1;

            if (mark != "M" && armedTable[row, column] == 9) { Console.WriteLine("\n|== Game Over ==|\n"); return false; }

            if (marks.Count == 10)
            {
                foreach (var item in mines)
                {
                    var r = item.row;
                    var c = item.column;

                    if (gameTable[r, c] == "|?|") bin++;
                }
            }
            if (discovered.Count == 92 || bin == 10)
            {
                Console.WriteLine("\n Congrats 8-) ! You've won the game!\n"); return false;
            }

            else return true;
        }

        public void UserInput(string input)
        {
            int row = alphabetToNumbers[input[0].ToString().ToUpper()];
            int column = int.Parse(input.Substring(1))-1;

            if((row >= 0 && row <10) && (column >= 0 && column < 10))
            {
                int number = armedTable[row, column];
                if (!ScanZeroCells(new Point(row, column)))
                {
                    if (number == 0) {  gameTable[row, column] = $"| |"; }
                    else 
                    {
                        if(gameTable[row,column] == "|?|")
                        {
                            marks.RemoveAt(marks.Count - 1);
                        }
                        gameTable[row, column] = $"|{number}|";
                    }
                }
            }
        }

        
        public void MarkTable(string coordinates)
        {
            int row = alphabetToNumbers[coordinates[0].ToString().ToUpper()];
            int column = int.Parse(coordinates.Substring(1)) - 1;

            if (marks.Count <= 10)
            {
                if(gameTable[row,column] == "|=|" && armedTable[row, column] >= 0)
                {
                    if(marks.Count != 10)
                    {
                        marks.Add(1);
                        gameTable[row, column] = "|?|";
                    }
                }
                else
                {
                    if (gameTable[row, column] == "|?|" && armedTable[row, column] >= 0)
                    {
                        marks.RemoveAt(marks.Count - 1);
                        gameTable[row, column] = "|=|";
                    }
                }
            }
            
        }

        public bool ScanZeroCells(Point point)
        {

            if (point.row == -1 || point.column == -1 || point.row == 10 || point.column == 10)
            {
                return false;
            }

            if (points.Exists(r=>r.row==point.row && r.column == point.column))
            {
                return false;
            }

            discovered.Add(point.row.ToString() + point.column.ToString(), point);

            points.Add(point);


            if (armedTable[point.row,point.column] > 0)
            {
                return false;
            }

            if(armedTable[point.row, point.column] == 0)
            {
                gameTable[point.row, point.column] = "| |";
            }

            var up = new Point(point.row - 1, point.column);
            var down = new Point(point.row + 1, point.column);
            var left = new Point(point.row, point.column - 1);
            var right = new Point(point.row, point.column + 1);

            return ScanZeroCells(up) ||
                   ScanZeroCells(down) ||
                   ScanZeroCells(left) ||
                   ScanZeroCells(right);
        }

        public void Render()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(alphabet[i]);
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(gameTable[i,j]);
                }
                Console.WriteLine();
                if(i == 9) 
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Console.Write($"  {k+1}");
                    }
                }
            }
        }

        public void ShowMineTable()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(alphabet[i]);
                for (int j = 0; j < 10; j++)
                {

                    Console.Write($"|{armedTable[i, j]}|");
                }
                Console.WriteLine();
                if (i == 9)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Console.Write($"  {k + 1}");
                    }
                }
            }
        }
        public void RandomMineTable()
        {
            var random = new Random();

            
            for (int i = 0; i < 10; i++)
            {
                var row = random.Next(1, 9);
                var column = random.Next(1, 9);

                if (armedTable[row, column] != 9)
                {

                    mines.Add(new Point(row,column));
                    armedTable[row, column] = 9;
                }
                else i--;
            }
            Console.WriteLine();
        }

        public void ArmTable()
        {
            int row = 0;
            int column = 0;

            //loop line iterating through left to right row
            while (true)
            {
                //if row and column is equal to 9 loop is breaking
                if (row == 9 && column == 9) break;
                else
                {
                    //if is equal to 9, next line
                    if(column == 9)
                    {
                        column = 0;
                        row++;
                    }

                    //if mine is detected
                    if ((armedTable[row, column] == 9))
                    {
                        //tmp variables
                        int r = row;
                        int c = column;

                        //corners
                        if (armedTable[0, 0] == 9) 
                        {  
                            if (armedTable[1,0] != 9) ++armedTable[1,0];
                            if (armedTable[1,1] != 9) ++armedTable[1,1];
                            if (armedTable[0,1] != 9) ++armedTable[0,1];
                            
                        }
                        if (armedTable[0, 9] == 9) 
                        {
                            if(armedTable[0,8] != 9) ++armedTable[0,8];
                            if(armedTable[1,8] != 9) ++armedTable[1,8]; 
                            if(armedTable[1,9] != 9) ++armedTable[1,9];
                        }
                        if (armedTable[9, 0] == 9) 
                        {  
                            if(armedTable[8,0] != 9) ++armedTable[8,0]; 
                            if(armedTable[8,1] != 9) ++armedTable[8,1]; 
                            if(armedTable[9,1] != 9) ++armedTable[9,1];
                        }
                        if (armedTable[9, 9] == 9) 
                        {  
                            if(armedTable[9,8] != 9 ) ++armedTable[9,8]; 
                            if(armedTable[8,8] != 9 ) ++armedTable[8,8]; 
                            if(armedTable[8,9] != 9 ) ++armedTable[8,9];
                        }
                        
                        //left border
                        if(column == 0 && (row > 0 && row < 9))
                        {
                            //up
                            if (armedTable[--r, c] != 9) ++armedTable[r, c];

                            //upright
                            if(armedTable[r,++c] != 9) ++armedTable[r, c];

                            //right
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];
                            
                            //rightdown
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];

                            //down
                            if (armedTable[r, --c] != 9) ++armedTable[r, c];

                            
                        }

                        //right border
                        if (column == 9 && (row > 0 && row < 9))
                        {
                            //up
                            if (armedTable[--r, c] != 9) ++armedTable[r, c];

                            //upleft
                            if (armedTable[r, --c] != 9) ++armedTable[r, c];

                            //left
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];

                            //leftdown
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];

                            //down
                            if (armedTable[r, ++c] != 9) ++armedTable[r, c];

                        }

                        //up border
                        if (row == 0 && (column > 0 && column < 9))
                        {
                            //left
                            if (armedTable[r, --c] != 9) ++armedTable[r, c];

                            //leftdown
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];

                            //down
                            if (armedTable[r, ++c] != 9) ++armedTable[r, c];

                            //downright
                            if (armedTable[r, ++c] != 9) ++armedTable[r, c];

                            //right
                            if (armedTable[--r, c] != 9) ++armedTable[r, c];

                        }

                        //down border

                        if (row == 9 && (column > 0 && column < 9))
                        {
                            if (armedTable[r, --c] != 9) ++armedTable[r, c];

                            //upleft
                            if (armedTable[--r, c] != 9) ++armedTable[r, c];

                            //up
                            if (armedTable[r, ++c] != 9) ++armedTable[r, c];

                            //upright
                            if (armedTable[r, ++c] != 9) ++armedTable[r, c];

                            //right
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];

                        }

                        //inner square
                        if (((row != 0 && column != 0)) && (row != 9 && column != 9))
                        {
                            //for example if column=3 and row=1 -> left goes to column = 2 and row = 1
                            //left
                            if (armedTable[r, --c] != 9) ++armedTable[r, c];

                            //upleft
                            if (armedTable[--r, c] != 9) ++armedTable[r, c];

                            //up
                            if (armedTable[r, ++c] != 9) ++armedTable[r, c];

                            //upright
                            if (armedTable[r, ++c] != 9) ++armedTable[r, c];

                            //right
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];

                            //downright
                            if (armedTable[++r, c] != 9) ++armedTable[r, c];

                            //down
                            if (armedTable[r, --c] != 9) ++armedTable[r, c];

                            //downleft
                            if (armedTable[r, --c] != 9) ++armedTable[r, c];
                        }

                        

                    }
                    column++;


                }

                //if row meets wall, move down. if 
                //line iterating through row


            }
        }

    }
}
