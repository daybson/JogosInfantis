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
    public TileBase pathTile;
    TilemapCollider2D tilemapCollider;

    public char wall;
    public char street;
    public char path;
    public int width;
    public int height;

    public Vector2Int[] levels;

    private void Awake()
    {
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
    }


    public void Generate()
    {
        backTracker = new MazeGenerator(width, height, wall, street, path);
        backTracker.Generate();
        GenerateFromString(backTracker.ToString());
        SortBorderPoint();
        RecenterGridOnScreen();
        //backTracker.RatSolver(backTracker.entry, MazeGenerator.Directions.Down);
    }


    public void Generate(int level)
    {
        if (level <= levels.Length - 1)
        {
            width = levels[level].x;
            height = levels[level].y;
        }

        Generate();
    }


    private void GenerateFromString(string map)
    {
        tilemap.ClearAllTiles();

        var lines = map.Split('\n');

        for (int x = 0; x < lines.Length; x++)
        {
            for (int y = 0; y < lines[x].Length; y++)
            {
                if (lines[x][y] == wall)
                    tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                else if (lines[x][y] == street)
                    tilemap.SetTile(new Vector3Int(x, y, 0), streetTile);
                else if (lines[x][y] == path)
                    tilemap.SetTile(new Vector3Int(x, y, 0), pathTile);
            }
        }

        //Destroy(tilemap.gameObject.GetComponent<TilemapCollider2D>());
        //tilemapCollider = tilemap.gameObject.AddComponent<TilemapCollider2D>();
        //tilemapCollider.usedByComposite = true;
    }


    private void ReadFromFile(string mazeName)
    {
        //https://www.dcode.fr/maze-generator
        //https://thenerdshow.com/amaze.html?rows=7&cols=6&color=FFFFFF&bgcolor=4C4587&sz=10px&blank=++&wall=%3Cem%3EHI%3C%2Fem%3E
        //https://rosettacode.org/wiki/Maze_generation#C.23

        var lines = File.ReadAllLines("Assets/Resources/MazesFiles/" + mazeName + ".txt");
        StringBuilder stringBuilder = new StringBuilder(lines.ToString());

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


    private void SortBorderPoint()
    {
        var heightFirstColumn = UnityEngine.Random.Range(2, height - 2);
        var heightLastColumn = UnityEngine.Random.Range(2, height - 2);


        tilemap.SetTile(new Vector3Int(0, heightFirstColumn, 0), streetTile);
        tilemap.SetTile(new Vector3Int(1, heightFirstColumn, 0), streetTile);

        tilemap.SetTile(new Vector3Int(width - 1, heightLastColumn, 0), streetTile);
        tilemap.SetTile(new Vector3Int(width - 2, heightLastColumn, 0), streetTile);

        backTracker.entry = new Vector2Int(0, heightFirstColumn);
        backTracker.exit = new Vector2Int(width - 1, heightLastColumn);

    }


    private void RecenterGridOnScreen()
    {
        var center = new Vector3Int(width / 2 + 1, height / 2 + 1, 0);
        tilemap.GetTile(center);

        var mazeCellCenter = tilemap.GetCellCenterWorld(center);
        var worldCenter = GameSystem.Instance.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        var centerOffset = worldCenter - mazeCellCenter;
        tilemap.transform.parent.transform.position += centerOffset;
    }
}