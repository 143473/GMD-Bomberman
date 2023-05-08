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
    public class SearchForTarget : IState
    {
        private AIBombermanController aiBombermanController;
        private int[,] stageGrid;
        private Gridx gridx;
        private List<(int x, int y)> chosenGridObjectives;
        private List<(int x, int y)> powerUps;
        private List<(int x, int y)> walls;
        private List<(int x, int y)> freeSpots;
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
            Debug.Log("Searching");
            powerUps = SearchGridFor((int)Gridx.Legend.Power);
            gridx = aiBombermanController.GetGrid();
            var position = aiBombermanController.transform.position;
            var currVect = aiBombermanController.currentTargetPosition;
            gridx.GetXY(position, out int x, out int y);

            if (powerUps.Count > 0)
            {
                var secTarget = powerUps
                    .OrderBy(a => Vector3.Distance(new Vector3(a.x, 0, a.y), position))
                    .FirstOrDefault();
                var secVector = new Vector3(secTarget.x, 0, secTarget.y);
                if (aiBombermanController.ComputePath(secVector))
                {
                    aiBombermanController.secondaryTargetPosition = new Vector3(secTarget.x, 0, secTarget.y);
                    aiBombermanController.secondaryTargetType = Gridx.Legend.Power;
                    return;
                }


                powerUps.Remove(secTarget);
            }

            walls = SearchGridFor((int)Gridx.Legend.DWall);
            if (walls.Count > 0)
            {
                var secTarget = walls
                    .OrderBy(a => Vector3.Distance(new Vector3(a.x, 0, a.y), position))
                    .FirstOrDefault();
                var secVector = new Vector3(secTarget.x, 0, secTarget.y);
                if (aiBombermanController.ComputePath(secVector))
                {
                    
                    aiBombermanController.secondaryTargetPosition = new Vector3(secTarget.x, 0, secTarget.y);
                    aiBombermanController.secondaryTargetType = Gridx.Legend.DWall;
                    return;
                }

                walls.Remove(secTarget);
            }

            var freeNeighbors = aiBombermanController.GetFreeNeighbors(aiBombermanController.transform.position);
            var freeNeighbor = freeNeighbors.FirstOrDefault(a => stageGrid[a.x, a.y] == 0);
            aiBombermanController.secondaryTargetPosition = new Vector3(freeNeighbor.x, 0, freeNeighbor.y);
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
        List<(int x, int y)> SearchGridFor(int value)
        {
            for (int i = 0; i < stageGrid.GetLength(0); i++)
            {
                for (int j = 0; j < stageGrid.GetLength(1); j++)
                {
                    if (stageGrid[i, j] == value)
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
            powerUps = new List<(int x, int y)>();
            walls = new List<(int x, int y)>();
            freeSpots = new List<(int x, int y)>();

            //SearchForASecondaryTargetOnTheWayToMainTarget(aiBombermanController.mainTarget.transform.position, 3);
        }

        public void OnExit()
        {
            Array.Clear(chosenGridObjectives.ToArray(), 0, chosenGridObjectives.Count);
            Array.Clear(secondaryTargets.ToArray(), 0, secondaryTargets.Count);
        }
    }
}