public class BombermanStats
{
    public string Name { get; set; }
    public int Lives { get; set; }
    public int Bombs { get; set; } = 1;
    public int Explosion { get; set; } = 1;
    public int Speed { get; set; } = 1;
    public bool RemoteExplosion { get; set; } = false;
    public bool Kick { get; set; } = false;
    public bool Curse { get; set; } = false;
}