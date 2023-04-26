using System;
using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class BombPlus : MonoBehaviour, IPowerUp
{
    [SerializeField] private int extraBomb = 1;
    [SerializeField] private int chanceToSpawn = 100;
    private StatsHandler statsHandler;
    
    public void ApplyEffect(FinalBombermanStatsV2 finalBombermanStatsV2)
    {
        statsHandler = new StatsHandler(finalBombermanStatsV2);
        statsHandler.AddPermanentStat(Stats.Bombs, numericValue: extraBomb);
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return chanceToSpawn;
    }
}