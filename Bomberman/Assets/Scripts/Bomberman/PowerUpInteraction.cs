using PowerUps.Interfaces;
using UnityEngine;

public class PowerUpInteraction : MonoBehaviour
{
    private void Start()
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