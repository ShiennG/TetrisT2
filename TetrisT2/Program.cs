using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TetrisT2;
using System.Timers;
using System.Reflection.Metadata.Ecma335;


namespace Tetris2
{
    class TetrisT2
    {
        // starting off with my functions, 'main' is at the end of this code



        // function to clear the console in order to display next frames
        static void ClearConsole(int lineAmount)
        {
            for (int i = 0; i < lineAmount; i++) { Console.WriteLine(); }
            //Console.WriteLine("Cleared!");
        }

        // gets the direction the user wants the shape to move in
        static string GetDirection()
        {

            /* string direction = Console.ReadLine();
             if      (direction == "A" || direction == "a") { return "Left"; }
             else if (direction == "D" || direction == "d") { return "Right"; }
             else if (direction == "S" || direction == "s") { return "Down"; }
             return "None";
             */



            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow) { return "Left"; }
            else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow) { return "Right"; }
            else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow) { return "Down"; }


            return "None";

        }


        // moves the shape left
        public static void GoLeft(int[,] grid, int arRows, int arColumns)
        {
            bool adjacentLeft = false;

            for (int row = 0; row < arRows; row++) // checks wether there is a wall blocking left
            {
                for (int column = 0; column < arColumns; column++)
                {
                    if (grid[row, column] == 1 && (column == 0 || grid[row, column - 1] == 2))
                    {
                        adjacentLeft = true;
                    }
                }
            }

            if (!adjacentLeft) // if nothing is blocking left, moves all 1s left
            {
                for (int row = 0; row < arRows; row++)
                {
                    for (int column = 0; column < arColumns; column++)
                    {
                        if (grid[row, column] == 1)
                        {
                            grid[row, column - 1] = 1;
                            grid[row, column] = 0;
                        }
                    }
                }
            }
            else { Console.WriteLine("Left is Blocked"); }
        }

        public static void GoRight(int[,] grid, int arRows, int arColumns)
        {
            bool adjacentRight = false;

            for (int row = 0; row < arRows; row++) // checks wether there is a wall blocking right
            {
                for (int column = 0; column < arColumns; column++)
                {
                    if (grid[row, column] == 1 && (column == arColumns - 1 || grid[row, column + 1] == 2))
                    {
                        adjacentRight = true;
                    }
                }
            }

            if (!adjacentRight) // if nothing is blocking right, moves all 1s right
            {
                for (int row = 0; row < arRows; row++)
                {
                    for (int column = arColumns - 1; column >= 0; column--)
                    {
                        if (grid[row, column] == 1)
                        {
                            grid[row, column + 1] = 1;
                            grid[row, column] = 0;
                        }
                    }
                }
            }
            else { Console.WriteLine("Right is Blocked"); }
        }


        public static void GoDown(bool once, int[,] grid, int arRows, int arColumns)
        {
            bool adjacentDown = false;
            int i = 0;
            do // 'do while' will always execute this atleast once unless the condition is true
            {
                for (int row = arRows - 1; row >= 0; row--) // checks wether there is nothing below the shape
                {
                    for (int column = 0; column < arColumns; column++)
                    {
                        if (grid[row, column] == 1 && (row == arRows - 1 || grid[row + 1, column] == 2))
                        {
                            adjacentDown = true;
                        }
                    }
                }

                if (!adjacentDown) // if the path is clear, moves the shape down
                {
                    for (int row = arRows - 1; row >= 0; row--)
                    {
                        for (int column = 0; column < arColumns; column++)
                        {
                            if (grid[row, column] == 1)
                            {
                                grid[row + 1, column] = 1;
                                grid[row, column] = 0;
                            }
                        }
                    }
                }

                if (adjacentDown) // if the shape reaches the bottom or [2], becomes unmovable
                {
                    for (int row = arRows - 1; row >= 0; row--)
                    {
                        for (int column = 0; column < arColumns; column++)
                        {
                            if (grid[row, column] == 1)
                            {
                                grid[row, column] = 2;
                            }
                        }
                    }
                    once = true;
                }
                i++;
                if (i == arRows - 1) { break; }
            } while (!once); // i used 'do while' in order to choose between executing this once or looping it
        }

        // moves the shape 
        static void MoveShape(int count, string direction, int[,] grid, int arRows, int arColumns)
        {

            if (direction == "Left") { GoLeft(grid, arRows, arColumns); }
            if (direction == "Right") { GoRight(grid, arRows, arColumns); }

            // after each 2nd move, moves the shape down once
            if (count == 1) { GoDown(true, grid, arRows, arColumns); } // true = moves down once

            if (direction == "Down") { GoDown(false, grid, arRows, arColumns); } // false = keeps moving
        }

        static void SpawnShape(int[,] grid, int arRows, int arColumns)
        {
            bool shapeOnGrid = false;
            // checks wether there is atleast 1 [1] (shape) on board, if not -> spawn new
            for (int row = 0; row < arRows; row++)
            {
                for (int column = 0; column < arColumns; column++)
                {
                    if (grid[row, column] == 1) { shapeOnGrid = true; }
                }
            }
            if (!shapeOnGrid)
            {
                Random rand = new Random();
                int randomNum = rand.Next(0, 19);
                Shapes shape = new Shapes();
                shape.Create(randomNum, grid, arColumns);

            }
        }

        // checks wether there is a line full of 2s
        static void CheckLine(int[,] grid, int arRows, int arColumns)
        {

            for (int row = 0; row < arRows; row++)
            {
                bool all2s = true;

                for (int column = 0; column < arColumns; column++)
                {
                    if (grid[row, column] != 2)
                    {
                        all2s = false;
                    }

                }
                if (all2s)
                {
                    Console.WriteLine("ALL 2S DETECTED!");
                    ClearLine(grid, row, arColumns);
                }
            }
        }

        // clears the line full of 2s and moves everything above down once
        public static void ClearLine(int[,] grid, int currentRow, int arColumns)
        {
            for (int column = 0; column < arColumns; column++)
            {
                grid[currentRow, column] = 0;
                Shapes.score++;
            }


            for (int row = currentRow; row >= 0; row--)
            {
                for (int column = 0; column < arColumns; column++)
                {
                    if (row == 0) { grid[row, column] = 0; }
                    else { grid[row, column] = grid[row - 1, column]; }
                }
            }
        }

        // checks wether any of the placed shapes [2] reached the top
        static bool CheckStatus(int[,] grid, int arColumns)
        {
            for (int i = 0; i < arColumns; i++)
            {
                if (grid[0, i] == 2) { return false; }
            }
            return true;
        }


        // timer
        public static System.Timers.Timer myTimer = new System.Timers.Timer(1000);
        public static int secondsCount = 0;


        private static void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            secondsCount++;
        }






        static void Main(string[] args)
        {
            // quick info about the game
            ClearConsole(30);
            Console.WriteLine("This is my first project in C#");
            Console.WriteLine("Due to lack of time I wasn't able to add shape rotation to the game");
            Console.WriteLine("\nYou will be asked for grid size once you start");
            Console.WriteLine("Type anything to continue");
            Console.ReadLine();
            ClearConsole(30);


            // receiving user inputs to set up the size of the grid
            Console.Write("Amount of rows (Minimum 7, Recommended 15): ");
            int rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Amount of columns (Minimum 7, Recommended 10): ");
            int columns = Convert.ToInt32(Console.ReadLine());

            // creating the tArray class which is used to create the grid
            tArray tArray = new tArray(rows, columns);
            int[,] grid = tArray.Create();

            string direction; // needed to move the shape
            int count = 0; // needed to simulate the shape falling down a tile each 2nd move

            // setting up a timer
            myTimer.Elapsed += MyTimer_Elapsed;
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
            myTimer.Start();

            bool GameAlive = true;
            while (GameAlive) // main loop
            {
                ClearConsole(30);

                for (int i = 0; i < 2 * (columns) + 2; i++) { Console.Write("-"); } // border
                Console.WriteLine("\n| Controls: ");
                Console.WriteLine("| [A] - left");
                Console.WriteLine("| [D] - right");
                Console.WriteLine("| [S] - down");


                for (int i = 0; i < 2 * (columns) + 2; i++) { Console.Write("-"); } // border
                Console.WriteLine("\n| Shape count: " + Shapes.shapeCount);
                Console.WriteLine("| Timer: " + secondsCount);
                Console.WriteLine("| Score: " + Shapes.score);

                tArray.PrintGrid(grid, true); // true = converts, false = shows digits
                direction = GetDirection();
                MoveShape(count, direction, grid, rows, columns);
                count++;
                if (count == 2) { count = 0; }
                CheckLine(grid, rows, columns);
                SpawnShape(grid, rows, columns);
                GameAlive = CheckStatus(grid, columns);

            }

            // end screen
            ClearConsole(5);
            for (int i = 0; i < 2 * (columns) + 2; i++) { Console.Write("-"); } // border
            Console.WriteLine("\n GAME OVER!");
            for (int i = 0; i < 2 * (columns) + 2; i++) { Console.Write("-"); } // border
            Console.WriteLine("\n Shapes placed down: " + Shapes.shapeCount);
            Console.WriteLine(" Score: " + Shapes.score);
            for (int i = 0; i < 2 * (columns) + 2; i++) { Console.Write("-"); } // border
            Console.WriteLine("\n Type anything to close the window");
            Console.ReadLine();


        }

    }
}

