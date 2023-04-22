using Interfaces;
using UnityEngine;

public class OnWallDestroy : MonoBehaviour, IDamage
{
    void Start()
    {
        
    }
    public void OnDamage()
    {
        gameObject.GetComponent<PickupsSpawner>().SpawnPickUp();
        Destroy(gameObject);
    }
}