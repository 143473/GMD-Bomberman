
using UnityEngine;

namespace PowerUps.Interfaces
{
    public interface IPowerUp
    {
        void ApplyEffect(BombermanStats bombermanStats);
        float ChanceToSpawn();
    }
}