using Bomberman.AI.StatesV2.ControllerV2;
using UnityEngine;

namespace Bomberman.AI.StatesV2
{
    public class Unstuck : IState
    {
        private AIControllerV2 aiControllerV2;

        public Unstuck(AIControllerV2 aiControllerV2)
        {
            this.aiControllerV2 = aiControllerV2;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            aiControllerV2.transform.position = aiControllerV2.transform.position*0.05f;
        }

        public void OnExit()
        {
            aiControllerV2.stuck = false;
        }
    }
}