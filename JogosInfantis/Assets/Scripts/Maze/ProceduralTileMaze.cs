using MyMaze;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralTileMaze : MonoBehaviour
{
    MazeGenerator backTracker;
    public Tilemap tilemap;
    public TileBase streetTile;
    public TileBase wallTile;
    TilemapCollider2D tilemapCollider;

    public char wall;
    public char street;
    public int width;
    public int height;


    private void Awake()
    {
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();

        Generate();
    }


    public void Generate()
    {
        backTracker = new MazeGenerator(width, height, wall, street);
        backTracker.Generate();
        GenerateFromString(backTracker.ToString());
    }


    void ReadFromFile(string mazeName)
    {
        //https://www.dcode.fr/maze-generator
        //https://thenerdshow.com/amaze.html?rows=7&cols=6&color=FFFFFF&bgcolor=4C4587&sz=10px&blank=++&wall=%3Cem%3EHI%3C%2Fem%3E
        //https://rosettacode.org/wiki/Maze_generation#C.23
        var lines = File.ReadAllLines("Assets/Resources/MazesFiles/" + mazeName + ".txt");

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] != street)
                    tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                else
                    tilemap.SetTile(new Vector3Int(x, y, 0), streetTile);
            }
        }
    }


    public void GenerateFromString(StringBuilder map)
    {
        tilemap.ClearAllTiles();

        var lines = map.ToString().Split('\n');

        for (int x = 0; x < lines.Length; x++)
        {
            for (int y = 0; y < lines[x].Length; y++)
            {
                if (lines[x][y] == wall)
                    tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                else
                    tilemap.SetTile(new Vector3Int(x, y, 0), streetTile);
            }
        }

        //Destroy(tilemap.gameObject.GetComponent<TilemapCollider2D>());
        //tilemapCollider = tilemap.gameObject.AddComponent<TilemapCollider2D>();
        //tilemapCollider.usedByComposite = true;
    }
}