using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TetrisT2
{
    class Shapes
    {
        public static int shapeCount = 0;
        public static int score = 0;
        public Shapes() { shapeCount++; } // used to tell the user how many shapes have been creted

        public int[,] Create(int number, int[,] grid, int arColumns)
        {
            int c = arColumns / 2 - 1;
            switch (number)
            {
                case 0: // triangle up
                    grid[0, c] = 1;
                    for (int i = c - 1; i < c + 2; i++) { grid[1, i] = 1; }
                    return grid;
                case 1: // triangle right
                    grid[1, c + 1] = 1;
                    for (int i = 0; i < 3; i++) { grid[i, c] = 1; }
                    return grid;
                case 2: // triangle down
                    grid[1, c + 1] = 1;
                    for (int i = c; i < c + 3; i++) { grid[0, i] = 1; }
                    return grid;
                case 3: // triangle left
                    grid[1, c - 1] = 1;
                    for (int i = 0; i < 3; i++) { grid[i, c] = 1; }
                    return grid;

                case 4: // vertical line
                    for (int i = 0; i < 4; i++) { grid[i, c] = 1; }
                    return grid;
                case 5: // horizontal line
                    for (int i = c - 2; i < c + 2; i++) { grid[0, i] = 1; }
                    return grid;
                case 6: // cube
                    for (int i = c; i < c + 2; i++) { grid[0, i] = 1; grid[1, i] = 1; }
                    return grid;

                case 7: // Lramp up
                    grid[0, c] = 1;
                    for (int i = c; i <= c + 2; i++) { grid[1, i] = 1; }
                    return grid;
                case 8: // Lramp right
                    grid[0, c + 1] = 1;
                    for (int i = 0; i < 3; i++) { grid[i, c] = 1; }
                    return grid;
                case 9: // Lramp down
                    grid[1, c + 1] = 1;
                    for (int i = c - 1; i < c + 2; i++) { grid[0, i] = 1; }
                    return grid;
                case 10: // Lramp left
                    grid[2, c - 1] = 1;
                    for (int i = 0; i < 3; i++) { grid[i, c] = 1; };
                    return grid;

                case 11: // Rramp up
                    grid[0, c + 1] = 1;
                    for (int i = c - 1; i < c + 2; i++) { grid[1, i] = 1; }
                    return grid;
                case 12: // Rramp right
                    grid[2, c + 1] = 1;
                    for (int i = 0; i < 3; i++) { grid[i, c] = 1; }
                    return grid;
                case 13: // Rramp down
                    grid[1, c - 1] = 1;
                    for (int i = c - 1; i < c + 2; i++) { grid[0, i] = 1; }
                    return grid;
                case 14: // Rramp left
                    grid[0, c - 1] = 1;
                    for (int i = 0; i < 3; i++) { grid[i, c] = 1; }
                    return grid;

                case 15: // S horizontal
                    for (int i = c; i < c + 2; i++) { grid[1, i + 1] = 1; grid[0, i + 2] = 1; }
                    return grid;
                case 16: // S vertical
                    for (int i = 0; i < 2; i++) { grid[i, c] = 1; grid[i + 1, c + 1] = 1; }
                    return grid;
                case 17: // Z horizontal
                    for (int i = c; i < c + 2; i++) { grid[0, i] = 1; grid[1, i + 1] = 1; }
                    return grid;
                case 18: // Z vertical
                    for (int i = 0; i < 2; i++) { grid[1, i] = 1; grid[0, i + 1] = 1; }
                    return grid;
                default:
                    return grid;



            }
        }

    }
}
