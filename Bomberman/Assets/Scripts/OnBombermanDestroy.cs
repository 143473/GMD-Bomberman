using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class OnBombermanDestroy: MonoBehaviour, IDamage
    {
        public void OnDamage()
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<BombermanStats>().Lives--;
        }
    }
}