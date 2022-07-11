using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [Zenject.Inject] PlayerController playerController;

    [SerializeField] private GameObject stackRef;

    private int bagCapacity;

    private void Awake()
    {
        if (stackRef == null) stackRef = gameObject;

        bagCapacity = playerController.BagCapacity;

        HandleStackSize(0);
    }

    private void OnEnable()
    {
        playerController.onResourcesChanged += HandleStackSize;
    }

    private void OnDisable()
    {
        playerController.onResourcesChanged -= HandleStackSize;
    }

    private void HandleStackSize(int amount)
    {
        stackRef.SetActive(amount > 0);

        stackRef.transform.localScale = Vector3.one * amount / bagCapacity;
    }
}
