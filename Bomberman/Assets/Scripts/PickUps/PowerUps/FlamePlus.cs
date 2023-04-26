using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class FlamePlus : MonoBehaviour, IPowerUp
{
    [SerializeField] private int extraFlame = 1;
    [SerializeField] private int chanceToSpawn = 80;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStatsV2 finalBombermanStatsV2)
    {
        statsHandler = new StatsHandler(finalBombermanStatsV2);
        statsHandler.AddPermanentStat(Stats.Flame, numericValue: extraFlame);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}
