// using System.Collections.Generic;
//
// namespace Bomberman.AI
// {
//     public class Pathfinding
//     {
//         private List<PathNode> grid;
//         private List<PathNode> openList;
//         private List<PathNode> closedList;
//
//         public Pathfinding(int length, int width)
//         {
//         }
//
//         private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
//         {
//             // Size of the grid
//             int gridLength = 0;
//             int gridWidth = 0;
//
//             PathNode startNode = new PathNode(null, startX, startY);
//
//             openList = new List<PathNode>();
//             openList.Add(startNode);
//
//             closedList = new List<PathNode>();
//
//             for (int x = 0; x < gridLength; x++)
//             {
//                 for (int y = 0; y < gridWidth; y++)
//                 {
//                     PathNode pathNode = new PathNode(null, x, y);
//                     pathNode.gCost = int.MaxValue;
//                 }
//             }
//         }
//     }
// }