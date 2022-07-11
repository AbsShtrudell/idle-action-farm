using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Zenject.Inject] private Animator animator;
    [Zenject.Inject] private MovementController movementController;
    [Zenject.Inject] private Sickle sickle;

    [SerializeField] private int _bagCapacity = 20;
    private int _currentResources = 0;
    private int _currentCoins = 0;

    [SerializeField] private GameObject sickleObj;

    public event Action<bool> onWorkStateChanged;
    public event Action<int> onResourcesChanged;
    public event Action<int> onCoinsChanged;

    public int ResourcesCount
    { get { return _currentResources; } private set { _currentResources = value; onResourcesChanged?.Invoke(_currentResources); } }
    public int BagCapacity
    { get { return _bagCapacity; } }
    public int CoinsCount
    { get { return _currentCoins; } private set { _currentCoins = value; onCoinsChanged?.Invoke(_currentCoins); } }

    void Awake()
    {
        SetWorkingState(false);
    }

    void Update()
    {
        animator.SetFloat("Speed", movementController.CurrentSpeedPercent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FieldController>() != null)
        {
            SetWorkingState(true);
            return;
        }
        Loot loot = other.GetComponent<Loot>();

        if (loot != null && _currentResources < _bagCapacity)
        {
            ResourcesCount++;
            loot.Take();
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FieldController>() != null)
        {
            SetWorkingState(false);
        }
    }

    private void SetWorkingState(bool working)
    {
        animator.SetBool("Working", working);

        if(sickleObj != null)
            sickleObj.SetActive(working);

        if (working)
            sickle.UnUsed();

        onWorkStateChanged(working);
    }

    public void DecreaseResources(int amount)
    {
        if (amount <= 0) return;

        ResourcesCount -= Mathf.Clamp(amount, 0, ResourcesCount);
    }

    public void AddCoins(int amount)
    {
        if(amount <= 0) return;
        
        CoinsCount += amount;
    }
}
