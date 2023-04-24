namespace PickUps.Curses
{
    public class MaxStats : Curse
    {
        private void Awake()
        {
            var stats = gameObject.GetComponent<BombermanStats>();
            Flame = 15;
            Speed = 10;
            BombDelay = 2;
            Nasty = stats.Nasty;

            var inventory = gameObject.GetComponent<BombsInventory>();
            Bombs = inventory.Bombs;
            StartCoroutine(SelfDestroy());
        }

        private void Start()
        {

        }
    }
}