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
        //GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
      //  Rigidbody rb = bomb.GetComponent<Rigidbody>();
      //  rb.AddForce(transform.forward * throwForce);
/*
      Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(transform.position.x), 
              bombPrefab.transform.position.y, Mathf.RoundToInt(transform.position.z)),
          bombPrefab.transform.rotation);
  */    
		var vect = new Vector3(ToGrid(transform.position.x), 0.5f, ToGrid(transform.position.z));
		Instantiate(bombPrefab, vect, transform.rotation);
    }

	float ToGrid(float pos)
	{
		return Mathf.Round(pos / 1.25f) * 1.25f;
	}
}
