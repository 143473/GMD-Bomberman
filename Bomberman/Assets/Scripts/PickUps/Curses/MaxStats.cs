namespace PickUps.Curses
{
    public class MaxStats : Curse
    {
        private void Awake()
        {
            var stats = gameObject.GetComponent<BombermanStats>();
            Flame = 100;
            Speed = 15;
            AllowMultiple = stats.AllowMultiple;
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