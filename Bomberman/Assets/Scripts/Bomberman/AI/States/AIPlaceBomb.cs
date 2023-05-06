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
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}