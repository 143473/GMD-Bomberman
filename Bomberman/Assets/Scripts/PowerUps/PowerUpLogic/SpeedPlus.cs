using PowerUps.Interfaces;
using UnityEngine;

public class SpeedPlus : MonoBehaviour, IPowerUp
{
    void Start()
    {
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Speed += 2;
        Destroy(gameObject.transform.parent.gameObject);
    }
}