using Unity.VisualScripting;
using UnityEngine;

namespace Bomberman
{
    public class AnimatorScript : MonoBehaviour
    {
        private Animator animator;
        private string state = "IsWalking";

        void Start()
        {
            animator = GetComponent<Animator>();
            BombermanCharacterController.onWalk += SetAnimator;
        }

        void SetAnimator(bool isWalking)
        {
            animator.SetBool(state, isWalking);
        }
    }
}