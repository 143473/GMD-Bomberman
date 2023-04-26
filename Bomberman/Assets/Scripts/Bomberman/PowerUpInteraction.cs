using PowerUps.Interfaces;
using UnityEngine;
using Utils;

public class PowerUpInteraction : MonoBehaviour
{
    private FinalBombermanStatsV2 _finalBombermanStatsV2;

    private void Start()
    {
        _finalBombermanStatsV2 = GetComponent<FinalBombermanStatsV2>();

    }
    private void OnTriggerEnter(Collider powerUp)
    {
        if (powerUp.TryGetComponent(out IPowerUp script))
        {
            script.ApplyEffect(_finalBombermanStatsV2);
        }
    }
}