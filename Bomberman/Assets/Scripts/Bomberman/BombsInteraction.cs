using UnityEngine;

namespace Bomberman
{
    public class BombsInteraction : MonoBehaviour
    {
        [SerializeField] protected float speed;
        private void OnCollisionEnter(Collision bomb)
        {
            Debug.Log("colision");
            if (bomb.gameObject.tag.Equals("Bomb"))
            {
                Debug.Log("Bomb?");
                if (gameObject.GetComponent<BombermanStats>().Kick)
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