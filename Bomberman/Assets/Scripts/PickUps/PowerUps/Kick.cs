using PowerUps.Interfaces;
using UnityEngine;

public class Kick : MonoBehaviour, IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Kick = true;
        Destroy(transform.parent.gameObject);
    }
    
    public float ChanceToSpawn()
    {
        return 30;
    }
}