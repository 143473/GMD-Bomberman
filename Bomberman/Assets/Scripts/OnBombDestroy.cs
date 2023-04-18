using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class OnBombDestroy: MonoBehaviour, IDamage
    {
        public void OnDamage()
        {
            gameObject.GetComponent<BombScript>().Explode();
        }
    }
}