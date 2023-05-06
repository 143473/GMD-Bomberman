using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bomberman.AI.States
{
    public class SearchForCover : IState
    {
        private Gridx gridx;
        private AIBombermanController aiBombermanController;
        private List<(int x, int y)> possibleSafeSpots;
        private int safeZoneRadius = 5;

        public SearchForCover(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {

        }

        public void OnEnter()
        {
            gridx = aiBombermanController.GetGrid();
            gridx.GetXY(aiBombermanController.transform.position, out int x, out int y);
            
            var lowerLimitX = (x - safeZoneRadius) > 1 ? (x - safeZoneRadius) : 0;
            var upperLimitX = (x + safeZoneRadius) < gridx.GetLength() ? (x + safeZoneRadius) : gridx.GetLength();
            
            var lowerLimitY = (y - safeZoneRadius) >= 1 ? (y - safeZoneRadius) : 0;
            var upperLimitY = (y + safeZoneRadius) < gridx.GetWidth() ? (y + safeZoneRadius) : gridx.GetWidth();
            for (int i = lowerLimitX; i < upperLimitX; i++)
            {
                for (int j = lowerLimitY; j < upperLimitY; j++)
                {
                    if(gridx.GetGrid()[i, j] == 0)
                        possibleSafeSpots.Add((i,j));
                }  
            }

            var safe = possibleSafeSpots.FirstOrDefault();
            aiBombermanController.potentialDestinationVector = new Vector3(safe.x, 0, safe.y);
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}