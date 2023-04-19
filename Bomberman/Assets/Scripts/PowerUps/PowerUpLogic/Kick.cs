using PowerUps.Interfaces;
using UnityEngine;

public class Kick : MonoBehaviour, IPowerUp
{
    void Start()
    {
        
    }
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Kick = true;
        Destroy(gameObject.transform.parent.gameObject);
    }
}