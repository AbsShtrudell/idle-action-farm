using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFlowWorld : ItemsFlow
{
    protected override IEnumerator SpawnItems(Transform start, Transform destination, FlowItem itemData, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var flowItem = flowItemPool.Get();
            flowItem.data = itemData;

            flowItem.onReachedDestination += ItemReachedDestination;

            flowItem.Move(start.position, destination.position, duration);

            OnItemStartMove(flowItem.data);

            yield return new WaitForSeconds(delay);
        }
        OnFlowComplete(amount);
    }
}
