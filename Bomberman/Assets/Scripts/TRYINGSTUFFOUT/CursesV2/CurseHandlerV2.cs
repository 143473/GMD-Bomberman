using System.Collections.Generic;
using PickUps.Curses;
using UnityEngine;

namespace TRYINGSTUFFOUT.CursesV2
{
    public class CurseHandlerV2 : MonoBehaviour
    {
        [SerializeField] protected float availabilityInSeconds = 10;
        [SerializeField] protected List<CurseModifier> curseModifiers;

        private void Awake()
        {
            Destroy(gameObject, availabilityInSeconds);
        }

        private void OnTriggerEnter(Collider player)
        {

            if (player.gameObject.tag.Equals("Player"))
            {
                var curse = player.gameObject.AddComponent<CurseV2>();
                curse.appliedCurse = GetRandomCurse();
                Destroy(gameObject);
            }
        }
        
        private CurseModifier GetRandomCurse()
        {
            var curseModifier = Random.Range(0, curseModifiers.Count);
            return curseModifiers[curseModifier];
        }
    }
}