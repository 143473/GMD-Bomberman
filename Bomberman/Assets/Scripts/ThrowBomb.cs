using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public float throwForce = 0f;
    public GameObject bombPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Bomb();
        }
    }

    void Bomb()
    {    
		var vect = new Vector3(ToGrid(transform.position.x), 0.5f, ToGrid(transform.position.z));
		Instantiate(bombPrefab, vect, transform.rotation);
    }

	float ToGrid(float pos)
	{
		return Mathf.Round(pos);
	}
}
