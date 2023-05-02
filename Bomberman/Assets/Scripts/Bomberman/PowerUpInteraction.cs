using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class PowerUpInteraction : MonoBehaviour
{
    private FinalBombermanStats finalBombermanStats;

    private void Start()
    {
        finalBombermanStats = GetComponent<FinalBombermanStats>();

    }
    private void OnTriggerEnter(Collider powerUp)
    {
        if (powerUp.TryGetComponent(out IPowerUp script))
        {
            script.ApplyEffect(finalBombermanStats);
        }
    }
}