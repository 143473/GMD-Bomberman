using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    [SerializeField] private float flameDestroyDelay = 1f;
    private FlamePool flamePoolSpawner;
    
    private void Awake()
    {
        var flamePoolGO = GameObject.Find("FlameGOPool");
        flamePoolSpawner = flamePoolGO.GetComponent<FlamePool>();
    }

    private void OnEnable()
    {
        // var position = gameObject.transform.position;
        // Gridx.onGridValueChanged?.Invoke(position.x, position.z, 4);
        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent(out IDamage damage))
            damage.OnDamage();
    }

    IEnumerator SelfDestroy()
    {
        // var position = gameObject.transform.position;
        // Gridx.onGridValueChanged?.Invoke(position.x, position.z, 0);
        yield return new WaitForSeconds(flameDestroyDelay);
        flamePoolSpawner.ReturnFlameToPool(gameObject);
    }
}
