
using UnityEngine;
using Utils;

namespace PowerUps.Interfaces
{
    public interface IPowerUp
    {
        void ApplyEffect(FinalBombermanStatsV2 finalBombermanStatsV2);
        float ChanceToSpawn();
    }
}
