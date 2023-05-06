using System;
using Bomberman.AI;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

public class OnBombermanDestroy : MonoBehaviour, IDamage
{
    private StatsHandler _statsHandler;
    private FinalBombermanStats finalBombermanStats;
    public delegate void OnBombermanDeath(float lives, GameObject gameObject);
    public static OnBombermanDeath onBombermanDeath;

    private void Awake()
    {
        finalBombermanStats = gameObject.GetComponent<FinalBombermanStats>();
    }

    private void Start()
    {
        _statsHandler = new StatsHandler(finalBombermanStats);
    }

    public void OnDamage()
    {
        _statsHandler.AddPermanentStat(Stats.Lives, numericValue: -1);
        gameObject.transform.position = Vector3.up * int.MaxValue;
        if(gameObject.GetComponent<BombermanCharacterController>() != null)
            gameObject.GetComponent<BombermanCharacterController>().enabled = false;
        else if (gameObject.GetComponent<AIBombermanController>() != null)
            gameObject.GetComponent<AIBombermanController>().enabled = false;
        
        onBombermanDeath?.Invoke(finalBombermanStats.numericStats[Stats.Lives], gameObject);
    }
}