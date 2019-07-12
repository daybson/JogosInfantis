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
        char wall;
        char street;
        public int[][] Cells { get; private set; }


        public MazeGenerator(int width, int height, char wall, char street)
        {
            this.width = width;
            this.height = height;
            this.wall = wall;
            this.street = street;
        }


        public void Generate()
        {
            Cells = new int[height][];

            //inicializa 
            for (int i = 0; i < height; i++)
            {
                Cells[i] = new int[width];
                for (int j = 0; j < width; j++)
                    Cells[i][j] = 1;
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

            Cells[r][c] = 0;

            RecursiveDigging(r, c);
        }


        public void RecursiveDigging(int r, int c)
        {
            var randDirs = GetRandomDirections();

            for (int i = 0; i < randDirs.Length; i++)
            {
                switch (randDirs[i])
                {
                    case 1: //up
                        if (r - 2 <= 0)
                            continue;

                        if (Cells[r - 2][c] != 0)
                        {
                            Cells[r - 2][c] = 0;
                            Cells[r - 1][c] = 0;
                            RecursiveDigging(r - 2, c);
                        }
                        break;

                    case 2: //right
                        if (c + 2 >= width - 1)
                            continue;
                        if (Cells[r][c + 2] != 0)
                        {
                            Cells[r][c + 2] = 0;
                            Cells[r][c + 1] = 0;
                            RecursiveDigging(r, c + 2);
                        }
                        break;

                    case 3: //down
                        if (r + 2 >= height - 1)
                            continue;
                        if (Cells[r + 2][c] != 0)
                        {
                            Cells[r + 2][c] = 0;
                            Cells[r + 1][c] = 0;
                            RecursiveDigging(r + 2, c);
                        }
                        break;

                    case 4: //left
                        if (c - 2 <= 0)
                            continue;
                        if (Cells[r][c - 2] != 0)
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


        public new StringBuilder ToString()
        {
            var s = new StringBuilder();

            for (int r = 0; r < height; r++)
            {
                var line = string.Empty;

                for (int c = 0; c < width; c++)
                {
                    line += Cells[r][c] == 1 ? wall : street;
                }

                s.AppendLine(line);
            }

            return s;
        }
    }
}