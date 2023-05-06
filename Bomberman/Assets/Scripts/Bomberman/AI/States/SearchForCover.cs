using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Bomberman.AI.States
{
    public class SearchForCover : IState
    {
        private Gridx gridx;
        private int[,] stageGrid;
        private AIBombermanController aiBombermanController;
        private List<(int x, int y)> possibleSafeSpots;
        private int safeZoneRadius = 2;

        public SearchForCover(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {

        }

        public void OnEnter()
        {
            possibleSafeSpots = new List<(int x, int y)>();
            gridx = aiBombermanController.GetGrid();
            stageGrid = gridx.GetGrid();
            gridx.GetXY(aiBombermanController.transform.position, out int x, out int y);
            
            var lowerLimitX = (x - safeZoneRadius) >= 1 ? (x - safeZoneRadius) : 1;
            var upperLimitX = (x + safeZoneRadius) < gridx.GetLength() ? (x + safeZoneRadius) : gridx.GetLength()-1;
            
            var lowerLimitY = (y - safeZoneRadius) >= 1 ? (y - safeZoneRadius) : 1;
            var upperLimitY = (y + safeZoneRadius) < gridx.GetWidth() ? (y + safeZoneRadius) : gridx.GetWidth()-1;
            for (int i = lowerLimitX; i <= upperLimitX; i++)
            {
                for (int j = lowerLimitY; j <= upperLimitY; j++)
                {
                    if(stageGrid[i, j] == 0 || stageGrid[i, j] == 1)
                        possibleSafeSpots.Add((i,j));
                }  
            }

            var bombFlame = (int)aiBombermanController.GetComponent<FinalBombermanStats>().GetNumericStat(Stats.Flame);
            var bombPosition = aiBombermanController.transform.position;
            var flameVectors = aiBombermanController.FlameDetector(bombPosition, bombFlame);

            var safe = possibleSafeSpots
                .Except(flameVectors)
                .OrderBy(a => Vector3.Distance(aiBombermanController.transform.position, new Vector3(a.x, 0, a.y)))
                .ToList();

            foreach (var s in safe)
            {
                if (aiBombermanController.ComputePath(new Vector3(s.x, 0, s.y)))
                {
                    aiBombermanController.potentialDestinationVector = new Vector3(s.x, 0, s.y);
                    break;
                }
                else
                {
                    aiBombermanController.potentialDestinationVector = Vector3.zero;
                }
            }
        }

        public void OnExit()
        {
        }
    }
}