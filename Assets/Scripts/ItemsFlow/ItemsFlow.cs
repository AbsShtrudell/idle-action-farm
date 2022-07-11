using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using System;

public abstract class ItemsFlow : MonoBehaviour
{
    [SerializeField, Min(0)] protected float delay;
    [SerializeField, Min(0)] protected float duration;
    [Header("Pool")]
    [SerializeField, Min(0)] protected int capacity;
    [SerializeField, Min(0)] protected int maxCapacity;

    [SerializeField] protected FlowItemVisual flowItemVisual;

    protected ObjectPool<FlowItemVisual> flowItemPool;

    public event Action<FlowItem> onItemReachedDestination;
    public event Action<FlowItem> onItemStartMove;
    public event Action<int> onFlowComplete;

    private void Start()
    {
        InitPool();
    }

    public void StartFlow(Transform start, Transform destination, FlowItem itemData, int amount)
    {
        StartCoroutine(SpawnItems(start, destination, itemData, amount));
    }

    protected abstract IEnumerator SpawnItems(Transform start, Transform destination, FlowItem itemData, int amount);

    protected void ItemReachedDestination(FlowItemVisual flowItem)
    {
        flowItem.onReachedDestination -= ItemReachedDestination;

        OnItemReachedDestination(flowItem.data);

        flowItemPool.Release(flowItem);
    }

    protected void OnItemReachedDestination(FlowItem flowItem)
    { 
        onItemReachedDestination?.Invoke(flowItem);
    }

    protected void OnItemStartMove(FlowItem flowItem)
    {
        onItemStartMove?.Invoke(flowItem);
    }

    protected void OnFlowComplete(int amount)
    {
        onFlowComplete?.Invoke(amount);
    }

    private void InitPool()
    {
        flowItemPool = new ObjectPool<FlowItemVisual>(() =>
        {
            return Instantiate(flowItemVisual, transform);
        }, flowItemVisual =>
        {
            flowItemVisual.gameObject.SetActive(true);
        }, flowItemVisual =>
        {
            flowItemVisual.gameObject.SetActive(false);
        }, flowItemVisual =>
        {
            Destroy(flowItemVisual);
        }, true, capacity, maxCapacity);
    }
}