using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMaze
{
    //http://www.migapro.com/depth-first-search/
    public class MazeGenerator
    {
        int height;
        int width;
        char wall;
        char street;
        public int[][] Cells { get; private set; }
        string asciiMaze;

        public MazeGenerator(int height, int width, char wall, char street)
        {
            this.height = width;
            this.width = height;
            this.wall = wall;
            this.street = street;
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
                    Cells[i][j] = 1;
            }

            Random rand = new Random();

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
                        if (c + 2 >= height - 1)
                            continue;
                        if (Cells[r][c + 2] != 0)
                        {
                            Cells[r][c + 2] = 0;
                            Cells[r][c + 1] = 0;
                            RecursiveDigging(r, c + 2);
                        }
                        break;

                    case 3: //down
                        if (r + 2 >= width - 1)
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
                        line += Cells[r][c] == 1 ? wall : street;
                    }

                    s.AppendLine(line);
                }

                asciiMaze = s.ToString();
            }

            return asciiMaze;
        }
    }
}