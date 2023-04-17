using UnityEngine;

public class FlamePlus : MonoBehaviour, IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Explosion++;
    }
}
