using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class Kick : MonoBehaviour, IPowerUp
{
    [SerializeField] private bool kick = false;
    [SerializeField] private int chanceToSpawn = 50;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStatsV2 finalBombermanStatsV2)
    {
        statsHandler = new StatsHandler(finalBombermanStatsV2);
        statsHandler.AddPermanentStat(Stats.Kick, boolValue: kick);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}