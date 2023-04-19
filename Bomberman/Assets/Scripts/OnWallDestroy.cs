using Interfaces;
using UnityEngine;

public class OnWallDestroy : MonoBehaviour, IDamage
{
    void Start()
    {
        
    }
    public void OnDamage()
    {
        //gameObject.GetComponent<PowerUpContainerSpawner>().SpawnPowerUpContainer();
        Destroy(gameObject);
    }
}