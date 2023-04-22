using PowerUps.Interfaces;
using UnityEngine;

public class FlamePlus : MonoBehaviour, IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Flame++;
        Destroy(transform.parent.gameObject);
    }
    
    public float ChanceToSpawn()
    {
        return 100;
    }
}
