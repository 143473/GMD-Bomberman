using UnityEngine;

namespace PowerUps.PowerUpLogic
{
    public class RemoteExplosion:MonoBehaviour, IPowerUp
    {
        public void ApplyEffect(BombermanStats bombermanStats)
        {
            bombermanStats.RemoteExplosion = true;
        }
    }
}