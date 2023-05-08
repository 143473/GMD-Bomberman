using Bomberman.AI.StatesV2.ControllerV2;
using UnityEngine;

namespace Bomberman.AI.StatesV2
{
    public class AiBomb : IState
    {
        private AIControllerV2 aiControllerV2;

        public AiBomb(AIControllerV2 aiControllerV2)
        {
            this.aiControllerV2 = aiControllerV2;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            Debug.Log("Placing bomb");
            aiControllerV2.gameObject.GetComponent<PlaceBomb>().Bomb();
        }

        public void OnExit()
        {
            aiControllerV2.placedBomb = true;
        }
    }
}