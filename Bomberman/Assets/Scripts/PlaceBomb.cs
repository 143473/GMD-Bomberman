using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour
{   
	public GameObject bombPrefab;
	private int _placedBombs = 0;
	void Update()
    {
	    if(_placedBombs < gameObject.GetComponent<BombermanStats>().Bombs)
		    if (Input.GetKeyDown("space"))
		    {
			    Bomb();
			    _placedBombs++;
		    }
    }
	void Bomb()
    {    
		var vect = new Vector3(ToGrid(transform.position.x), 0.3f, ToGrid(transform.position.z));
		Instantiate(bombPrefab, vect, transform.rotation);
    }
	float ToGrid(float pos)
	{
		return Mathf.Round(pos);
	}
}
