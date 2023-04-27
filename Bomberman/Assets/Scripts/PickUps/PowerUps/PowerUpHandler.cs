using System;
using System.Linq;
using PowerUps.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] 
    protected float availabilityInSeconds = 10;

    [SerializeField]
    protected GameObject[] powerUps;

    private void Awake()
    {
        Destroy(gameObject, availabilityInSeconds);
    }

    void Start()
    {
        var randomChance = Random.Range(0, 100);
        powerUps = powerUps.Where(a => 
        {
            var addedToInitialList = false;
            var powerUpScript = a.GetComponent<IPowerUp>();
            
            addedToInitialList = randomChance <= powerUpScript.ChanceToSpawn();
            
            return addedToInitialList;
        }).ToArray();
        SpawnRandomPowerUp();
    }
    void SpawnRandomPowerUp()
    {
        var powerUpNumber = Random.Range(0, powerUps.Length);
        Instantiate(powerUps[powerUpNumber], gameObject.transform);
    }
}
