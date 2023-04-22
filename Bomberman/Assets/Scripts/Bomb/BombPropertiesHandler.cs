using System;
using UnityEngine;

public class BombPropertiesHandler : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    
    private Transform _transform;
    private bool _kickable;

    private void OnEnable()
    {
        _kickable = GetComponent<BombStats>().Kickable;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.gameObject.GetComponent<BombermanStats>().Kick)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 100);
            }
        }
    }
}