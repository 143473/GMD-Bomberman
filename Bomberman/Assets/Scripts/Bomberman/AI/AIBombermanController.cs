using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bomberman.AI.States;
using Bomberman.AI.StatesV2;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utils;

namespace Bomberman.AI
{
    public class AIBombermanController : MonoBehaviour
    {
        private Animator animator;
        private FiniteStateMachine stateMachine;
        private Gridx gridx;
        private int[,] stageGridx;
        private PathfindingAStar pathfindingAStar;
        private SearchForTarget search;

        public List<Vector3> pathToTarget;
        public Vector3 placedBombLocation;
        public bool placedBomb;

        public Vector3 targetPosition;
        public Gridx.Legend targetType;

        public Vector3 potentialSafeSpot;

        private string state = "IsWalking";
        public bool isReacheable = false;
        public bool isStuck;
        public bool isMoving;

        private void Awake()
        {
            StageManager.onGridSet += SetGrid;
            pathToTarget = new List<Vector3>();
            potentialSafeSpot = new Vector3();
            placedBombLocation = new Vector3();

            targetPosition = new Vector3();
            targetType = Gridx.Legend.NonDWall;
        }

        private void Update()
        {
            stateMachine.Tick();
            animator.SetBool(state, isMoving);
        }

        public Gridx GetGrid()
        {
            return gridx;
        }

        private void OnEnable()
        {
            pathToTarget = new List<Vector3>();
            placedBombLocation = new Vector3();
            targetPosition = new Vector3();
            targetType = Gridx.Legend.None;
            potentialSafeSpot = new Vector3();
            isReacheable = false;

            StartCoroutine(Respawn());
        }

        IEnumerator Respawn()
        {
            yield return new WaitForSeconds(0.5f);
            if (gridx != null)
                stateMachine.SetState(search);
        }

        void SetGrid(Gridx gridx)
        {
            this.gridx = gridx;
            stageGridx = gridx.GetGrid();
            pathfindingAStar = new PathfindingAStar(this.gridx);

            animator = GetComponent<Animator>();
            animator.SetBool(state, false);
            stateMachine = new FiniteStateMachine();

            search = new SearchForTarget(this);
            var moveToTarget = new MoveToTarget(this, animator);
            var placeBomb = new AIPlaceBomb(this);
            var searchForCover = new SearchForCover(this);
            var waitForExplosion = new WaitForExplosion(this);
            var unstuck = new Unstuck(this);

            // Adding state transitions 
            NewStateTransition(search, moveToTarget, Target());
            NewStateTransition(moveToTarget, placeBomb, ReachedTarget());
            NewStateTransition(placeBomb, searchForCover, PlacedBomb());
            NewStateTransition(searchForCover, moveToTarget, HasSafeSpot());
            NewStateTransition(moveToTarget, waitForExplosion, IsDangerous());
            NewStateTransition(waitForExplosion, search, IsSafe());
            
            NewAnyStateTransition(search, ReachedPowerUp());

            // State machine start
            stateMachine.SetState(search);

            // Conditions declaration
            Func<bool> Target() => () => (targetPosition != Vector3.zero
                                          && isReacheable);

            Func<bool> ReachedPowerUp() => () => targetType == Gridx.Legend.Power && TargetTypeIsReached();
            Func<bool> ReachedTarget() => () => TargetTypeIsReached() && potentialSafeSpot == Vector3.zero && targetType != Gridx.Legend.Power;
            
            Func<bool> IsDangerous() => () =>
                FlameDetector(placedBombLocation, (int)GetComponent<FinalBombermanStats>().GetNumericStat(Stats.Flame))
                    .Any(a => GetFreeNeighbors(transform.position).Contains((a.x, a.y)));

            Func<bool> IsSafe() => () => waitForExplosion.waitingTime < 0.01f;

            // Func<bool> IsDangerous() => () => placedBomb;
            //
            // Func<bool> IsSafe() => () => waitForExplosion.waitingTime < 0.01f && placedBomb == false;

            Func<bool> HasSafeSpot() => () => potentialSafeSpot != Vector3.zero;
            Func<bool> PlacedBomb() => () => this.GetComponent<BombsInventory>().Bombs.Any(a => a.activeSelf);

            void NewStateTransition(IState from, IState to, Func<bool> condition) =>
                stateMachine.AddTransition(from, to, condition);
            void NewAnyStateTransition(IState anyState, Func<bool> condition) =>
                stateMachine.AddAnyTransition(anyState, condition);
        }
        
        bool TargetTypeIsReached()
        {
            switch (targetType)
            {
                case Gridx.Legend.DWall: return Vector3.Distance(transform.position, targetPosition) < 1.1f;
                case Gridx.Legend.Power: return Vector3.Distance(transform.position, targetPosition) < 0.1f;
                default: return Vector3.Distance(transform.position, targetPosition) < 1.1f;
            }
        }

        public List<(int x, int y)> FlameDetector(Vector3 bombPosition, int bombFlame)
        {
            List<(int x, int y)> flameVectors = new List<(int x, int y)>();
            GetGrid().GetXY(bombPosition, out int bX, out int bY);
            for (int i = bX - bombFlame; i <= bombFlame + bX; i++)
            {
                flameVectors.Add((i, bY));
            }

            for (int i = bY - bombFlame; i <= bombFlame + bY; i++)
            {
                flameVectors.Add((bX, i));
            }

            return flameVectors;
        }

        public List<(int x, int y)> GetFreeNeighbors(Vector3 current)
        {
            gridx.GetXY(current, out int x, out int y);
            var stageGrid = gridx.GetGrid();
            var neighbors = new List<(int x, int y)>()
            {
                (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1)
            };

            var maxX = stageGrid.GetLength(0);
            var maxY = stageGrid.GetLength(1);

            return neighbors
                .Where(tile => tile.x > 0 && tile.x < maxX)
                .Where(tile => tile.y > 0 && tile.y < maxY)
                .Where(tile => stageGrid[tile.x, tile.y] == 0 || stageGrid[tile.x, tile.y] == 1)
                .ToList();
        }

        public bool ComputePath(Vector3 target)
        {
            isReacheable = false;
            var currWorld = gameObject.transform.position;

            gridx.GetXY(currWorld, out int currx, out int curry);
            gridx.GetXY(target, out int targetx, out int targety);

            var pathToTargetTile = pathfindingAStar.GetPath((currx, curry), (targetx, targety));
            if (pathToTargetTile != null && pathToTargetTile.Any())
            {
                isReacheable = true;
                pathToTarget = new List<Vector3>();
                foreach (var tile in pathToTargetTile)
                {
                    pathToTarget.Add(new Vector3(tile.X, 0, tile.Y));
                }
                return true;
            }

            return false;
        }
    }
}