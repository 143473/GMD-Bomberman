using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBomb : MonoBehaviour
{   
	public GameObject bombPrefab;
	
	public void Bomb()
    {
	    var vect = new Vector3(ToGrid(transform.position.x), 0.3f, ToGrid(transform.position.z));
		//Instantiate(bombPrefab, vect, transform.rotation);

		var availableBombs = gameObject.GetComponent<BombsInventory>().Bombs.Exists(a => !a.activeSelf);
		if (availableBombs)
			InstantiateBomb(vect);
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
