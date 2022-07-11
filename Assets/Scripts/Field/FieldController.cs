using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class FieldController : MonoBehaviour
{
    [SerializeField] private Vector2 size;
    [Header("Foliage")]
    [SerializeField] private Foliage foliageRef;
    [SerializeField, Min(0)] private float foliageRadius = 0.52f;
    [Space]
    [SerializeField, Min(0)] private float growthTime = 10f;
    [SerializeField, Range(0, 1)] private float ripePlanck = 0.8f;
    [SerializeField, Min(0)] private float rotTime = 4f;
    [Header("Loot")]
    [SerializeField] private Loot lootRef;
    [SerializeField, Range(0, 100)] private int chanceToDrop = 80;
    [SerializeField, Min(0)] private float disappearTime = 10f;

    private ObjectPool<Loot> lootPool;

    private Vector3 Center
    { get { return new Vector3(size.x / 2, 1, size.y / 2); } }
    private Vector3 Size
    { get { return new Vector3(size.x, 2, size.y); } }

    private void Awake()
    {
        InitCollider();
        InitPool();
    }

    void Start()
    {
        InitField();
    }

    private void InitField()
    {
        Vector2[] spawnPoints = PoissonDiscSamplinng.GeneratePoints(foliageRadius, size, 25);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Foliage foliage = Foliage.Construct.Clone(foliageRef, growthTime, ripePlanck, rotTime);

            foliage.onCutted += DropLoot;

            foliage.transform.parent = transform;
            foliage.transform.localPosition = new Vector3(spawnPoints[i].x, 0, spawnPoints[i].y);
            foliage.transform.localRotation = new Quaternion(0, Random.Range(0, 10), 0, 1);
        }
    }

    private void DropLoot(Foliage foliage)
    {
        int result = Random.Range(0, 100);

        if (result > chanceToDrop) return;

        Loot loot = lootPool.Get();

        loot.onLootTaken += OnLootTaken;
        loot.transform.position = foliage.transform.position;
    }

    private void OnLootTaken(Loot loot)
    {
        loot.onLootTaken -= OnLootTaken;

        lootPool.Release(loot);
    }

    private void InitCollider()
    {
        BoxCollider collider = GetComponent<BoxCollider>();

        collider.size = Size;
        collider.center = Center;
    }

    private void InitPool()
    {
        lootPool = new ObjectPool<Loot>(() =>
        {
            return Loot.Construct.Clone(lootRef, disappearTime);
        }, loot =>
        {
            loot.gameObject.SetActive(true);
        }, loot =>
        {
            loot.gameObject.SetActive(false);
            loot.transform.localPosition = Vector3.zero;
        }, loot =>
        {
            Destroy(loot);
        }, true, 100, 200);
    }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position + new Vector3(size.x / 2, 1, size.y / 2);
        Vector3 sz = new Vector3(size.x, 2, size.y);
        Gizmos.DrawWireCube(center, sz);
    }
}
