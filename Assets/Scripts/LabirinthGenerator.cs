using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorWalls
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool visited = false;

    public bool upZone = false;
    public bool downZone = false;
    public bool rightZone = false;
    public bool leftZone = false;

    public int distanceToExit;
}
public class LabirinthGenerator
{
    public int Width = 20;
    public int Height = 10;
    public GeneratorWalls[,] GenerateLabirinth()
    {
        GeneratorWalls[,] labirinth = new GeneratorWalls[Width, Height];

        for (int x = 0; x < labirinth.GetLength(0); x++)
        {
            for (int y = 0; y < labirinth.GetLength(1); y++)
            {
                labirinth[x, y] = new GeneratorWalls { X = x, Y = y };
            }
        }

        for (int x = 0; x < labirinth.GetLength(0); x++)
        {
            labirinth[x, Height - 1].WallLeft = false;

        }
        for (int y = 0; y < labirinth.GetLength(1); y++)
        {
            labirinth[Width - 1, y].WallBottom = false;
        }

        RemoveWallsGenerator(labirinth);

        PlaceLabirinthExit(labirinth);

        return labirinth;
    }

    private void RemoveWallsGenerator(GeneratorWalls[,] labirinth)
    {
        GeneratorWalls currentWalls = labirinth[0, 0];
        currentWalls.visited = true;
        currentWalls.distanceToExit = 0;

        Stack<GeneratorWalls> stackWalls = new Stack<GeneratorWalls>();
        do
        {
            List<GeneratorWalls> unvisitedWalls = new List<GeneratorWalls>();

            int x = currentWalls.X;
            int y = currentWalls.Y;

            if (x > 0 && !labirinth[x - 1, y].visited) unvisitedWalls.Add(labirinth[x - 1, y]);
            if (y > 0 && !labirinth[x, y - 1].visited) unvisitedWalls.Add(labirinth[x, y - 1]);
            if (x < Width - 2 && !labirinth[x + 1, y].visited) unvisitedWalls.Add(labirinth[x + 1, y]);
            if (y < Height - 2 && !labirinth[x, y + 1].visited) unvisitedWalls.Add(labirinth[x, y + 1]);

            if (unvisitedWalls.Count > 0)
            {
                GeneratorWalls chosen = unvisitedWalls[Random.Range(0, unvisitedWalls.Count)];
                RemoveWall(currentWalls, chosen);
                chosen.visited = true;
                stackWalls.Push(chosen);
                currentWalls = chosen;
                chosen.distanceToExit = stackWalls.Count;
            }
            else
            {
                currentWalls = stackWalls.Pop();
            }

        } while (stackWalls.Count > 0);
    }

    private void PlaceLabirinthExit(GeneratorWalls[,] labirinth)
    {
        GeneratorWalls exitWall = labirinth[0, 0];
        for (int x = 0; x < labirinth.GetLength(0); x++)
        {
            if (labirinth[x, Height - 2].distanceToExit > exitWall.distanceToExit) exitWall = labirinth[x, Height - 2];
            if (labirinth[x, 0].distanceToExit > exitWall.distanceToExit) exitWall = labirinth[x, 0];
        }

        for (int y = 0; y < labirinth.GetLength(1); y++)
        {
            if (labirinth[Width - 2, y].distanceToExit > exitWall.distanceToExit) exitWall = labirinth[Width - 2, y];
            if (labirinth[0, y].distanceToExit > exitWall.distanceToExit) exitWall = labirinth[0, y];
        }

        if (exitWall.X == 0)
        {
            exitWall.WallLeft = false;
            exitWall.leftZone = true;
        }
        else if (exitWall.Y == 0)
        {
            exitWall.WallBottom = false;
            exitWall.downZone = true;
        }
        else if (exitWall.X == Width - 2)
        {
            labirinth[exitWall.X + 1, exitWall.Y].WallLeft = false;
            labirinth[exitWall.X + 1, exitWall.Y].rightZone = true;
        }
        else if (exitWall.Y == Height - 2)
        {
            labirinth[exitWall.X, exitWall.Y + 1].WallBottom = false;
            labirinth[exitWall.X, exitWall.Y + 1].upZone = true;
        }
    }


    private void RemoveWall(GeneratorWalls a, GeneratorWalls b)
    {
        if(a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }
}
