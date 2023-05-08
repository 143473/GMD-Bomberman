using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

//A* Search Pathfinding Example from : https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/ 

class PathfindingAStar
{
    private Gridx gridx;
    private int[,] stageLayout;
    private bool isWall = false;

    public PathfindingAStar(Gridx gridx)
    {
        this.gridx = gridx;
    }

    public List<Tile> GetPath((int X, int Y) current, (int X, int Y) target)
    {
        stageLayout = gridx.GetGrid();
        isWall = stageLayout[target.X, target.Y] == 2;
        
        var start = new Tile();
        start.X = current.X;
        start.Y = current.Y;
        
        var finish = new Tile();
        finish.X = target.X;
        finish.Y = target.Y;

        start.SetDistance(finish.X, finish.Y);

        var activeTiles = new List<Tile>();
        activeTiles.Add(start);
        var visitedTiles = new List<Tile>();

        while (activeTiles.Any())
        {
            var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

            if (checkTile.X == finish.X && checkTile.Y == finish.Y)
            {
                if (isWall)
                    stageLayout[target.X, target.Y] = 2;
                
                var finalPath = new List<Tile>();
                var backwardsPath = new List<Tile>();
                //We found the destination and we can be sure (Because the the OrderBy above)
                //That it's the most low cost option. 

                // Adding tiles backwards in a list
                var tile = checkTile;
                while (true)
                {
                    backwardsPath.Add(tile);
                    tile = tile.Parent;
                    if (tile == null)
                        break;
                }

                // Generating the right path to the destination
                for (int i = backwardsPath.Count-1; i >= 0; i--)
                {
                    if (isWall && i == 0)
                        break;
                    finalPath.Add(backwardsPath[i]);
                }

                return finalPath;
            }

            visitedTiles.Add(checkTile);
            activeTiles.Remove(checkTile);

            var walkableTiles = GetWalkableTiles(stageLayout, checkTile, finish);

            foreach (var walkableTile in walkableTiles)
            {
                //We have already visited this tile so we don't need to do so again!
                if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    continue;

                //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                {
                    var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                    if (existingTile.CostDistance > checkTile.CostDistance)
                    {
                        activeTiles.Remove(existingTile);
                        activeTiles.Add(walkableTile);
                    }
                }
                else
                {
                    //We've never seen this tile before so add it to the list. 
                    activeTiles.Add(walkableTile);
                }
            }
        }

        Debug.Log("No Path Found!");
        return null;
    }

    private static List<Tile> GetWalkableTiles(int[,] stageLayout, Tile currentTile, Tile targetTile)
    {
        var possibleTiles = new List<Tile>()
        {
            new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
            new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
            new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
        };
        if (stageLayout[targetTile.X, targetTile.Y] == 2)
            stageLayout[targetTile.X, targetTile.Y] = 0;

        possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

        var maxX = stageLayout.GetLength(0);

        var maxY = stageLayout.GetLength(1);

        return possibleTiles
            .Where(tile => tile.X > 0 && tile.X < maxX)
            .Where(tile => tile.Y > 0 && tile.Y < maxY)
            .Where(tile => stageLayout[tile.X, tile.Y] == 0 
                           || stageLayout[tile.X, tile.Y] == 1 
                           || (tile == targetTile && stageLayout[tile.X, tile.Y] == 2) 
                           || stageLayout[tile.X, tile.Y] == 6 
                           || stageLayout[tile.X, tile.Y] == 7)
            .ToList();
    }
}

public class Tile
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Cost { get; set; }
    public int Distance { get; set; }
    public int CostDistance => Cost + Distance;
    public Tile Parent { get; set; }

    //The distance is essentially the estimated distance, ignoring walls to our target. 
    //So how many tiles left and right, up and down, ignoring walls, to get there. 
    public void SetDistance(int targetX, int targetY)
    {
        this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
}