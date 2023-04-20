using PowerUps.Interfaces;
using UnityEngine;

public class FlamePlus : MonoBehaviour, IPowerUp
{
    void Start()
    {
        
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.BombStats.Flame++;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
