using PowerUps.Interfaces;
using UnityEngine;
using Utils;


public class RemoteExplosion : MonoBehaviour, IPowerUp
{
    [SerializeField] private int chanceToSpawn = 50;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStatsV2 finalBombermanStatsV2)
    {
        statsHandler = new StatsHandler(finalBombermanStatsV2);
        statsHandler.AddPermanentStat(Stats.RemoteExplosion, boolValue: true);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}