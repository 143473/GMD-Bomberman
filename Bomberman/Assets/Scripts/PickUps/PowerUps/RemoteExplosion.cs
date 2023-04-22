using PowerUps.Interfaces;
using UnityEngine;


public class RemoteExplosion : MonoBehaviour, IPowerUp
{
    void Start()
    {
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.BombStats.RemoteExplosion = true;
        Destroy(transform.parent.gameObject);
    }
    
    public float ChanceToSpawn()
    {
        return 30;
    }
}