using PowerUps.Interfaces;
using UnityEngine;


public class RemoteExplosion : MonoBehaviour, IPowerUp
{
    void Start()
    {
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.RemoteExplosion = true;
        Destroy(gameObject.transform.parent.gameObject);
    }
}