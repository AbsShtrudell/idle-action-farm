using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FIV_World : FlowItemVisual
{
    MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = transform.GetComponent<MeshFilter>();
    }

    protected override void SetData(FlowItem data)
    {
        meshFilter.mesh = data.mesh;

        this._data = data;
    }
}
