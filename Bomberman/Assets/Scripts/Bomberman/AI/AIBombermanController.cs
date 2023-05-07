using System;
using System.Collections.Generic;
using System.Linq;
using Bomberman.AI.States;
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
        private PathfindingAStar pathfindingAStar;
        private SearchForTarget search;

        public List<Vector3> pathToTarget;
        public Vector3 placedBombLocation;

        public GameObject potentialTarget;
        public Vector3 secondaryTargetPosition;
        public Gridx.Legend secondaryTargetType;

        public GameObject currentTarget;

        public GameObject mainTarget;

        public Vector3 potentialDestinationVector;

        private string state = "IsWalking";
        public bool isReacheable = false;
        public bool placedBomb = false;
        public bool targetChanged = false;

        private void Awake()
        {
            StageManager.onGridSet += SetGrid;
            pathToTarget = new List<Vector3>();
            potentialTarget = new GameObject();
            potentialDestinationVector = new Vector3();
            placedBombLocation = new Vector3();

            secondaryTargetPosition = new Vector3();
            secondaryTargetType = Gridx.Legend.NonDWall;
        }

        private void Update()
        {
            stateMachine.Tick();
        }

        public Gridx GetGrid()
        {
            return gridx;
        }

        private void OnEnable()
        {
            if (gridx != null)
                stateMachine.SetState(search);
        }

        void SetGrid(Gridx gridx)
        {
            this.gridx = gridx;
            pathfindingAStar = new PathfindingAStar(this.gridx);

            animator = GetComponent<Animator>();
            animator.SetBool(state, false);
            stateMachine = new FiniteStateMachine();

            search = new SearchForTarget(this);
            var moveToTarget = new MoveToTarget(this, animator);
            var placeBomb = new AIPlaceBomb(this);
            var searchForCover = new SearchForCover(this);
            var waitForExplosion = new WaitForExplosion(this);
            var inspectArea = new InspectArea(this);

            // Adding state transitions 
            NewStateTransition(search, moveToTarget, SecondaryTarget());
            NewStateTransition(search, moveToTarget, NoSecondaryTargets());
            
            NewStateTransition(moveToTarget, inspectArea, DidntReachTarget());
            NewStateTransition(inspectArea, moveToTarget, SameTarget());
            
            NewStateTransition(moveToTarget, placeBomb, ReachedTarget());
            NewStateTransition(placeBomb, searchForCover, PlacedBomb());
            NewStateTransition(searchForCover, moveToTarget, HasSafeSpot());
            // NewStateTransition(moveToTarget, waitForExplosion, IsDangerous());
            // NewStateTransition(waitForExplosion, search, IsSafe());


            // NewStateTransition(placeBomb, searchForCover, PlacedBomb());

            NewStateTransition(moveToTarget, search, StuckForASecond());


            // NewStateTransition(placeBomb, search, HasMoreBombs());
            // NewStateTransition(placeBomb, search, EnoughTime());
            // NewStateTransition(moveToTarget, waitingForBombs, NoBombsAvailable());


            // NewStateTransition(moveToTarget, search, StuckForASecond());


            //NewStateTransition(moveToTarget, moveToTarget, TargetChangedPosition());

            // State machine start
            stateMachine.SetState(search);

            // Conditions declaration
            Func<bool> SecondaryTarget() => () => secondaryTargetPosition != Vector3.zero
                                                  && secondaryTargetType != Gridx.Legend.None
                                                  && isReacheable;

            Func<bool> NoSecondaryTargets() => () => secondaryTargetPosition == Vector3.zero
                                                     && secondaryTargetType == Gridx.Legend.None
                                                     && mainTarget != null;

            Func<bool> ReachedTarget() => () => secondaryTargetPosition != Vector3.zero
                                                && secondaryTargetType != Gridx.Legend.Power
                                                && Vector3.Distance(
                                                    new Vector3(transform.position.x, 0, transform.position.z),
                                                    secondaryTargetPosition) <= 1.1f && moveToTarget.TimeStuck > 0.1f;

            Func<bool> DidntReachTarget() => () => Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), pathToTarget.LastOrDefault())<0.1f
                && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), secondaryTargetPosition) > 2f
                && secondaryTargetPosition != Vector3.zero
                && secondaryTargetType != Gridx.Legend.None;

            Func<bool> SameTarget() => () => secondaryTargetPosition != Vector3.zero
                                             && secondaryTargetType != Gridx.Legend.None
                                             && isReacheable
                                             && targetChanged == false
                                             && Vector3.Distance(
                                                 new Vector3(transform.position.x, 0, transform.position.z),
                                                 secondaryTargetPosition) > 1.1f;
            


            Func<bool> IsDangerous() => () =>
                FlameDetector(placedBombLocation, (int)GetComponent<FinalBombermanStats>().GetNumericStat(Stats.Flame))
                    .Any(a => GetFreeNeighbors(transform.position).Contains((a.x, a.y)));

            // Check if it is safe after 2 Vectors, after continue to the target
            Func<bool> IsSafe() => () => FlameDetector(placedBombLocation, (int)GetComponent<FinalBombermanStats>().GetNumericStat(Stats.Flame))
                .Any(a => !GetFreeNeighbors(transform.position).Contains((a.x, a.y)));

            Func<bool> HasMoreBombs() =>
                () => this.GetComponent<BombsInventory>().Bombs.Any(a => a.activeSelf == false);

            Func<bool> EnoughTime() => () => waitForExplosion.waitingTime > 2.5f;
            Func<bool> NoBombsAvailable() => () => this.GetComponent<BombsInventory>().Bombs.All(a => a.activeSelf);
            Func<bool> StuckForASecond() => () => moveToTarget.TimeStuck > 0.3f;
            Func<bool> HasSafeSpot() => () => potentialDestinationVector != Vector3.zero;
            Func<bool> PlacedBomb() => () => this.GetComponent<BombsInventory>().Bombs.Any(a => a.activeSelf);

            void NewStateTransition(IState from, IState to, Func<bool> condition) =>
                stateMachine.AddTransition(from, to, condition);
        }

        // public void SetDistance(int targetX, int targetY)
        // {
        //     this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        // }

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
            pathToTarget = new List<Vector3>();
            var currWorld = gameObject.transform.position;

            gridx.GetXY(currWorld, out int currx, out int curry);
            gridx.GetXY(target, out int targetx, out int targety);

            var pathToTargetTile = pathfindingAStar.GetPath((currx, curry), (targetx, targety));
            if (pathToTargetTile != null && pathToTargetTile.Any())
            {
                isReacheable = true;
                foreach (var tile in pathToTargetTile)
                {
                    pathToTarget.Add(new Vector3(tile.X, 0, tile.Y));
                }

                if (pathToTarget.Count > 3)
                    pathToTarget = pathToTarget.Take(3).ToList();
                // if (pathToTarget.LastOrDefault() == secondaryTargetPosition)
                //     pathToTarget.Remove(pathToTarget.LastOrDefault());
                return true;
            }

            return false;
        }
    }
}