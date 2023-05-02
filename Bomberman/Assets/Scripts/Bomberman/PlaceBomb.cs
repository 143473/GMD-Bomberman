using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PlaceBomb : MonoBehaviour
{
	private BombsInventory bombermanInventory;
	private FinalBombermanStats bombermanStats;
	private Collider[] colliders;
	private void Awake()
	{
		bombermanInventory = gameObject.GetComponent<BombsInventory>();
		bombermanStats = gameObject.GetComponent<FinalBombermanStats>();
	}

	private void Update()
	{
		if (bombermanStats.GetBooleanStat(Stats.Nasty))
		{
			Bomb();
		}
	}

	public void Bomb()
    {
	    var vect = new Vector3(ToGrid(transform.position.x), 0.3f, ToGrid(transform.position.z));
	    var bomb = bombermanInventory.GetBomb();

	    if (bomb)
		{
			colliders = Physics.OverlapSphere(transform.position, 0.5f);
			if (!colliders.Any(c => c.gameObject.tag.Equals("Bomb"))) {
				bomb.transform.position = vect;
				bomb.transform.rotation = transform.rotation;
				bomb.GetComponent<BombStats>().SetStats(bombermanStats.GetNumericStat(Stats.Flame), 
					bombermanStats.GetBooleanStat(Stats.RemoteExplosion), 
					bombermanStats.GetNumericStat(Stats.BombDelay));
				bomb.SetActive(true);
			}
			Array.Clear(colliders, 0, colliders.Length);
		}
    }

	float ToGrid(float pos)
	{
		return Mathf.Round(pos);
	}
}
