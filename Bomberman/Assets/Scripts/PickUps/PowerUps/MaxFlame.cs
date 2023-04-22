using PowerUps.Interfaces;
using UnityEngine;

namespace PickUps.PowerUps
{
    public class MaxFlame : MonoBehaviour, IPowerUp
    {
        public void ApplyEffect(BombermanStats bombermanStats)
        {
            bombermanStats.Flame = 100;
        }

        public float ChanceToSpawn()
        {
            return 10;
        }
    }
}