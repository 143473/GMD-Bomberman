using DefaultNamespace;
using PowerUps.Interfaces;
using UnityEngine;

public class Kick : MonoBehaviour, IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Kick = true;
    }
}