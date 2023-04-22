using PowerUps.Interfaces;
using UnityEngine;

namespace PickUps.PowerUps
{
    public class AllowMultiple : MonoBehaviour, IPowerUp
    {
        public void ApplyEffect(BombermanStats bombermanStats)
        {
            bombermanStats.AllowMultiple = true;
            Destroy(transform.parent.gameObject);
        }

        public float ChanceToSpawn()
        {
            return 80;
        }
    }
}