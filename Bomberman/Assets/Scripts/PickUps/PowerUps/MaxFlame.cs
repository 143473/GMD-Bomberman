using PowerUps.Interfaces;
using UnityEngine;
using Utils;

namespace PickUps.PowerUps
{
    public class MaxFlame : MonoBehaviour, IPowerUp
    {
        [SerializeField] private int maxFlame = 12;
        [SerializeField] private int chanceToSpawn = 10;
        private StatsHandler statsHandler;
    
        public void ApplyEffect(FinalBombermanStatsV2 finalBombermanStatsV2)
        {
            statsHandler = new StatsHandler(finalBombermanStatsV2);
            statsHandler.AddPermanentStat(Stats.Flame, numericValue: maxFlame);
            Destroy(transform.parent.gameObject);
        }

        public float ChanceToSpawn()
        {
            return chanceToSpawn;
        }
    }
}