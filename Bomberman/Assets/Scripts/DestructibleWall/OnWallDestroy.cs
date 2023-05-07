using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class OnWallDestroy : MonoBehaviour, IDamage
{
    [SerializeField] private float spawnDelay;
    
    public UnityEvent onWallDestroy;
    public void OnDamage()
    {
        StartCoroutine(SpawnDelay());
    }
    
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        onWallDestroy.Invoke();
        var position = gameObject.transform.position;
        Gridx.onGridValueChanged?.Invoke(position.x, position.z, 0);
        Destroy(gameObject);
    }
}