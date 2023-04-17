using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class OnWallDestroy: MonoBehaviour, IDamage
    {
        public void OnDamage()
        {
            gameObject.GetComponent<PowerUpContainerSpawner>().SpawnPowerUpContainer();
        }
    }
}