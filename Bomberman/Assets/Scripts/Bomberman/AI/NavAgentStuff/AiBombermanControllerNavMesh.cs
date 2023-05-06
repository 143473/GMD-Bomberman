// using System;
// using Bomberman.AI.States;
// using Managers;
// using UnityEngine;
// using UnityEngine.AI;
//
// namespace Bomberman.AI
// {
//     public class AIBombermanControllerNavMesh: MonoBehaviour
//     {
//         private NavMeshAgent navMeshAgent;
//         private Animator animator;
//         private FiniteStateMachine stateMachine;
//         public GameObject potentialTarget;
//         private void Awake()
//         {
//             PlayerManager.onNavAgentAttachment += SetNavAgent;
//             animator = GetComponent<Animator>();
//             stateMachine = new FiniteStateMachine();
//         }
//
//         private void Update()
//         {
//             stateMachine.Tick();
//         }
//
//         void SetNavAgent()
//         {
//             navMeshAgent = GetComponent<NavMeshAgent>();
//             if (Physics.Raycast(new Ray(transform.position, Vector3.down), out var hit, LayerMask.GetMask("Ground")))
//             {
//                 navMeshAgent.Warp(hit.point);
//             }
//
//             var search = new SearchForTargetNavMesh(this, navMeshAgent);
//             var moveToTarget = new MoveToTargetNavMesh(navMeshAgent, animator, this);
//             var placeBomb = new AIPlaceBomb();
//             var takeCover = new SearchForCover();
//             
//             // Adding state transitions 
//             NewStateTransition(search, moveToTarget, CheckForTarget());
//             NewStateTransition(moveToTarget, search, ReachedTarget());
//             // NewStateTransition(moveToTarget, placeBomb, ReachedTarget());
//             
//             // State machine start
//             stateMachine.SetState(search);
//             
//             // Conditions declaration
//             Func<bool> CheckForTarget() => () => potentialTarget != null;
//
//             Func<bool> ReachedTarget() => () => potentialTarget != null &&
//                                                 Vector3.Distance(transform.position,
//                                                     potentialTarget.transform.position) < 1f;
//
//             void NewStateTransition(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);
//         }
//     }
// }