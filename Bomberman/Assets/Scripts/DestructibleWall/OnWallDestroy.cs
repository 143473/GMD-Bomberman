using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class OnWallDestroy : MonoBehaviour, IDamage
{
    [SerializeField] private float spawnDelay = 0.01f;
    public UnityEvent onWallDestroy;
    public void OnDamage()
    {
        StartCoroutine(SpawnDelay());
    }
    
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        onWallDestroy.Invoke();
        Destroy(gameObject);
    }
}