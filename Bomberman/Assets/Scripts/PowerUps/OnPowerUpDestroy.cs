using Interfaces;
using UnityEngine;

namespace PowerUps
{
    public class OnPowerUpDestroy: MonoBehaviour, IDamage
    {
        public void OnDamage()
        {
            Destroy(gameObject);
        }
    }
}