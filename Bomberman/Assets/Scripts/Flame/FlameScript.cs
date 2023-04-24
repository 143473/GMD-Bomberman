using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    [SerializeField] private float flameDestroyDelay = 1f;

    private void Awake()
    {
        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent(out IDamage damage))
            damage.OnDamage();
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(flameDestroyDelay);
        Destroy(gameObject);
    }
}
