using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.AI.StatesV2.ControllerV2
{
    public class Helper
    {
        public List<(int x, int y)> SearchGridFor(Gridx.Legend objective, int[,] stageGrid)
        {
            var gridObjectives = new List<(int x, int y)>();
            for (int i = 0; i < stageGrid.GetLength(0); i++)
            {
                for (int j = 0; j < stageGrid.GetLength(1); j++)
                {
                    if (stageGrid[i, j] == (int)objective)
                    {
                        gridObjectives.Add((i, j));
                    }
                }
            }
            return gridObjectives;
        }

        public List<(int x, int y)> SearchInRadiusAroundBomberman(Gridx gridx, int[,] stageGrid, Vector3 currP, int radius)
        {
            var possibleObjectives = new List<(int x, int y)>();
            gridx.GetXY(currP, out int x, out int y);
            
            var lowerLimitX = (x - radius) >= 1 ? (x - radius) : 1;
            var upperLimitX = (x + radius) < gridx.GetLength() ? (x + radius) : gridx.GetLength()-1;
            
            var lowerLimitY = (y - radius) >= 1 ? (y - radius) : 1;
            var upperLimitY = (y + radius) < gridx.GetWidth() ? (y + radius) : gridx.GetWidth()-1;
            for (int i = lowerLimitX; i <= upperLimitX; i++)
            {
                for (int j = lowerLimitY; j <= upperLimitY; j++)
                {
                    if(stageGrid[i, j] == 0 || stageGrid[i, j] == 1)
                        possibleObjectives.Add((i,j));
                }  
            }

            return possibleObjectives;
        }

        public Gridx.Legend GetTypeWithValue(int value)
        {
            switch (value)
            {
                case 2: return Gridx.Legend.DWall;
                case 3: return Gridx.Legend.Bomb;
                case 4: return Gridx.Legend.Flame;
                case 6: return Gridx.Legend.Power;
            }

            return Gridx.Legend.None;
        }
    }
}