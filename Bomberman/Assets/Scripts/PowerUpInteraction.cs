using System;
using PowerUps.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpInteraction : MonoBehaviour
{
    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider powerUp)
    {
        if (powerUp.TryGetComponent(out IPowerUp script))
        {
            script.ApplyEffect(gameObject.GetComponent<BombermanStats>());
        }
    }
}