using PowerUps.Interfaces;
using UnityEngine;


public class RemoteExplosion : MonoBehaviour, IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.RemoteExplosion = true;
        Destroy(transform.parent.gameObject);
    }
    
    public float ChanceToSpawn()
    {
        return 30;
    }
}