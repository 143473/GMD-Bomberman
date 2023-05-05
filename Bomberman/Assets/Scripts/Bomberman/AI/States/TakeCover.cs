using UnityEngine;

namespace Bomberman.AI.States
{
    public class TakeCover : IState
    {
        public void Tick()
        {
            Debug.Log("Chilling");
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