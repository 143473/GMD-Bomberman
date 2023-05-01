using Unity.VisualScripting;
using UnityEngine;

namespace Bomberman
{
    public class AnimatorScript : MonoBehaviour
    {
        private BombermanCharacterController controller;
        private Animator animator;
        private string state = "IsWalking";

        void Start()
        {
            controller = GetComponent<BombermanCharacterController>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            animator.SetBool(state, controller.isWalking);
        }
    }
}