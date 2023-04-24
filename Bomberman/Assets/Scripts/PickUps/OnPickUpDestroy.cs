using Interfaces;
using UnityEngine;

namespace PowerUps
{
    public class OnPickUpDestroy : MonoBehaviour, IDamage
    {
        void Start()
        {
        
        }
        public void OnDamage()
        {
            Destroy(gameObject);
        }
    }
}