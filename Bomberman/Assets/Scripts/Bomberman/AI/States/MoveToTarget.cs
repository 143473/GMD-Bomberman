using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Utils;

namespace Bomberman.AI.States
{
    public class MoveToTarget : IState
    {
        private AIBombermanController aiBombermanController;
        private Animator animator;
        private string state = "IsWalking";
        private int currentTargetIndex = 0;
        private Vector3 lastPosition = Vector3.zero;
        public float TimeStuck = 0f;

        public MoveToTarget(AIBombermanController aiBombermanController, Animator animator)
        {
            this.aiBombermanController = aiBombermanController;
            this.animator = animator;
        }

        public void Tick()
        {
            if (Vector3.Distance(aiBombermanController.transform.position, lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            lastPosition = aiBombermanController.transform.position;
        }

        public void OnEnter()
        {
            animator.SetBool(state, true);
            aiBombermanController.StartCoroutine(Movement());
        }

        IEnumerator Movement()
        {
            while(currentTargetIndex < aiBombermanController.pathToTarget.Count)
            {
                float step = (aiBombermanController.gameObject.GetComponent<FinalBombermanStats>().GetNumericStat(Stats.Speed)-1) * Time.deltaTime;
                aiBombermanController.transform.position = 
                    Vector3.MoveTowards(aiBombermanController.transform.position
                        , aiBombermanController.pathToTarget[currentTargetIndex], step);
                
                if (Vector3.Distance(aiBombermanController.transform.position, aiBombermanController.pathToTarget[currentTargetIndex]) < 0.01)
                {
                    currentTargetIndex++;
                }
                yield return null;
            }
        }

        public void OnExit()
        {
            animator.SetBool(state, false);
            currentTargetIndex = 0;
        }
    }
}