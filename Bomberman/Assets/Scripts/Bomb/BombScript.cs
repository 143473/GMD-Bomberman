using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Interfaces;

public class BombScript : MonoBehaviour
{
    public float delay = 3f;
    private bool hasExploded;
    public BoxCollider bc;
    public int blast = 1; 
    private Vector3 halfExtent = new Vector3(0.25f, 0, 0.25f);
    public GameObject flamePrefab;

    private void Awake()
    {
      BombermanCharacterController.onManuallyExplodeBomb += Boom;
    }

    private void OnEnable()
    {
      delay = GetComponent<BombStats>().BombDelay;
      bc.enabled = false;
    }
    void Update()
    {
        // gameObject.GetComponent<BombStats>().Delay -= Time.deltaTime;
        // if (gameObject.GetComponent<BombStats>().Delay <= 0f) {
        //  Explode();
        //  onBombExplosion?.Invoke();
        // }
        if (!gameObject.GetComponent<BombStats>().RemoteExplosion)
        {
          delay -= Time.deltaTime;
          if (delay <= 0f)
          {
              Explode();
          }
        }
        if (!bc.enabled) {
           Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
           if (!colliders.Any(c => c.GetComponent<BombermanCharacterController>() != null)) {
             bc.enabled = true;
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
      Collider[] colliders = Physics.OverlapBox(cellPosition, halfExtent, Quaternion.identity);
      foreach (Collider collider in colliders)
      {
        Debug.Log($"Collider {collider.transform.position}");
          
        // OnDestroy destructible = collider.GetComponent<OnDestroy>();
        // if (destructible != null) {
        //   destructible.DestroyObject();
        // }
        var destructible = collider.gameObject.GetComponent<IDamage>();
        if(destructible != null && collider.gameObject != gameObject)
          destructible.OnDamage();
      }
      return colliders.Length > 0;
    }
}
