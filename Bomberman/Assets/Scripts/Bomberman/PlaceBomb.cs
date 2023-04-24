using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBomb : MonoBehaviour
{   
	public GameObject bombPrefab;

	private void Update()
	{
		if (GetComponent<BombermanStats>().Nasty)
		{
			Bomb();
		}
	}

	public void Bomb()
    {
	    var vect = new Vector3(ToGrid(transform.position.x), 0.3f, ToGrid(transform.position.z));
		//Instantiate(bombPrefab, vect, transform.rotation);

		var availableBombs = gameObject.GetComponent<BombsInventory>().Bombs.Exists(a => !a.activeSelf);
		if (availableBombs)
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
			if (!colliders.Any(c => c.gameObject.tag.Equals("Bomb"))) {
				InstantiateBomb(vect);
			}
		}

    }

	private void InstantiateBomb(Vector3 vector3)
	{
		var bombermanInventory = gameObject.GetComponent<BombsInventory>();
		var bombermanStats = gameObject.GetComponent<BombermanStats>();
		var bomb = bombermanInventory.Bombs.Find(a => !a.activeSelf);
		bomb.GetComponent<Transform>().position = vector3;
		bomb.GetComponent<Transform>().rotation = transform.rotation;
		bomb.GetComponent<BombStats>().SetStats(bombermanStats.Flame, bombermanStats.RemoteExplosion, bombermanStats.BombDelay);
		bomb.SetActive(true);
	}

	float ToGrid(float pos)
	{
		return Mathf.Round(pos);
	}
}
