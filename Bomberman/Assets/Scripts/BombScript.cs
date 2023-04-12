using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{

    public float delay = 80000f;
    private bool hasExploted;
    public float radius = 5f;
    public float force = 500;
//    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0f && !hasExploted) {
        Explode();
      }
    }

    void Explode(){
       //show effect
     //  Instantiate(explosionEffect, transform.position, transform.rotation);
     //destroy
      Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
      foreach(Collider nearbyWall in colliders){
      Rigidbody rb = nearbyWall.GetComponent<Rigidbody>();
      if(rb != null){
      rb.AddExplosionForce(force, transform.position, radius);
      }
      Destructible wall = nearbyWall.GetComponent<Destructible>();
       if (wall != null){
      wall.DestroyWall();
      }
      }
      hasExploted = true;
      Destroy(gameObject);
    }
}
