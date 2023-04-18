using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour
{   
	public GameObject bombPrefab;
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
		Instantiate(bombPrefab, vect, transform.rotation);
    }
	float ToGrid(float pos)
	{
		return Mathf.Round(pos);
	}
}
