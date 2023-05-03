using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Interfaces;
using UnityEditor;

public class BombScript : MonoBehaviour
{
    public SphereCollider sc;
    private Vector3 halfExtent = new Vector3();
    
    private FlamePool flamePoolSpawner;
    private BombStats bombStats;
    private Collider[] colliders;
    private bool flameCoroutineStarted = false;


    private void Awake()
    {
      colliders = new Collider[10];
      var flamePoolGO = GameObject.Find("FlameGOPool");
      flamePoolSpawner = flamePoolGO.GetComponent<FlamePool>();
      
      gameObject.SetActive(false);
    }

    private void OnEnable()
    {
      BombermanCharacterController.onManuallyExplodeBomb += Boom;
      bombStats = GetComponent<BombStats>();
      sc.isTrigger = true;
    }

    private void OnDisable()
    {
      BombermanCharacterController.onManuallyExplodeBomb -= Boom;
    }

    void Update()
    {
      if (!bombStats.RemoteExplosion)
      {
        bombStats.BombDelay -= Time.deltaTime;
        if (bombStats.BombDelay <= 0f)
        {
          Explode();
        }
      }
      
      if (sc.isTrigger) {
        colliders = Physics.OverlapSphere(transform.position, 0.5f);
        if (!colliders.Any(c => c.gameObject.CompareTag("Player"))) {
          sc.isTrigger = false;
        }
        CollidersDisposal(colliders);
      }
    }

    private void Boom(string bomberman)
    {
      if(name.Contains(bomberman)) 
        Explode();
    }

    public void Explode()
    {
      //CheckCell(transform.position);

      // CheckDirection(Vector3.forward);
      // CheckDirection(Vector3.back);
      // CheckDirection(Vector3.left);
      // CheckDirection(Vector3.right);

      if (!flameCoroutineStarted && gameObject.activeSelf)
      {
        StartCoroutine(SpawnFlames());
      }
    }

    IEnumerator SpawnFlames()
    {
      flameCoroutineStarted = true;
      CheckCell(transform.position);
      
      List<Vector3> directions = new List<Vector3>() { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
      List<Vector3> directionsBlocked = new List<Vector3>();

      for (int i = 1; i < bombStats.Flame + 1; i++)
      {
        foreach (var direction in directions)
        {
          Vector3 offset = direction * (i * 1f);
          Vector3 cellPosition = transform.position + offset;
          
          if (CheckCell(cellPosition))
          {
            directionsBlocked.Add(direction);
          }
        }
        directions = directions.Where(x => !directionsBlocked.Contains(x)).ToList();
        directionsBlocked.Clear();

        yield return null;
      }

      flameCoroutineStarted = false;
      directions.Clear();
      gameObject.SetActive(false);

    }


    // Second Approach - per direction
    void CheckDirection(Vector3 direction)
    {
      for (int i = 1; i < bombStats.Flame + 1; i++)
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
      colliders = Physics.OverlapBox(cellPosition, halfExtent, Quaternion.identity, 3);
      var length = colliders.Length;
      if (colliders.Any(c => c.gameObject.CompareTag("NonDestructibleWall")))
        return true;

      var flame = flamePoolSpawner.GetFlameFromPool();
      flame.transform.position = cellPosition;
      
      CollidersDisposal(colliders);
      return length > 0;
    }

    void CollidersDisposal(Collider[] colliders)
    {
      Array.Clear(colliders, 0, colliders.Length);
    }
}
