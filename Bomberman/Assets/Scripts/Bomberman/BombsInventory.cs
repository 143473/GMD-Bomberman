using System.Collections.Generic;
using PickUps.Curses;
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
        newBomb.name = $"Bomb {Bombs.Count + 1}";
        Bombs.Add(newBomb);
    }
}