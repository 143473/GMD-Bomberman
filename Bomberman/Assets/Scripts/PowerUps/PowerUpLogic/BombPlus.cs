using UnityEngine;

public class BombPlus : MonoBehaviour, IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Bombs++;
    }
}
