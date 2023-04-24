using System;
using System.Collections.Generic;
using PickUps.Curses;
using UnityEngine;

public class BombsInventory : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    public List<GameObject> Bombs { get; set; }
    private GameObject bombsInventory;

    private void Awake()
    {
        Bombs = new List<GameObject>();
        BombermanStats.onBombIncrease += AddBomb;
    }

    private void Start()
    {
        // pretty editor runtime 
        bombsInventory = new GameObject();
        bombsInventory.name = $"{name} Inventory";
        
        AddBomb(name);
    }

    private void AddBomb(string bomberman)
    {
        if (bomberman.Equals(name))
        {
            var newBomb = Instantiate(bombPrefab, new Vector3(), new Quaternion(), bombsInventory.transform);
            newBomb.name = $"{name} : Bomb {Bombs.Count + 1}";
            Bombs.Add(newBomb);
        }
    }
}