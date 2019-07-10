using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralTileMaze : MonoBehaviour
{
    RecursizeBackTracker backTracker;
    public Tilemap tilemap;
    public TileBase tile1;
    public TileBase wall;

    public char wallCharacter;
    public char emptyCharacter;
    public string mazeName;

    private void Start()
    {
        ReadFromFile(mazeName);
    }

    void GenBacktrack()
    {

        this.backTracker = new RecursizeBackTracker(10, 10);
        this.backTracker.Generate();
        this.backTracker.Print(this.backTracker.Cells);

        {
            var rows = backTracker.Cells.GetLength(RecursizeBackTracker._rowDimension);
            var columns = backTracker.Cells.GetLength(RecursizeBackTracker._columnDimension);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    var directions = (Directions)backTracker.Cells[y, x];

                    var empty = directions.HasFlag(Directions.S) ? true : false;

                    if (empty)
                        tilemap.SetTile(new Vector3Int(y + 2, x + 2, 0), wall);

                    empty = directions.HasFlag(Directions.E) ? true : false;

                    if (empty)
                        tilemap.SetTile(new Vector3Int(y + 2, x + 2, 0), wall);
                }
            }
        }
    }


    void ReadFromFile(string mazeName)
    {
        //https://www.dcode.fr/maze-generator
        //https://thenerdshow.com/amaze.html?rows=7&cols=6&color=FFFFFF&bgcolor=4C4587&sz=10px&blank=++&wall=%3Cem%3EHI%3C%2Fem%3E
        //https://rosettacode.org/wiki/Maze_generation#C.23
        var lines = File.ReadAllLines("Assets/Resources/MazesFiles/" + mazeName + ".txt");

        for (int x = 0; x < lines.Length; x++)
        {
            for (int y = 0; y < lines[x].Length; y++)
            {
                if (lines[x][y] != emptyCharacter)
                    tilemap.SetTile(new Vector3Int(x, y, 0), wall);
            }
        }

    }
}