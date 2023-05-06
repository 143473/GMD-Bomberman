using UnityEngine;
using Utils;

namespace Bomberman.AI.States
{
    public class WaitForExplosion: IState
    {
        private AIBombermanController aiBombermanController;
        public float waitingTime = 0f;

        public WaitForExplosion(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
            waitingTime -= Time.deltaTime;
        }

        public void OnEnter()
        {
            waitingTime = aiBombermanController.GetComponent<FinalBombermanStats>().GetNumericStat(Stats.BombDelay);
        }

        public void OnExit()
        {
            
        }
    }
}