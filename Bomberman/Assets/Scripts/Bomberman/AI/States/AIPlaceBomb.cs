using Unity.VisualScripting;
using UnityEngine;

namespace Bomberman.AI.States
{
    public class AIPlaceBomb: IState
    {
        private AIBombermanController aiBombermanController;

        public AIPlaceBomb(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            aiBombermanController.gameObject.GetComponent<PlaceBomb>().Bomb();
            aiBombermanController.placedBombLocation = aiBombermanController.transform.position;
        }

        public void OnExit()
        {
        }
    }
}