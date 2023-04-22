using System;
using UnityEngine;

public class BombExplosionHandler : MonoBehaviour
{
    public delegate void OnExplosionTrigger(string name);
    public static OnExplosionTrigger onExplosionTrigger;

    private float _delay;

    private void OnEnable()
    {
        _delay = gameObject.GetComponent<BombStats>().Delay;
    }

    private void Update()
    {
        if (gameObject.GetComponent<BombStats>().RemoteExplosion)
        {
            if (Input.GetKeyDown("x"))
                onExplosionTrigger?.Invoke(name);
        }
        else
        {
            _delay -= Time.deltaTime;
            if (_delay <= 0f)
            {
                onExplosionTrigger?.Invoke(name);
            }
        }
    }
}