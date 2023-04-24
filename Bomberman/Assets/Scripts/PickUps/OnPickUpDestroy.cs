using Interfaces;
using UnityEngine;

namespace PowerUps
{
    public class OnPickUpDestroy : MonoBehaviour, IDamage
    {
        public void OnDamage()
        {
            Destroy(gameObject);
        }
    }
}