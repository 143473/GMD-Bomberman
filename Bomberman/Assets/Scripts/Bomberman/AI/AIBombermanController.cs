using System;
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
        
        
        public GameObject potentialTarget;

        private void Awake()
        {
            StageManager.onGridSet += SetGrid;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            stateMachine = new FiniteStateMachine();
            
            var search = new SearchForTarget();
            var moveToTarget = new MoveToTargetNavMesh();
            var placeBomb = new AIPlaceBomb();
            var takeCover = new TakeCover();
            
            // Adding state transitions 
            NewStateTransition(search, moveToTarget, CheckForTarget());
            NewStateTransition(moveToTarget, placeBomb, ReachedTarget());
            // NewStateTransition(moveToTarget, placeBomb, ReachedTarget());
            
            // State machine start
            stateMachine.SetState(search);
            
            // Conditions declaration
            Func<bool> CheckForTarget() => () => potentialTarget != null;

            Func<bool> ReachedTarget() => () => potentialTarget != null &&
                                                Vector3.Distance(transform.position,
                                                    potentialTarget.transform.position) < 1f;

            void NewStateTransition(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);
        }

        private void Update()
        {
            stateMachine.Tick();
        }

        void SetGrid(Gridx gridx)
        {
            this.gridx = gridx;
            pathfindingAStar = new PathfindingAStar(this.gridx);
        }
    }
}