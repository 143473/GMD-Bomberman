using System;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI
{
    public class AIBombermanNavAgent : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;

        private string state = "IsWalking";

        private void Awake()
        {
            PlayerManager.onNavAgentAttachment += GetNavAgent;
        }

        void GetNavAgent()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            // if (navMeshAgent != null)
            // {
            //     if (navMeshAgent.velocity.magnitude == 0)
            //         animator.SetBool(state, false);
            //     else
            //         animator.SetBool(state, true);
            // }
        }
    }
}