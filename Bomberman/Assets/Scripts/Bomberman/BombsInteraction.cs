using System;
using UnityEngine;
using Utils;

namespace Bomberman
{
    public class BombsInteraction : MonoBehaviour
    {
        [SerializeField] protected float speed;
        

        private void Awake()
        {
            
        }

        private void OnCollisionEnter(Collision bomb)
        {
            Debug.Log("colision");
            if (bomb.gameObject.tag.Equals("Bomb"))
            {
                Debug.Log("Bomb?");
                if (gameObject.GetComponent<FinalBombermanStatsV2>().GetBooleanStat(Stats.Kick))
                {
                    Debug.Log("kick");
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    var bombRb = bomb.rigidbody;
                    bombRb.AddRelativeForce(transform.forward);
                    //bombRb.velocity = (Vector3.Normalize(bombRb.velocity)) * speed;
                }
            }

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}