using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombScript : MonoBehaviour
{

    public float delay = 3f;
    private bool hasExploded;
    public BoxCollider bc;
    public int blast = 3;

    Vector3 halfExtent = new Vector3(0.25f, 0, 0.25f);

    void Start()
    {
      
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0f && !hasExploded) {
         Explode();
        }
        if (!bc.enabled) {
           Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
           if (!colliders.Any(c => c.GetComponent<BombermanCharacterController>() != null)) {
             bc.enabled = true;
           }
        }
    }
    public void Explode()
    {
      CheckCell(transform.position);
      
      CheckDirection(Vector3.forward);
      CheckDirection(Vector3.back);
      CheckDirection(Vector3.left);
      CheckDirection(Vector3.right);
      
      Destroy(gameObject);      
      hasExploded = true;
    }

    void CheckDirection(Vector3 direction)
    {
      for (int i = 1; i < blast + 1; i++)
      {
        //offset from the bomb position to look up the other cells 
        Vector3 offset = direction * (i * 1f);
        //we need to consider the bomb position as well
        Vector3 cellPosition = transform.position + offset;

        if (CheckCell(cellPosition))
        {
          break;
        }
      }
    }

    bool CheckCell(Vector3 cellPosition)
    {
      //check the colliders in the cell 
      Collider[] colliders = Physics.OverlapBox(cellPosition, halfExtent, Quaternion.identity);
      Debug.Log($"Pos {transform.position} Cell {cellPosition} Colliders {colliders.Length}");

      foreach (Collider collider in colliders)
      {
        Debug.Log($"Collider {collider.transform.position}");
          
        Destructible destructible = collider.GetComponent<Destructible>();
        if (destructible != null) {
          destructible.DestroyObject();
        }
      }
      return colliders.Length > 0;
    }
}
