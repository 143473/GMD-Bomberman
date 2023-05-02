using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class Kick : MonoBehaviour, IPowerUp
{
    [SerializeField] private int chanceToSpawn = 50;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStats finalBombermanStats)
    {
        statsHandler = new StatsHandler(finalBombermanStats);
        statsHandler.AddPermanentStat(Stats.Kick, boolValue: true);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}