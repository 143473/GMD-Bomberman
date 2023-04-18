using PowerUps.Interfaces;

public class Kick : IPowerUp
{
    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Kick = true;
    }
}