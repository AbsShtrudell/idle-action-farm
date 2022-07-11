using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FIV_UI : FlowItemVisual
{
    private Image image;

    public Sprite sprite
    { get { return image.sprite; } private set { image.sprite = value; } }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    protected override void SetData(FlowItem data)
    {
        this._data = data;

        sprite = data.sprite;
    }
}
