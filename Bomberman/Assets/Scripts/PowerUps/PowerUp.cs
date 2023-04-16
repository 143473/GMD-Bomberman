using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float availabilityInSeconds = 10;
    private static GameObject[] _powerUps;
    // Start is called before the first frame update
    void Start()
    {
        _powerUps = Resources.LoadAll<GameObject>("PowerUps");
        SpawnRandomPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, availabilityInSeconds);
    }

    void SpawnRandomPowerUp()
    {
        var powerUpNumber = Random.Range(0, _powerUps.Length);
        GameObject powerUp = Instantiate(_powerUps[powerUpNumber], gameObject.transform);
    }
}
