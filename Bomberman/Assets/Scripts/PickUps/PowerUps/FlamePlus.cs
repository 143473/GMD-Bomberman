using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class FlamePlus : MonoBehaviour, IPowerUp
{
    [SerializeField] private int extraFlame = 1;
    [SerializeField] private int chanceToSpawn = 100;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStats finalBombermanStats)
    {
        statsHandler = new StatsHandler(finalBombermanStats);
        statsHandler.AddPermanentStat(Stats.Flame, numericValue: extraFlame);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}
