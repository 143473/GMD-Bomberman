using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombScript : MonoBehaviour
{

    public float delay = 3f;
    private bool hasExploded;
    public float radius = 5f;
    public float force = 500;
    public BoxCollider bc;

    void Start()
    {
      
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0f && !hasExploded) {
          Explode();
        }
        if (!bc.enabled) {
           Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
           if (!colliders.Any(c => c.GetComponent<BombermanCharacterController>() != null)) {
             bc.enabled = true;
           }
        }
    }

    void Explode(){
      Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
      foreach(Collider nearbyWall in colliders) {
        Rigidbody rb = nearbyWall.GetComponent<Rigidbody>();
        if(rb != null) {
          rb.AddExplosionForce(force, transform.position, radius);
        }
        Destructible wall = nearbyWall.GetComponent<Destructible>();
        if (wall != null) {
          wall.DestroyObject();
        }
      }
      hasExploded = true;
      Destroy(gameObject);
    }
}
