using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMaze
{
    public class Maze
    {
        public struct Cell
        {
            public int linha;
            public int coluna;

            public Cell(int linha, int coluna)
            {
                this.linha = linha;
                this.coluna = coluna;
            }

            public static bool operator !=(Cell a, Cell b)
            {
                return a.linha != b.linha || a.coluna != b.coluna;
            }

            public static bool operator ==(Cell a, Cell b)
            {
                return a.linha == b.linha && a.coluna == b.coluna;
            }

            public override bool Equals(object obj)
            {
                return this == (Cell)obj;
            }
        }

        public enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }

        public int colunas { get; private set; }
        public int linhas { get; private set; }

        public int[][] Cells;

        public Cell entry;
        public Cell exit;

        int WALL = 1;
        int STREET = 0;
        int PATH = 2;



        /// <summary>
        /// Funciona melhor com valores ímpares
        /// </summary>
        /// <param name="linhas"></param>
        /// <param name="colunas"></param>
        public Maze(int linhas, int colunas)
        {
            this.colunas = colunas;
            this.linhas = linhas;
        }


        public void Generate()
        {
            Cells = new int[linhas][];

            for (int l = 0; l < linhas; l++)
            {
                Cells[l] = new int[colunas];
                for (int col = 0; col < colunas; col++)
                    Cells[l][col] = WALL;
            }

            Random rand = new Random();

            //celula inicial aleatoria
            int coluna = rand.Next(colunas);
            while (coluna % 2 == 0)
            {
                coluna = rand.Next(colunas);
            }

            int linha = rand.Next(linhas);
            while (linha % 2 == 0)
            {
                linha = rand.Next(linhas);
            }

            Cells[linha][coluna] = STREET;

            RecursiveDigging(linha, coluna);
        }


        public void RecursiveDigging(int l, int c)
        {
            var randDirs = GetRandomDirections();

            for (int i = 0; i < randDirs.Length; i++)
            {
                switch (randDirs[i])
                {
                    case 1: //up
                        if (l - 2 <= 0)
                            continue;

                        if (Cells[l - 2][c] != STREET)
                        {
                            Cells[l - 2][c] = STREET;
                            Cells[l - 1][c] = STREET;
                            RecursiveDigging(l - 2, c);
                        }
                        break;

                    case 2: //right
                        if (c + 2 >= colunas - 1)
                            continue;
                        if (Cells[l][c + 2] != STREET)
                        {
                            Cells[l][c + 2] = STREET;
                            Cells[l][c + 1] = STREET;
                            RecursiveDigging(l, c + 2);
                        }
                        break;

                    case 3: //down
                        if (l + 2 >= linhas - 1)
                            continue;
                        if (Cells[l + 2][c] != STREET)
                        {
                            Cells[l + 2][c] = STREET;
                            Cells[l + 1][c] = STREET;
                            RecursiveDigging(l + 2, c);
                        }
                        break;

                    case 4: //left
                        if (c - 2 <= 0)
                            continue;
                        if (Cells[l][c - 2] != STREET)
                        {
                            Cells[l][c - 2] = STREET;
                            Cells[l][c - 1] = STREET;
                            RecursiveDigging(l, c - 2);
                        }
                        break;
                }
            }
        }


        public bool RatSolver(Cell position, Directions direction)
        {
            if (position == exit)
            {
                Cells[exit.linha][exit.coluna] = PATH;
                return true;
            }

            if (IsSafeToGo(position))
            {
                var previous = Cells[position.linha][position.coluna];
                Cells[position.linha][position.coluna] = PATH;

                if (direction != Directions.Up && RatSolver(new Cell(position.linha + 1, position.coluna), Directions.Down))
                {
                    //go down
                    return true;
                }
                //else go down
                if (direction != Directions.Left && RatSolver(new Cell(position.linha, position.coluna + 1), Directions.Right))
                {
                    //go right
                    return true;
                }
                if (direction != Directions.Down && RatSolver(new Cell(position.linha - 1, position.coluna), Directions.Up))
                {
                    //go up
                    return true;
                }
                if (direction != Directions.Right && RatSolver(new Cell(position.linha, position.coluna - 1), Directions.Left))
                {
                    //go left
                    return true;
                }

                //if none of the options work out BACKTRACK undo the move
                Cells[position.linha][position.coluna] = previous;
                return false;
            }

            return false;
        }


        private bool IsSafeToGo(Cell position)
        {
            if (position.linha >= 0 &&
                position.coluna >= 0 &&
                position.linha < linhas &&
                position.coluna < colunas &&
                Cells[position.linha][position.coluna] != WALL)
                return true;

            return false;
        }

        private int[] GetRandomDirections()
        {
            var randoms = new List<int>() { 1, 2, 3, 4 };
            return randoms.OrderBy(x => new Random().Next(4)).ToArray();
        }


        public void SortBorderPoint()
        {
            var rand = new Random();
            var lineEntry = rand.Next(2, linhas - 2);
            var lineExit = rand.Next(2, linhas - 2);

            entry = new Cell(lineEntry, 0);
            exit = new Cell(lineExit, colunas - 1);

            Cells[entry.linha][entry.coluna] = 0;
            Cells[entry.linha][entry.coluna + 1] = 0;

            Cells[exit.linha][exit.coluna] = 0;
            Cells[exit.linha][exit.coluna - 1] = 0;
        }



        public new string ToString()
        {
            var s = new StringBuilder();

            for (int r = 0; r < linhas; r++)
            {
                var line = "";

                for (int c = 0; c < colunas; c++)
                {
                    switch (Cells[r][c])
                    {
                        case 0:
                            line += " ";
                            break;
                        case 1:
                            line += "#";
                            break;
                        case 2:
                            line += "°";
                            break;
                    }
                }

                s.AppendLine(line);
            }

            return s.ToString();
        }

    }
}