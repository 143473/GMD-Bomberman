using PowerUps.Interfaces;
using UnityEngine;

public class FlamePlus : MonoBehaviour, IPowerUp
{
    void Start()
    {
        
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Explosion++;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
