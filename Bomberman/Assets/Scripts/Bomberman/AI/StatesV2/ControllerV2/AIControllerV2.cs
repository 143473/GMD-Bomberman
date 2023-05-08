using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Utils;

namespace Bomberman.AI.StatesV2.ControllerV2
{
    public class AIControllerV2 : MonoBehaviour
    {
        private Animator animator;
        private FiniteStateMachine stateMachine;
        private Gridx gridx;
        private PathfindingAStar pathfindingAStar;
        private SearchTargetPriority search;

        private int[,] bombermanMap;
        public List<Vector3> fullPathToATarget;
        public Vector3 targetPosition;
        public Gridx.Legend targetType;

        public Vector3 nextCell;
        private string state = "IsWalking";
        private bool isReacheable;
        private Vector3 lastPosition = Vector3.zero;
        public float TimeStuck;

        // State conditions
        private bool isMoving = true;
        public bool checkedForBombs;
        public bool placedBomb;
        public bool searching;
        public bool targetReached;
        public bool stuck;


        private float step;
        private float time;
        private float timeDelay = 0.1f;
        private bool moveTransition;

        private void Awake()
        {
            StageManager.onGridSet += SetGrid;
            step = (GetComponent<FinalBombermanStats>().GetNumericStat(Stats.Speed) / 2f) * Time.deltaTime;
            moveTransition = false;
        }

        private void FixedUpdate()
        {
            animator.SetBool(state, isMoving);

            time = time + 1f * Time.deltaTime;
            if (time >= timeDelay)
            {
                time = 0f;
                stateMachine.Tick();
            }

            if (Vector3.Distance(transform.position, lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            lastPosition = transform.position;

            transform.LookAt(nextCell);
            transform.Rotate(0, 180, 0);

            transform.position =
                Vector3.MoveTowards(transform.position
                    , nextCell, step);

            if (Vector3.Distance(transform.position,
                    nextCell) < 0.01f)
            {
                transform.position = nextCell;
            }
        }

        public Gridx GetGridx()
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
            bombermanMap = gridx.GetGrid();
            pathfindingAStar = new PathfindingAStar(this.gridx);

            animator = GetComponent<Animator>();
            stateMachine = new FiniteStateMachine();

            search = new SearchTargetPriority(this);
            var check = new CheckForBombs(this);
            // var move = new Move(this);
            var bomb = new AiBomb(this);
            var unstuck = new Unstuck(this);


            // Adding state transitions 
            NewStateTransition(search, check, HasTarget());
            NewStateTransition(check, search, Checked());
            NewAnyStateTransition(bomb, TargetReached());


            // State machine start
            stateMachine.SetState(search);

            // Conditions declaration
            Func<bool> HasTarget() => () => nextCell != null && isReacheable;
            Func<bool> Checked() => () => !checkedForBombs;
            Func<bool> TargetReached() => () => targetReached && TargetTypeIsReached();
            Func<bool> IsStuck() => () => stuck;
            Func<bool> IsNotStuck() => () => !stuck;


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
            GetGridx().GetXY(bombPosition, out int bX, out int bY);
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
                fullPathToATarget = new List<Vector3>();
                foreach (var path in pathToTargetTile)
                {
                    fullPathToATarget.Add(new Vector3(path.X, 0, path.Y));
                }

                targetPosition = target;
                isReacheable = true;
                if (pathToTargetTile.Count >= 2)
                    nextCell = new Vector3(pathToTargetTile[1].X, 0, pathToTargetTile[1].Y);
                else targetReached = true;
                return true;
            }

            return false;
        }
    }
}