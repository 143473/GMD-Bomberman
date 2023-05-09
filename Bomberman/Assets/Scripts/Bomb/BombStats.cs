using UnityEngine;

public class BombStats : MonoBehaviour
{
    public float Flame { get; set; } = 1;
    public bool RemoteExplosion { get; set; } = false;
    public float BombDelay { get; set; } = 3;

    public void SetStats(float flame, bool remote, float bombDelay)
    {
        Flame = flame;
        RemoteExplosion = remote;
        BombDelay = bombDelay;
    }
}