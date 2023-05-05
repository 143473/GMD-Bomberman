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
        public GameObject potentialTarget;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            stateMachine = new FiniteStateMachine();
            
            var search = new SearchForTarget();
            var moveToTarget = new MoveToTarget();
            var placeBomb = new AIPlaceBomb();
            var takeCover = new TakeCover();
            
            // Adding state transitions 
            NewStateTransition(search, moveToTarget, CheckForTarget());
            NewStateTransition(moveToTarget, search, ReachedTarget());
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
    }
}