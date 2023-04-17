using UnityEngine;
public class SpeedPlus : MonoBehaviour,IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Speed++;
    }
}
