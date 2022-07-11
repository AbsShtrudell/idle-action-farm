using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public abstract class FlowItemVisual : MonoBehaviour
{
    protected FlowItem _data;
    public FlowItem data
    { get { return _data; } set { SetData(value); } }

    public event Action<FlowItemVisual> onReachedDestination;

    protected abstract void SetData(FlowItem data);

    public void Move(Vector3 start, Vector3 destination, float duration)
    {
        transform.position = start;

        transform.DOMove(destination, duration).OnComplete(() => { onReachedDestination?.Invoke(this); });
    }

}