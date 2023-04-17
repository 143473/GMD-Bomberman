using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpContainerSpawner : MonoBehaviour
{
    [SerializeField] private float chanceToSpawn = 50;

    [SerializeField] private GameObject powerUpContainer;
    private bool _isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnPowerUpContainer()
    {
        var randomNumber = Random.Range(0, 100);
        if (randomNumber < chanceToSpawn)
        {
            var container = Instantiate(powerUpContainer);
            container.transform.position = transform.position;
        }
    }
}