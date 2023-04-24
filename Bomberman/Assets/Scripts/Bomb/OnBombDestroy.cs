using System.Collections;
using Interfaces;
using UnityEngine;

public class OnBombDestroy : MonoBehaviour, IDamage
{
    void Start()
    {
        
    }
    public void OnDamage()
    {
        StartCoroutine(ExplosionDelay());
    }

    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<BombScript>().Explode();
    }
}