using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class SpeedPlus : MonoBehaviour, IPowerUp
{
    [SerializeField] private int extraSpeed = 1;
    [SerializeField] private int chanceToSpawn = 70;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStatsV2 finalBombermanStatsV2)
    {
        statsHandler = new StatsHandler(finalBombermanStatsV2);
        statsHandler.AddPermanentStat(Stats.Speed, numericValue: extraSpeed);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}