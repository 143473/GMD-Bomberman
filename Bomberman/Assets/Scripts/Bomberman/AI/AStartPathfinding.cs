// using System;
// using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
//
// namespace Bomberman.AI
// {
//     public class AStartPathfinding
//     {
//         public List<GridNode> GetPath(GridNode gridNode)
//         {
//             List<GridNode> path = new List<GridNode>();
//             while (gridNode.parent != null)
//             {
//                 path.Add(gridNode);
//                 gridNode = gridNode.parent;
//             }
//
//             return path;
//         }
//         
//         //What should be instead of gamestate?
//         public List<GridNode> AStartPathfinder(Object gamestate, (int, int) start, (int, int) destination)
//         {
//             List<GridNode> path = new List<GridNode>();
//
//             List<GridNode> openList = new List<GridNode>();
//             openList.Add(new GridNode(null, start, null));
//
//             List<GridNode> closedList = new List<GridNode>();
//
//             int maxLoops = 1000;
//             int counter = 0;
//
//             while (openList.Count > 0 && counter <= maxLoops)
//             {
//                 // find the node with the lowest rank
//                 GridNode currentNode = openList[0];
//                 int currIndex = 0;
//
//                 for (int i = 0; i < openList.Count; i++)
//                 {
//                     if (openList[i].f < currentNode.f)
//                     {
//                         currentNode = openList[i];
//                         currIndex = i;
//                     }
//                 }
//
//                 // check if this node is the goal
//                 if (currentNode.location.Equals(destination))
//                 {
//                     Console.WriteLine("~~~~~~~FOUND TARGET~~~~~~~");
//                     path = GetPath(currentNode);
//                     return path;
//                 }
//
//                 // current = remove lowest rank item from OPEN
//                 // add current to CLOSED
//                 openList.RemoveAt(currIndex);
//                 closedList.Add(currentNode);
//
//                 List<Dictionary<(int, int), string>> neighbors = GetFreeNeighbors(gameState, currentNode.location);
//                 List<GridNode> neighborNodes = new List<GridNode>();
//
//                 foreach (Dictionary<(int, int), string> neighbor in neighbors)
//                 {
//                     foreach (KeyValuePair<(int, int), string> kvp in neighbor)
//                     {
//                         neighborNodes.Add(new GridNode(null, kvp.Key, kvp.Value));
//                     }
//                 }
//
//                 foreach (GridNode neighbor in neighborNodes)
//                 {
//                     // used for loop behavior
//                     bool inClosed = false;
//                     bool inOpen = false;
//
//                     // cost = g(current) + movementcost(current, neighbor)
//                     int cost = currentNode.g + 1;
//
//                     // if neighbor in OPEN and cost less than g(neighbor):
//                     //   remove neighbor from OPEN, because new path is better
//                     for (int i = 0; i < openList.Count; i++)
//                     {
//                         GridNode node = openList[i];
//                         if (neighbor.location == node.location && cost < neighbor.g)
//                         {
//                             openList.RemoveAt(i);
//                             inOpen = true;
//                         }
//                     }
//
//                     for (int i = 0; i < closedList.Count; i++)
//                     {
//                         GridNode node = closedList[i];
//                         if (neighbor.location == node.location && cost < neighbor.g)
//                         {
//                             closedList.RemoveAt(i);
//                             inClosed = true;
//                         }
//                     }
//
//                     if (!inOpen && !inClosed)
//                     {
//                         neighbor.g = cost;
//                         openList.Add(neighbor);
//                         neighbor.h = ManhattanDistance(neighbor.location, target);
//                         neighbor.f = neighbor.g + neighbor.h;
//                         neighbor.parent = currentNode;
//                     }
//
//                     counter += 1;
//                 }
//             }
//         }
//
//         public int ManhattanDistance((int x, int y) start, (int x, int y) destination)
//         {
//             return Math.Abs(start.x - destination.x) + Math.Abs(start.y - destination.y);
//         }
//
//         public class GridNode
//         {
//             public GridNode parent;
//             public (int, int) location;
//             public string action;
//
//             public int h;
//             public int g;
//             public int f;
//
//             public GridNode(GridNode parent, (int, int) location, string action)
//             {
//                 this.parent = parent;
//                 this.location = location;
//                 this.action = action;
//
//                 h = 0;
//                 g = 0;
//                 f = 0;
//             }
//         }
//     }
// }