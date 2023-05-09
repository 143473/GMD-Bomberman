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
        private int safeZoneRadius = 4;

        public SearchForCover(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {

        }

        public void OnEnter()
        {
            Debug.Log("Take Cover");
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
            var bombsOnTheMap = SearchGridFor((int)Gridx.Legend.Bomb);
            // var bombPosition = aiBombermanController.transform.position;
            List<(int x, int y)> flameVectors = new List<(int x, int y)>();
            List<(int x, int y)> bombsFlameVectors = new List<(int x, int y)>();
            foreach (var bomb in bombsOnTheMap)
            {
                bombsFlameVectors = aiBombermanController.FlameDetector(new Vector3(bomb.x, 0, bomb.y), bombFlame);
                for (int i = 0; i < bombsFlameVectors.Count; i++)
                {
                    flameVectors.Add(bombsFlameVectors[i]);
                }
            }
            

            var safe = possibleSafeSpots
                .Except(flameVectors)
                .OrderBy(a => Vector3.Distance(aiBombermanController.transform.position, new Vector3(a.x, 0, a.y)))
                .ToList();

            foreach (var s in safe)
            {
                if (aiBombermanController.ComputePath(new Vector3(s.x, 0, s.y)))
                {
                    aiBombermanController.potentialSafeSpot = new Vector3(s.x, 0, s.y);
                    break;
                }
                else
                {
                    aiBombermanController.potentialSafeSpot = Vector3.zero;
                }
            }

            flameVectors.Clear();
        }
        
        List<(int x, int y)> SearchGridFor(int value)
        {
            List<(int x, int y)> chosenObjectives = new List<(int x, int y)>();
            
            for (int i = 0; i < stageGrid.GetLength(0); i++)
            {
                for (int j = 0; j < stageGrid.GetLength(1); j++)
                {
                    if (stageGrid[i, j] == value)
                        chosenObjectives.Add((i, j));
                }
            }

            return chosenObjectives;
        }

        public void OnExit()
        {
        }
    }
}