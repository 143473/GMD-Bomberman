using System;
using Interfaces;
using UnityEngine;

namespace PowerUps
{
    public class OnPickUpDestroy : MonoBehaviour, IDamage
    {
        private Vector3 position;
        private void Start()
        {
            position = gameObject.transform.position;
            if(gameObject.CompareTag("PowerUp"))
                Gridx.onGridValueChanged?.Invoke(position.x, position.z, 6);
            else
            {
                Gridx.onGridValueChanged?.Invoke(position.x, position.z, 7);
            }
        }

        private void OnDestroy()
        {
            Gridx.onGridValueChanged?.Invoke(position.x, position.z, 0);
        }

        public void OnDamage()
        {
            Destroy(gameObject);
        }
    }
}