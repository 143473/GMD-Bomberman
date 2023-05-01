using System;
using System.Linq;
using PickUps.Curses;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Bomberman
{
    public class OtherBombermanInteraction : MonoBehaviour
    {
        private BoxCollider bc;
        private Collider[] colliders;

        private void Awake()
        {
            //bc = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            // if (!bc.isTrigger) {
            //     colliders = Physics.OverlapSphere(transform.position, 0.5f);
            //     if (colliders.Any(c => c.gameObject.CompareTag("Player"))) {
            //         bc.isTrigger = true;
            //     }
            //     Array.Clear(colliders, 0, colliders.Length);
            // }
            //
            // if (bc.isTrigger) {
            //     colliders = Physics.OverlapSphere(transform.position, 0.5f);
            //     if (!colliders.Any(c => c.gameObject.CompareTag("Player"))) {
            //         bc.isTrigger = false;
            //     }
            //     Array.Clear(colliders, 0, colliders.Length);
            // }
        }

        private void OnTriggerEnter(Collider bomberman)
        {

        }

        // private void OnCollisionEnter(Collision other)
        // {
        //     Debug.Log(other.gameObject.tag);
        //     var bomberman = other.gameObject;
        //     if (GetComponent<FinalBombermanStatsV2>().GetBooleanStat(Stats.Cursed))
        //     {
        //         CurseV2[] curseComponents = GetComponents<CurseV2>();
        //         if (bomberman.gameObject.tag.Equals("Player"))
        //         {
        //             foreach (var curse in curseComponents)
        //             {
        //                 bomberman.gameObject.AddComponent(curse.GetType());
        //             }
        //         }
        //     }
        // }
    }
}