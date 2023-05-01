using Unity.VisualScripting;
using UnityEngine;

namespace Bomberman
{
    public class AnimatorScript : MonoBehaviour
    {
        private Animator animator;
        private string state = "IsWalking";
        private BombermanCharacterController controller;
        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<BombermanCharacterController>();
            controller.onWalk += SetAnimator;
        }

        void SetAnimator(bool isWalking)
        {
            animator.SetBool(state, isWalking);
        }
    }
}