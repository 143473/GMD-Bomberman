using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombScript : MonoBehaviour
{
    [SerializeField] private SphereCollider sc;
    private Vector3 halfExtent = new Vector3();
    
    private FlamePool flamePoolSpawner;
    private BombStats bombStats;
    private Collider[] colliders;
    private bool flameCoroutineStarted = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip flameClip;

    private void Awake()
    {
      colliders = new Collider[10];
      var flamePoolGO = GameObject.Find("FlameGOPool");
      flamePoolSpawner = flamePoolGO.GetComponent<FlamePool>();
      audioSource = gameObject.GetComponent<AudioSource>();
      gameObject.SetActive(false);
    }

    private void OnEnable()
    {
      var position = gameObject.transform.position;
      Gridx.onGridValueChanged?.Invoke(position.x, position.z, 3);
      
      BombermanCharacterController.onManuallyExplodeBomb += Boom;
      bombStats = GetComponent<BombStats>();
      sc.isTrigger = true;
    }

    private void OnDisable()
    {
      var position = gameObject.transform.position;
      if (position.x > 0 && position.z > 0)
      {
        Gridx.onGridValueChanged?.Invoke(position.x, position.z, 0);
      }
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
      if (!flameCoroutineStarted && gameObject.activeSelf)
      {
        AudioSource.PlayClipAtPoint(flameClip, Vector3.zero);
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

      directions.Clear();
      gameObject.SetActive(false); ;
      flameCoroutineStarted = false;    
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
