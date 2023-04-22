using PowerUps.Interfaces;
using UnityEngine;

public class SpeedPlus : MonoBehaviour, IPowerUp
{
    void Start()
    {
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Speed += 1;
        Destroy(transform.parent.gameObject);
    }
    
    public float ChanceToSpawn()
    {
        return 100;
    }
}