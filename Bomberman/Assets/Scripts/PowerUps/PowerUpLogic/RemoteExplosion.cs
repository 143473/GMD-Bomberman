using PowerUps.Interfaces;
using UnityEngine;

namespace PowerUps.PowerUpLogic
{
    public class RemoteExplosion: IPowerUp
    {
        public void ApplyEffect(BombermanStats bombermanStats)
        {
            bombermanStats.RemoteExplosion = true;
        }
    }
}