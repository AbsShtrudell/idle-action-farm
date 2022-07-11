using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class SellZone : MonoBehaviour
{
    public event Action onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            onPlayerEnter?.Invoke();
        }
    }
}
