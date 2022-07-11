using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sickle : MonoBehaviour
{
    private bool inUse = false;

    public void Used()
    { 
        inUse = true; 
    }

    public void UnUsed()
    {
        inUse = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Foliage foliage = other.GetComponent<Foliage>();

        if (foliage == null || !inUse) return;

        foliage.Cut();
    }
}
