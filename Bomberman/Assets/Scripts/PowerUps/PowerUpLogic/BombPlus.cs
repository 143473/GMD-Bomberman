using PowerUps.Interfaces;

namespace PowerUps.PowerUpLogic
{
    public class BombPlus : IPowerUp
    {
        public void ApplyEffect(BombermanStats bombermanStats)
        {
            bombermanStats.Bombs++;
        }
    }
}
