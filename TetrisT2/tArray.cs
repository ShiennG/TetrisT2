using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TetrisT2
{
    class tArray
    {
        public int rows;
        public int columns;

        public tArray(int aRows, int aColumns)
        {
            rows = aRows;
            columns = aColumns;
        }


        // creates the grid
        public int[,] Create()
        {
            int[,] array = new int[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    array[row, column] = 0;
                }
            }
            return array;
        }


        // prints out the current grid
        public void PrintGrid(int[,] array, bool convert)
        {
            for (int i = 0; i < 2 * (columns) + 2; i++) { Console.Write("-"); } // border
            Console.WriteLine();
            for (int row = 0; row < rows; row++)
            {
                Console.Write("|"); // border
                for (int column = 0; column < columns; column++)
                {
                    if (convert) // additional option for debugging 
                    {
                        if (array[row, column] == 0) { Console.Write("  "); }
                        else if (array[row, column] == 1) { Console.Write("[]"); }
                        else if (array[row, column] == 2) { Console.Write("[]"); }
                        else { Console.Write("E"); } // (in case something goes wrong - means error)
                    }
                    else { Console.Write(array[row, column] + " "); }
                }
                Console.Write("|"); // border
                Console.WriteLine();
            }
            for (int i = 0; i < 2 * (columns) + 2; i++) { Console.Write("-"); }
            Console.WriteLine();
        }



    }
}
