using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

public class OnBombermanDestroy : MonoBehaviour, IDamage
{
    private StatsHandler _statsHandler;
    private FinalBombermanStatsV2 _finalBombermanStatsV2;
    public delegate void OnBombermanDeath(float lives, GameObject gameObject);
    public static OnBombermanDeath onBombermanDeath;

    private void Awake()
    {
        _finalBombermanStatsV2 = gameObject.GetComponent<FinalBombermanStatsV2>();
    }

    private void Start()
    {
        _statsHandler = new StatsHandler(_finalBombermanStatsV2);
    }

    public void OnDamage()
    {
        _statsHandler.AddPermanentStat(Stats.Lives, numericValue: -1);
        gameObject.transform.position = Vector3.up * int.MaxValue;
        gameObject.GetComponent<BombermanCharacterController>().enabled = false;
        
        onBombermanDeath?.Invoke(_finalBombermanStatsV2.numericStats[Stats.Lives], gameObject);
    }
}