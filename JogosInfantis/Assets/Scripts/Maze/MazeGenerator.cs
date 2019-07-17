using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MyMaze
{
    //http://www.migapro.com/depth-first-search/
    public class MazeGenerator
    {
        public enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }

        int columns;
        int lines;

        char wall;
        char street;
        char path;
        public int[][] Cells { get; private set; }
        string asciiMaze;

        public Vector2Int entry;
        public Vector2Int exit;

        public readonly int WALL = 1;
        public readonly int STREET = 0;
        public readonly int PATH = 2;

        public MazeGenerator(int height, int width, char wall, char street, char path)
        {
            this.columns = height;
            this.lines = width;
            this.wall = wall;
            this.street = street;
            this.path = path;
        }


        public void Generate()
        {
            asciiMaze = string.Empty;

            Cells = new int[lines][];

            //inicializa 
            for (int line = 0; line < lines; line++)
            {
                Cells[line] = new int[columns];

                for (int col = 0; col < columns; col++)
                    Cells[line][col] = WALL;
            }

            System.Random rand = new System.Random();

            //celula inicial aleatoria
            int l = rand.Next(lines);
            while (l % 2 == 0)
            {
                l = rand.Next(lines);
            }

            int c = rand.Next(columns);
            while (c % 2 == 0)
            {
                c = rand.Next(columns);
            }

            Cells[l][c] = STREET;

            RecursiveDigging(l, c);
        }


        private void RecursiveDigging(int line, int col)
        {
            var randDirs = GetRandomDirections();

            for (int i = 0; i < randDirs.Length; i++)
            {
                switch (randDirs[i])
                {
                    case 1: //up
                        if (line - 2 <= 0)
                            continue;

                        if (Cells[line - 2][col] != STREET)
                        {
                            Cells[line - 2][col] = STREET;
                            Cells[line - 1][col] = STREET;
                            RecursiveDigging(line - 2, col);
                        }
                        break;

                    case 2: //right
                        if (col + 2 >= columns - 1)
                            continue;

                        if (Cells[line][col + 2] != STREET)
                        {
                            Cells[line][col + 2] = STREET;
                            Cells[line][col + 1] = STREET;
                            RecursiveDigging(line, col + 2);
                        }
                        break;

                    case 3: //down
                        if (line + 2 >= lines - 1)
                            continue;

                        if (Cells[line + 2][col] != STREET)
                        {
                            Cells[line + 2][col] = STREET;
                            Cells[line + 1][col] = STREET;
                            RecursiveDigging(line + 2, col);
                        }
                        break;

                    case 4: //left
                        if (col - 2 <= 0)
                            continue;

                        if (Cells[line][col - 2] != STREET)
                        {
                            Cells[line][col - 2] = STREET;
                            Cells[line][col - 1] = STREET;
                            RecursiveDigging(line, col - 2);
                        }
                        break;
                }
            }
        }


        private int[] GetRandomDirections()
        {
            var randoms = new List<int>() { 1, 2, 3, 4 };
            return randoms.OrderBy(x => UnityEngine.Random.Range(0, 4)).ToArray();
        }


        public bool RatSolver(Vector2Int position, Directions direction)
        {
            if (position == exit)
            {
                Cells[exit.x][exit.y] = PATH;
                return true;
            }

            if (IsSafeToGo(position))
            {
                var previous = Cells[position.x][position.y];
                Cells[position.x][position.y] = PATH;

                if (direction != Directions.Up && RatSolver(new Vector2Int(position.x + 1, position.y), Directions.Down))
                {
                    //go down
                    return true;
                }
                //else go down
                if (direction != Directions.Left && RatSolver(new Vector2Int(position.x, position.y + 1), Directions.Right))
                {
                    //go right
                    return true;
                }
                if (direction != Directions.Down && RatSolver(new Vector2Int(position.x - 1, position.y), Directions.Up))
                {
                    //go up
                    return true;
                }
                if (direction != Directions.Right && RatSolver(new Vector2Int(position.x, position.y - 1), Directions.Left))
                {
                    //go left
                    return true;
                }

                //if none of the options work out BACKTRACK undo the move
                Cells[position.x][position.y] = previous;
                return false;
            }

            return false;
        }


        private bool IsSafeToGo(Vector2Int position)
        {
            if (position.x >= 0 &&
                    position.y >= 0 &&
                    position.x < columns &&
                    position.y < lines &&
                    Cells[position.x][position.y] != WALL)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public new string ToString()
        {
            if (string.IsNullOrEmpty(asciiMaze))
            {
                var s = new StringBuilder();

                for (int l = 0; l < lines; l++)
                {
                    var line = string.Empty;

                    for (int c = 0; c < columns; c++)
                    {
                        switch (Cells[l][c])
                        {
                            case 0:
                                line += street;
                                break;
                            case 1:
                                line += wall;
                                break;
                            case 2:
                                line += path;
                                break;
                        }
                    }

                    s.AppendLine(line);
                }

                asciiMaze = s.ToString();
            }

            return asciiMaze;
        }
    }
}