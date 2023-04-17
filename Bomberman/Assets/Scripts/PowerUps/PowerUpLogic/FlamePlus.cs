using PowerUps.Interfaces;

public class FlamePlus : IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Explosion++;
    }
}
