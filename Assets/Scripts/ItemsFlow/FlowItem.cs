using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flow Item", menuName = "Flow Item")]
public class FlowItem : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public Mesh mesh;
}
