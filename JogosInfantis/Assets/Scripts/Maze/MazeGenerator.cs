using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMaze
{
    //http://www.migapro.com/depth-first-search/
    public class MazeGenerator
    {
        int width;
        int height;

        public int[][] maze;

        /// <summary>
        /// Funciona melhor com valores ímpares
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public MazeGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
        }


        public void Generate()
        {
            maze = new int[height][];

            //inicializa 
            for (int i = 0; i < height; i++)
            {
                maze[i] = new int[width];
                for (int j = 0; j < width; j++)
                    maze[i][j] = 1;
            }

            Random rand = new Random();

            //celula inicial aleatoria
            int r = rand.Next(height);
            while (r % 2 == 0)
            {
                r = rand.Next(height);
            }

            int c = rand.Next(width);
            while (c % 2 == 0)
            {
                c = rand.Next(width);
            }

            maze[r][c] = 0;

            recursion(r, c);
        }


        public void recursion(int r, int c)
        {
            var randDirs = generateRandomDirections();

            for (int i = 0; i < randDirs.Length; i++)
            {
                switch (randDirs[i])
                {
                    case 1: //up
                        if (r - 2 <= 0)
                            continue;

                        if (maze[r - 2][c] != 0)
                        {
                            maze[r - 2][c] = 0;
                            maze[r - 1][c] = 0;
                            recursion(r - 2, c);
                        }
                        break;

                    case 2: //right
                        if (c + 2 >= width - 1)
                            continue;
                        if (maze[r][c + 2] != 0)
                        {
                            maze[r][c + 2] = 0;
                            maze[r][c + 1] = 0;
                            recursion(r, c + 2);
                        }
                        break;

                    case 3: //down
                        if (r + 2 >= height - 1)
                            continue;
                        if (maze[r + 2][c] != 0)
                        {
                            maze[r + 2][c] = 0;
                            maze[r + 1][c] = 0;
                            recursion(r + 2, c);
                        }
                        break;

                    case 4: //left
                        if (c - 2 <= 0)
                            continue;
                        if (maze[r][c - 2] != 0)
                        {
                            maze[r][c - 2] = 0;
                            maze[r][c - 1] = 0;
                            recursion(r, c - 2);
                        }
                        break;
                }
            }
        }


        private int[] generateRandomDirections()
        {
            var randoms = new List<int>() { 1, 2, 3, 4 };

            randoms = randoms.OrderBy(x => UnityEngine.Random.Range(0, 4)).ToList();

            return randoms.ToArray();
        }


        public StringBuilder ToString()
        {
            var s = new StringBuilder();

            for (int r = 0; r < height; r++)
            {
                var line = "";

                for (int c = 0; c < width; c++)
                {
                    line += maze[r][c] == 1 ? "#" : ".";
                }

                s.AppendLine(line);
            }

            return s;
        }
    }
}