using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour
{   
	public GameObject bombPrefab;
	private bool _bombExploded = false;

	private void Awake()
	{
		BombScript.onBombExplosion += OnBombGoesBoom;
	}

	void Update()
    {
	    if (Input.GetKeyDown("space"))
	    {
		    Bomb();
	    }
    }
	void Bomb()
    {    
		var vect = new Vector3(ToGrid(transform.position.x), 0.3f, ToGrid(transform.position.z));
		//Instantiate(bombPrefab, vect, transform.rotation);

		var bomb = gameObject.GetComponent<BombsInventory>().Bombs.Find(a => !a.activeSelf);
		bomb.GetComponent<Transform>().position = vect;
		bomb.GetComponent<Transform>().rotation = transform.rotation;
		bomb.GetComponent<BombStats>().SetStats(gameObject.GetComponent<BombermanStats>().BombStats);
		bomb.SetActive(true);
    }
	float ToGrid(float pos)
	{
		return Mathf.Round(pos);
	}

	void OnBombGoesBoom()
	{
		_bombExploded = true;
	}
}
