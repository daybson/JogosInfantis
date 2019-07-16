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

        int height;
        int width;

        char wall;
        char street;
        char path;
        public int[][] Cells { get; private set; }
        string asciiMaze;

        public Vector2Int entry;
        public Vector2Int exit;

        int WALL = 1;
        int STREET = 0;
        int PATH = 2;

        public MazeGenerator(int height, int width, char wall, char street, char path)
        {
            this.height = width;
            this.width = height;
            this.wall = wall;
            this.street = street;
            this.path = path;
        }


        public void Generate()
        {
            asciiMaze = string.Empty;
            Cells = new int[width][];

            //inicializa 
            for (int i = 0; i < width; i++)
            {
                Cells[i] = new int[height];
                for (int j = 0; j < height; j++)
                    Cells[i][j] = WALL;
            }

            System.Random rand = new System.Random();

            //celula inicial aleatoria
            int r = rand.Next(width);
            while (r % 2 == 0)
            {
                r = rand.Next(width);
            }

            int c = rand.Next(height);
            while (c % 2 == 0)
            {
                c = rand.Next(height);
            }

            Cells[r][c] = STREET;

            RecursiveDigging(r, c);
        }


        private void RecursiveDigging(int r, int c)
        {
            var randDirs = GetRandomDirections();

            for (int i = 0; i < randDirs.Length; i++)
            {
                switch (randDirs[i])
                {
                    case 1: //up
                        if (r - 2 <= 0)
                            continue;

                        if (Cells[r - 2][c] != STREET)
                        {
                            Cells[r - 2][c] = STREET;
                            Cells[r - 1][c] = STREET;
                            RecursiveDigging(r - 2, c);
                        }
                        break;

                    case 2: //right
                        if (c + 2 >= height - 1)
                            continue;
                        if (Cells[r][c + 2] != STREET)
                        {
                            Cells[r][c + 2] = STREET;
                            Cells[r][c + 1] = STREET;
                            RecursiveDigging(r, c + 2);
                        }
                        break;

                    case 3: //down
                        if (r + 2 >= width - 1)
                            continue;
                        if (Cells[r + 2][c] != STREET)
                        {
                            Cells[r + 2][c] = STREET;
                            Cells[r + 1][c] = STREET;
                            RecursiveDigging(r + 2, c);
                        }
                        break;

                    case 4: //left
                        if (c - 2 <= 0)
                            continue;
                        if (Cells[r][c - 2] != STREET)
                        {
                            Cells[r][c - 2] = 0;
                            Cells[r][c - 1] = 0;
                            RecursiveDigging(r, c - 2);
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
                position.x < width &&
                position.y < height &&
                Cells[position.x][position.y] != WALL)
                return true;

            return false;
        }

        public new string ToString()
        {
            if (string.IsNullOrEmpty(asciiMaze))
            {
                var s = new StringBuilder();

                for (int r = 0; r < width; r++)
                {
                    var line = string.Empty;

                    for (int c = 0; c < height; c++)
                    {
                        switch (Cells[r][c])
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