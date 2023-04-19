using System;
using PowerUps.Interfaces;
using UnityEngine;

public class BombPlus : MonoBehaviour, IPowerUp
{
    void Start()
    {
        
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Bombs++;
        Destroy(gameObject.transform.parent.gameObject);
    }
}