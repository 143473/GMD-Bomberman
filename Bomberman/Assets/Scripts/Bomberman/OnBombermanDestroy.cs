using System;
using Interfaces;
using UnityEngine;
using Utils;

public class OnBombermanDestroy : MonoBehaviour, IDamage
{
    private StatsHandler _statsHandler;
    private FinalBombermanStatsV2 _finalBombermanStatsV2;

    private void Awake()
    {
        _finalBombermanStatsV2 = GetComponent<FinalBombermanStatsV2>();
        _statsHandler = new StatsHandler(_finalBombermanStatsV2);
    }

    public void OnDamage()
    {
        gameObject.SetActive(false);
        _statsHandler.AddPermanentStat(Stats.Lives, numericValue: -1);
    }
}