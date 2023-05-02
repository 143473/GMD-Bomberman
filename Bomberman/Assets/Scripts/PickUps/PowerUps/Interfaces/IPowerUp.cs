
using UnityEngine;
using Utils;

namespace PowerUps.Interfaces
{
    public interface IPowerUp
    {
        void ApplyEffect(FinalBombermanStats finalBombermanStats);
        float ChanceToSpawn();
    }
}
