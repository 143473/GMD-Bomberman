using Interfaces;
using UnityEngine;

public class OnPickUpDestroy : MonoBehaviour, IDamage
{
    void Start()
    {
        
    }
    public void OnDamage()
    {
        Destroy(gameObject);
    }
}