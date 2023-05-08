using System;
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
        private bool isMoving = false;
        private List<Vector3> pathToFollow;

        public MoveToTarget(AIBombermanController aiBombermanController, Animator animator)
        {
            this.aiBombermanController = aiBombermanController;
            this.animator = animator;
            pathToFollow = new List<Vector3>();
        }

        public void Tick()
        {
            if (Vector3.Distance(aiBombermanController.transform.position, lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            lastPosition = aiBombermanController.transform.position;
        }

        public void OnEnter()
        {
            Debug.Log($"Moving to target {aiBombermanController.secondaryTargetPosition.x} : {aiBombermanController.secondaryTargetPosition.z}");
            isMoving = true;
            TimeStuck = 0f;
            currentTargetIndex = 0;
            pathToFollow = aiBombermanController.pathToTarget;
            animator.SetBool(state, isMoving);
            aiBombermanController.StartCoroutine(Movement());
        }

        IEnumerator Movement()
        {
            if (isMoving)
            {
                while (currentTargetIndex < pathToFollow.Count)
                {
                    float step =
                        (aiBombermanController.gameObject.GetComponent<FinalBombermanStats>()
                            .GetNumericStat(Stats.Speed) - 3) * Time.deltaTime;
                    
                    aiBombermanController.transform.LookAt(pathToFollow[currentTargetIndex]);
                    aiBombermanController.transform.Rotate(0, 180, 0);
                    
                    aiBombermanController.transform.position =
                        Vector3.MoveTowards(aiBombermanController.transform.position
                            ,pathToFollow[currentTargetIndex], step);
                    

                    if (Vector3.Distance(aiBombermanController.transform.position,
                            pathToFollow[currentTargetIndex]) < 0.01)
                    {
                        currentTargetIndex++;
                    }

                    yield return null;
                }
            }

            isMoving = false;
        }

        public void OnExit()
        {
            animator.SetBool(state, false);
        }
    }
}