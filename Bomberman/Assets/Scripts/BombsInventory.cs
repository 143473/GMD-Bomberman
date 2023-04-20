using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombsInventory : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    public List<GameObject> Bombs { get; set; }
    private void Awake()
    {
        Bombs = new List<GameObject>();
        AddBomb();
        BombermanStats.onBombIncrease += AddBomb;
    }
    private void AddBomb()
    {
        var newBomb = Instantiate(bombPrefab, new Vector3(), new Quaternion());
        Bombs.Add(newBomb);
    }
}