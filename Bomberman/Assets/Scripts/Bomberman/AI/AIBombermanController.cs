using System;
using System.Collections.Generic;
using System.Linq;
using Bomberman.AI.States;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI
{
    public class AIBombermanController: MonoBehaviour
    {
        private Animator animator;
        private FiniteStateMachine stateMachine;
        private Gridx gridx;
        private PathfindingAStar pathfindingAStar;
        public List<Vector3> pathToTarget;
        
        
        public GameObject potentialTarget;
        public Vector3 potentialDestinationVector;
        private string state = "IsWalking";

        private void Awake()
        {
            StageManager.onGridSet += SetGrid;
            pathToTarget = new List<Vector3>();
            potentialTarget = new GameObject();
            potentialDestinationVector = new Vector3();
        }
        private void FixedUpdate()
        {
            stateMachine.Tick();
        }

        public Gridx GetGrid()
        {
            return gridx;
        }

        void SetGrid(Gridx gridx)
        {
            this.gridx = gridx;
            pathfindingAStar = new PathfindingAStar(this.gridx);
            
            animator = GetComponent<Animator>();
            animator.SetBool(state, false);
            stateMachine = new FiniteStateMachine();
            
            var search = new SearchForTarget(this);
            var moveToTarget = new MoveToTarget(this, animator);
            var placeBomb = new AIPlaceBomb(this);
            var searchForCover = new SearchForCover(this);
            
            // Adding state transitions 
            NewStateTransition(search, moveToTarget, CheckForTarget());
            NewStateTransition(moveToTarget, placeBomb, ReachedTarget());
            NewStateTransition(placeBomb, searchForCover, PlacedBomb());
            NewStateTransition(searchForCover, moveToTarget, HasSafeSpot());
            
            // NewStateTransition(moveToTarget, search, StuckForASecond());
            //NewStateTransition(moveToTarget, moveToTarget, TargetChangedPosition());

            // State machine start
            stateMachine.SetState(search);
            
            // Conditions declaration
            Func<bool> TargetChangedPosition() => () => potentialTarget !=null &&
                                                        (potentialTarget.transform.position.x != pathToTarget[pathToTarget.Count-1].x ||
                                                        potentialTarget.transform.position.y != pathToTarget[pathToTarget.Count-1].y)

            ;
            Func<bool> CheckForTarget() => () => potentialTarget != null && ComputePath(potentialTarget.transform.position);
            Func<bool> StuckForASecond() => () => moveToTarget.TimeStuck > 0.7f;
            Func<bool> HasSafeSpot() => () => potentialDestinationVector != null;
            Func<bool> PlacedBomb() => () => this.GetComponent<BombsInventory>().Bombs.Any(a => a.activeSelf);
            Func<bool> ReachedTarget() => () => potentialTarget != null &&
                                                Vector3.Distance(transform.position,
                                                    potentialTarget.transform.position) <= 1.2f;
            

            void NewStateTransition(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        }

        public bool ComputePath(Vector3 target)
        {
            pathToTarget = new List<Vector3>();
            var currWorld = gameObject.transform.position;
            var targetWorld = target;

            gridx.GetXY(currWorld, out int currx, out int curry);
            gridx.GetXY(targetWorld, out int targetx, out int targety);

            var pathToTargetTile = pathfindingAStar.GetPath((currx, curry), (targetx, targety));
            if (pathToTargetTile != null && pathToTargetTile.Any())
            {
                foreach (var tile in pathToTargetTile)
                {
                    pathToTarget.Add(new Vector3(tile.X,0,tile.Y));
                }
                return true;
            }
                
            return false;
        }
    }
}