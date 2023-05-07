using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.AI.States
{
    public class InspectArea : IState
    {
        private AIBombermanController aiBombermanController;
        private int[,] stageGrid;
        private Gridx gridx;
        private List<(int x, int y)> importantGridObjectives;

        public InspectArea(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("Inspecting area");
            importantGridObjectives = new List<(int x, int y)>();
            stageGrid = aiBombermanController.GetGrid().GetGrid();
            gridx = aiBombermanController.GetGrid();
            SearchGridForImportantObjectives();
            
            var target = aiBombermanController.secondaryTargetPosition;
            gridx.GetXY(target, out int x, out int y);
            var targetType = aiBombermanController.secondaryTargetType;
            
            if (stageGrid[x, y] == (int)targetType)
            {
                aiBombermanController.targetChanged = false;
                aiBombermanController.ComputePath(aiBombermanController.secondaryTargetPosition);
            }
        }
        
        void SearchGridForImportantObjectives()
        {
            for (int i = 0; i < stageGrid.GetLength(0); i++)
            {
                for (int j = 0; j < stageGrid.GetLength(1); j++)
                {
                    if(stageGrid[i, j] == 3 ||
                       stageGrid[i, j] == 4 ||
                       stageGrid[i, j] == 6 ||
                       stageGrid[i, j] == 2) 
                        importantGridObjectives.Add((i, j));
                }
            }
        }
        
        void BombermanSpiderSense(int radius)
        {
            // if the distance from the bomberman to the event updated cell < 5
            // check that location if it is a bomb
            // calculate the vectors that the flame cover
            // apply action
        }
        public void OnExit()
        {
            
        }
    }
}