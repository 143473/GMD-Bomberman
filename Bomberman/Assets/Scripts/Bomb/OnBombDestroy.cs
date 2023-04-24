using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class OnBombDestroy : MonoBehaviour, IDamage
{
    [SerializeField] private float explosionDelay = 0.05f;
    public UnityEvent onBombDestroy;

    public void OnDamage()
    {
        StartCoroutine(ExplosionDelay());
    }

    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(explosionDelay);
        onBombDestroy.Invoke();
    }
}