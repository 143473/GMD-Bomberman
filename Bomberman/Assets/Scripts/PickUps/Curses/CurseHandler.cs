using UnityEngine;
using Random = UnityEngine.Random;

namespace PickUps.Curses
{
    public class CurseHandler: MonoBehaviour
    {
        [SerializeField] protected float availabilityInSeconds = 10;
        [SerializeField] protected GameObject[] curses;
        
        private void Awake()
        {
            Destroy(gameObject, availabilityInSeconds);
        }
        
        private void OnTriggerEnter(Collider player)
        {
            var curseNumber = Random.Range(0, curses.Length);
            var curse = curses[curseNumber].GetComponent<Curse>();
            if (player.gameObject.tag.Equals("Player"))
            {
                if (!player.gameObject.GetComponent<BombermanStats>().Cursed)
                {
                    player.gameObject.AddComponent(curse.GetType());
                    player.GetComponent<BombermanStats>().Cursed = true;
                }
                else if(player.gameObject.GetComponent(curse.GetType()) != null)
                {
                    player.gameObject.GetComponent<Curse>().curseTimer = curse.CurseResetTimer();
                }
            }
            Destroy(gameObject);
            
            //Reset timer for curse?
            // Add new curse?
        }
    }
}