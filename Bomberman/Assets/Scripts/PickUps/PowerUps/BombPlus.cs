using System;
using PowerUps.Interfaces;
using UnityEngine;

public class BombPlus : MonoBehaviour, IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Bombs++;
        Destroy(transform.parent.gameObject);
    }

    public float ChanceToSpawn()
    {
        return 100;
    }
}