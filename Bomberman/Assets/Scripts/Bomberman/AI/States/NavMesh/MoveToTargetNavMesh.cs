using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Bomberman.AI.States
{
    public class MoveToTargetNavMesh: IState
    {
        // private NavMeshAgent navMeshAgent;
        // private Animator animator;
        // private AIBombermanController aiBombermanController;
        //
        // public float TimeStuck;
        // private FinalBombermanStats bombermanStats;
        // private string state = "IsWalking";
        // private Vector3 lastPosition = Vector3.zero;
        //
        // public MoveToTargetNavMesh(NavMeshAgent navMeshAgent, Animator animator, AIBombermanController aiBombermanController)
        // {
        //     this.navMeshAgent = navMeshAgent;
        //     this.animator = animator;
        //     this.aiBombermanController = aiBombermanController;
        //     bombermanStats = this.aiBombermanController.GetComponent<FinalBombermanStats>();
        // }
        //
        // public void Tick()
        // {
        //     if (Vector3.Distance(aiBombermanController.transform.position, lastPosition) <= 0f)
        //         TimeStuck += Time.deltaTime;
        //
        //     lastPosition = aiBombermanController.transform.position;
        // }
        //
        // public void OnEnter()
        // {
        //     TimeStuck = 0f;
        //     navMeshAgent.enabled = true;
        //     navMeshAgent.SetDestination(aiBombermanController.potentialTarget.transform.position);
        //     animator.SetBool(state, true);
        // }
        //
        // public void OnExit()
        // {
        //     navMeshAgent.enabled = false;
        //     animator.SetBool(state, false);
        // }
        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}