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
        bombermanStats.BombStats.Kickable = true;
        Destroy(gameObject.transform.parent.gameObject);
    }
    
    public float ChanceToSpawn()
    {
        return 30;
    }
}