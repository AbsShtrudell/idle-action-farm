using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(Collider), typeof(MeshRenderer), typeof(MeshFilter))]
public class Foliage : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Mesh cuttedMesh;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private float ripePlanck = 0.8f;
    private float lifeTime = 1f;
    private float growthTime = 20f;
    private float rotTime = 4f;

    public event Action<Foliage> onCutted;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    public void Cut()
    {
        if(lifeTime >= ripePlanck)
            OnCutted();
    }

    private void OnCutted()
    {
        meshFilter.sharedMesh = cuttedMesh;

        transform.DOScale(0, rotTime).OnComplete( () => { StartCoroutine(Grow()); });

        onCutted?.Invoke(this);
    }

    private IEnumerator Grow()
    {
        float step = 1f / growthTime;

        meshFilter.sharedMesh = mesh;

        lifeTime = 0f;

        while(lifeTime < 1f)
        {
            lifeTime += step * Time.deltaTime;

            meshRenderer.material.SetFloat("_LifeTime", lifeTime);

            transform.localScale = Vector3.one * lifeTime;

            yield return new WaitForEndOfFrame();
        }

        lifeTime = 1f;
    }

    public static class Construct
    {
        public static Foliage Clone(Foliage foliageRef ,float growth_time, float ripe_planck, float rot_time)
        {
            Foliage foliage = GameObject.Instantiate(foliageRef);

            foliage.growthTime = growth_time;
            foliage.ripePlanck = ripe_planck;
            foliage.rotTime = rot_time;

            return foliage;
        }
    }
}
