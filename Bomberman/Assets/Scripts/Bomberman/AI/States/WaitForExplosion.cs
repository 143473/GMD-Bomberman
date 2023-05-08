using UnityEngine;
using Utils;

namespace Bomberman.AI.States
{
    public class WaitForExplosion: IState
    {
        private AIBombermanController aiBombermanController;
        public float waitingTime = 3f;

        public WaitForExplosion(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
            waitingTime -= Time.deltaTime;
            if(waitingTime == 0f)
                aiBombermanController.placedBomb = false;
        }

        public void OnEnter()
        {
            Debug.Log("Waiting for explosion");
            waitingTime = aiBombermanController.GetComponent<FinalBombermanStats>().GetNumericStat(Stats.BombDelay)+1f;
        }

        public void OnExit()
        {
            aiBombermanController.placedBombLocation = new Vector3();
        }
    }
}