using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI.States
{
    public class SearchForTarget: IState
    {
        private AIBombermanController aiBombermanController;
        private int[,] stageGrid;
        private List<(int x, int y)> chosenGridObjectives;
        private List<(int x, int y, float distanceToMainTarget, int targetType)> secondaryTargets;

        public SearchForTarget(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
            ChooseOneOfTheNearestTargets();
        }

        void ChooseOneOfTheNearestTargets()
        {
            if (secondaryTargets.Count > 0)
            {
                var target = secondaryTargets.OrderBy(a => a.distanceToMainTarget).FirstOrDefault();
                if (aiBombermanController.ComputePath(new Vector3(target.x, 0, target.y)))
                {
                    aiBombermanController.secondaryTargetPosition.x = target.x;
                    aiBombermanController.secondaryTargetPosition.z = target.y;
                    aiBombermanController.secondaryTargetType = (Gridx.Legend)target.targetType;
                }
                secondaryTargets.Remove(target);
            }
            else
            {
                aiBombermanController.secondaryTargetType = Gridx.Legend.None;
                aiBombermanController.secondaryTargetPosition = Vector3.zero;
                aiBombermanController.ComputePath(new Vector3(aiBombermanController.mainTarget.transform.position.x, 0,
                    aiBombermanController.mainTarget.transform.position.z));

            }
        }
        // var powerUp = FindNearestPowerUp();
             // if (powerUp != null && aiBombermanController.ComputePath(powerUp.transform.position))
             //     return powerUp;
             //
             // var wall = FindNearestDestructibleWall();
             // if (wall != null && aiBombermanController.ComputePath(wall.transform.position))
             //     return wall;
             //
             // var player = FindNearestPlayer();
             // if (player != null && aiBombermanController.ComputePath(player.transform.position))
             //     return player;
             //
             //
             // return null;
         
         
         private GameObject FindNearestDestructibleWall()
         {
             return GameObject.FindGameObjectsWithTag("DestructibleWall")
                     .OrderBy(v => Vector3.Distance(aiBombermanController.transform.position, v.transform.position))
                     .FirstOrDefault();
         }
         
        private GameObject FindNearestPowerUp()
        {
            return GameObject
                .FindGameObjectsWithTag("PowerUp")
                .OrderBy(v => Vector3.Distance(aiBombermanController.transform.position, v.transform.position))
                .FirstOrDefault();
        }
        
        private GameObject FindNearestPlayer()
        {
            return GameObject
                .FindGameObjectsWithTag("Player")
                .OrderBy(v => Vector3.Distance(aiBombermanController.transform.position, v.transform.position))
                .FirstOrDefault(a => a.name != aiBombermanController.gameObject.name);
        }

        void SearchForASecondaryTargetOnTheWayToMainTarget(Vector3 mainTargetPosition, int radius)
        {
            var gridx = aiBombermanController.GetGrid();
            var position = aiBombermanController.transform.position;
            gridx.GetXY(position, out int x, out int y);

            float distanceToMain;
            Vector3 positionInGrid = new Vector3(0,0,0);
            
            var lowerLimitX = (x - radius) >= 1 ? (x - radius) : 1;
            var upperLimitX = (x + radius) < gridx.GetLength() ? (x + radius) : gridx.GetLength()-1;
            
            var lowerLimitY = (y - radius) >= 1 ? (y - radius) : 1;
            var upperLimitY = (y + radius) < gridx.GetWidth() ? (y + radius) : gridx.GetWidth()-1;
            for (int i = lowerLimitX; i <= upperLimitX; i++)
            {
                for (int j = lowerLimitY; j <= upperLimitY; j++)
                {
                    positionInGrid.x = i;
                    positionInGrid.y = 0;
                    positionInGrid.z = j;
                    if (stageGrid[i, j] == 2 ||
                        stageGrid[i, j] == 6)
                    {
                        distanceToMain = UnityEngine.Vector3.Distance(position, positionInGrid);
                        secondaryTargets.Add((i, j, distanceToMain, stageGrid[i,j]));
                    }
                }  
            }
        }

        void MoveTowardsMainTargetIfNoSecondaryTargets()
        {
            
        }

        List<(int x, int y)> SearchGridFor(int value)
        {
            for (int i = 0; i < stageGrid.GetLength(0); i++)
            {
                for (int j = 0; j < stageGrid.GetLength(1); j++)
                {
                    if(stageGrid[i, j] == value)
                        chosenGridObjectives.Add((i, j));
                }
            }

            return chosenGridObjectives;
        }

        public void OnEnter()
        {
            chosenGridObjectives = new List<(int x, int y)>();
            secondaryTargets = new List<(int x, int y, float distanceToMainTarget, int targetType)>();
            
            stageGrid = aiBombermanController.GetGrid().GetGrid();
            aiBombermanController.mainTarget = FindNearestPlayer();
            
            SearchForASecondaryTargetOnTheWayToMainTarget(aiBombermanController.mainTarget.transform.position, 3);
        }

        public void OnExit()
        {
            Array.Clear(chosenGridObjectives.ToArray(), 0, chosenGridObjectives.Count);
            Array.Clear(secondaryTargets.ToArray(), 0, secondaryTargets.Count);
        }
    }
}