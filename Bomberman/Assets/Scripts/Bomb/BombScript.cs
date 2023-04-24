using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Interfaces;

public class BombScript : MonoBehaviour
{
    public float delay;
    public BoxCollider bc;
    //private Vector3 halfExtent = new Vector3(0.25f, 0, 0.25f);
    private Vector3 halfExtent = new Vector3();
    
    [SerializeField] private GameObject flamePrefab;


    private void Awake()
    {
      BombermanCharacterController.onManuallyExplodeBomb += Boom;
      gameObject.SetActive(false);
    }

    private void OnEnable()
    {
      delay = GetComponent<BombStats>().BombDelay;
      bc.isTrigger = true;
    }
    void Update()
    {
      if (!gameObject.GetComponent<BombStats>().RemoteExplosion)
      {
        delay -= Time.deltaTime;
        if (delay <= 0f)
        {
          Explode();
        }
      }
        
      // Garbage collector for this, or use maybe rider's suggestion: OverlapSphereNonAlloc, OverlapBoxNonAlloc
      if (bc.isTrigger) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        if (!colliders.Any(c => c.GetComponent<BombermanCharacterController>() != null)) {
          bc.isTrigger = false;
        }
      }
    }

    public void Boom(string bomberman)
    {
      if(name.Contains(bomberman)) 
        Explode();
    }
    public void Explode()
    {
      CheckCell(transform.position);
      
      CheckDirection(Vector3.forward);
      CheckDirection(Vector3.back);
      CheckDirection(Vector3.left);
      CheckDirection(Vector3.right);

      gameObject.SetActive(false);
      //Destroy(gameObject);      
      //hasExploded = true;
    }

    void CheckDirection(Vector3 direction)
    {
      for (int i = 1; i < gameObject.GetComponent<BombStats>().Flame + 1; i++)
      {
        Vector3 offset = direction * (i * 1f);
        Vector3 cellPosition = transform.position + offset;
        
        if (CheckCell(cellPosition))
        {
          break;
        }
        
      }
    }

    bool CheckCell(Vector3 cellPosition)
    {
      Collider[] colliders = Physics.OverlapBox(cellPosition, halfExtent, Quaternion.identity, 3);
      if (colliders.Any(c => c.gameObject.CompareTag("NonDestructibleWall")))
        return true;
      
      Instantiate(flamePrefab, cellPosition, Quaternion.identity);
      return colliders.Length > 0;
    }
}
