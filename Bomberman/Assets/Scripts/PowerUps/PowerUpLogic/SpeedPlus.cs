using PowerUps.Interfaces;
using UnityEngine;
public class SpeedPlus : IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Speed++;
    }
}
