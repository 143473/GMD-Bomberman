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
        public float TimeStuck = 0f;
        private Vector3 lastPosition = Vector3.zero;
        private Gridx gridx;
        private List<(int x, int y)> powerUps;
        private List<(int x, int y)> free;
        private List<(int x, int y)> walls;
        private GameObject player;

        public SearchForTarget(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
            if (Vector3.Distance(aiBombermanController.transform.position, lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;
            if (TimeStuck > 0.1f)
            {
                aiBombermanController.isStuck = true;
            }
            
            lastPosition = aiBombermanController.transform.position;
            
            ChooseOneOfTheNearestTargets();
        }

        bool ChooseOneOfTheNearestTargets()
        {
            Debug.Log("Searching");

            powerUps = SearchGridFor((int)Gridx.Legend.Power);
            if (ChooseObjective(powerUps, Gridx.Legend.Power))
                return true;

            walls = SearchGridFor((int)Gridx.Legend.DWall);
            if(ChooseObjective(walls, Gridx.Legend.DWall))
                return true;
            
            player = FindNearestPlayer();
            if (aiBombermanController.ComputePath(player.transform.position))
            {
                aiBombermanController.targetPosition = player.transform.position;
                return true;
            }
            
            free = SearchGridFor((int)Gridx.Legend.Free);
            if (ChooseObjective(free, Gridx.Legend.Free))
                return true;
               
            return false;
        }

        bool ChooseObjective(List<(int x, int y)> list, Gridx.Legend objectiveType)
        {
            var position = aiBombermanController.transform.position;

            if (list.Count > 0)
            {
                var secTarget = list
                    .OrderBy(a => Vector3.Distance(new Vector3(a.x, 0, a.y), position))
                    .FirstOrDefault();
                var secVector = new Vector3(secTarget.x, 0, secTarget.y);
                if (aiBombermanController.ComputePath(secVector))
                {
                    
                    aiBombermanController.targetPosition = new Vector3(secTarget.x, 0, secTarget.y);
                    aiBombermanController.targetType = objectiveType;
                    return true;
                }

                list.Remove(secTarget);
            }

            return false;
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

        public void OnEnter()
        {
            TimeStuck = 0f;
            gridx = aiBombermanController.GetGrid();
            stageGrid = aiBombermanController.GetGrid().GetGrid();
            powerUps = new List<(int x, int y)>();
            walls = new List<(int x, int y)>();
        }

        public void OnExit()
        {
            powerUps.Clear();
            walls.Clear();
        }
    }
}