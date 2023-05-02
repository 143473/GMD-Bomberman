using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class SpeedPlus : MonoBehaviour, IPowerUp
{
    [SerializeField] private int extraSpeed = 1;
    [SerializeField] private int chanceToSpawn = 100;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStats finalBombermanStats)
    {
        statsHandler = new StatsHandler(finalBombermanStats);
        statsHandler.AddPermanentStat(Stats.Speed, numericValue: extraSpeed);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}