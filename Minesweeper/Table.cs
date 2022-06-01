using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Table
    {
        char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        char[] symbols = { '*' ,'?'};
        public static int[,] armedTable = new int[10,10];

        

        public void Render()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(alphabet[i]);
                for (int j = 0; j < 10; j++)
                {
                    if (armedTable[i, j] == 9) Console.Write("|{0}|", armedTable[i, j]);
                    else Console.Write("|{0}|", armedTable[i,j]) ;
                }
                Console.WriteLine();
                if(i == 9) 
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Console.Write($"  {alphabet[k]}");
                    }
                }
            }
        }
        public void MineTable()
        {
            var random = new Random();

            
            //9 means bomb
            for (int i = 0; i < 10; i++)
            {
                var row = random.Next(0, 9);
                var column = random.Next(0, 9);

                if (armedTable[row, column] == 0)
                {
                    armedTable[row, column] = 9;
                }
                else i--;
            }

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

                        if(column == 0)
                        {
                            if(row == 0)
                            {
                                
                            }
                            if(row == 9)
                            {

                            }
                            else
                            {

                            }
                        }
                        if(column == 9)
                        {
                            if(row == 0)
                            {

                            }
                            if(row == 9)
                            {

                            }
                            else
                            {

                            }
                        }

                        if(((row != 0 && column != 0)) && (row != 9 && column != 9))
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
