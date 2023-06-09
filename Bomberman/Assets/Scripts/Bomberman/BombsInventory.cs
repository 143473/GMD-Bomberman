using System;
using System.Collections.Generic;
using PickUps.Curses;
using UnityEngine;
using Utils;

public class BombsInventory : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    public List<GameObject> Bombs { get; set; }
    private GameObject bombsInventory;
    private FinalBombermanStats finalBombermanStats;

    private void Awake()
    {
        finalBombermanStats = GetComponent<FinalBombermanStats>();
        Bombs = new List<GameObject>();
    }

    private void Start()
    {
        // pretty editor runtime 
        bombsInventory = new GameObject();
        bombsInventory.name = $"{name} Inventory";
        
        AddBomb(name);
    }

    private void FixedUpdate()
    {
        if (finalBombermanStats.GetNumericStat(Stats.Bombs) > Bombs.Count)
        {
            AddBomb(name);
        }
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

    public GameObject GetBomb()
    {
        var bombsCapacity = finalBombermanStats.GetNumericStat(Stats.Bombs);
        var availableBombs = Bombs.Exists(a => !a.activeSelf);
        
        if(availableBombs && bombsCapacity != 0)
            return Bombs.Find(a => !a.activeSelf);
        return null;
    }
    
}