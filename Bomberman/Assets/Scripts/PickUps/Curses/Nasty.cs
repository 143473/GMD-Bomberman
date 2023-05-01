// namespace PickUps.Curses
// {
//     public class Nasty : Curse
//     {
//         private void Awake()
//         {
//             var stats = gameObject.GetComponent<BombermanStats>();
//             Flame = stats.Flame;
//             Speed = 10;
//             BombDelay = 2;
//             Nasty = true;
//
//             var inventory = gameObject.GetComponent<BombsInventory>();
//             Bombs = inventory.Bombs;
//             StartCoroutine(SelfDestroy());
//         }
//     }
// }