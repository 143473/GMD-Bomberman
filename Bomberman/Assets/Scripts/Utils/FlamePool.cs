using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePool : MonoBehaviour
{
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private int maxPoolSize = 1000;

    private Stack<GameObject> inactiveFlames = new Stack<GameObject>();
    private GameObject flamePapa;

    private void Awake()
    {
        flamePapa = new GameObject();
        flamePapa.name = "FlamePool";
        
        if (flamePrefab != null)
        {
            for (int i = 0; i < maxPoolSize; ++i)
            {
                var newFlame = Instantiate(flamePrefab, flamePapa.transform);
                newFlame.SetActive(false);
                inactiveFlames.Push(newFlame);
            }
        }
    }

    public GameObject GetFlameFromPool()
    {
        while (inactiveFlames.Count > 0)
        {
            var flame = inactiveFlames.Pop();

            if (flame)
            {
                flame.SetActive(true);
                return flame;
            }
            else
            {
                Debug.LogWarning("Found a null object in the pool. Has some code outside the pool destroyed it?");
            }
        }

        Debug.LogError("All pooled objects are already in use or have been destroyed");
        return null;
    }

    public void ReturnFlameToPool(GameObject objectToDeactivate)
    {
        objectToDeactivate.SetActive(false);
        inactiveFlames.Push(objectToDeactivate);
    }
}