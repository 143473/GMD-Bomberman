using UnityEngine;

namespace Bomberman.AI.States
{
    public class AIPlaceBomb: IState
    {
        public void Tick()
        {
            Debug.Log("Bomb");
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