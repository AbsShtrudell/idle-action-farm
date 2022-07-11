using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Collider))]
public class Loot : MonoBehaviour
{
    [Header("Drop Animation")]
    [SerializeField] private float strength = 0.5f;
    [SerializeField] private float duration = 0.5f;

    private Collider _collider;

    private float disappearDelay = 10f;

    public event Action<Loot> onLootTaken;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        PlayDropAnimation();
    }

    public void Take()
    {
        StopAllCoroutines();

        onLootTaken?.Invoke(this);
    }

    private void PlayDropAnimation()
    {
        _collider.enabled = false;

        transform.DOPunchPosition(Vector3.up * strength, duration, 0, 1).OnComplete(() => 
            { 
                _collider.enabled = true; 
                StartCoroutine(DisappearTimer()); 
            });
    }

    private IEnumerator DisappearTimer()
    {
        yield return new WaitForSeconds(disappearDelay);

        Take();
    }

    public static class Construct
    {
        public static Loot Clone(Loot lootRef, float disapper_time)
        {
            Loot loot = GameObject.Instantiate(lootRef);

            loot.disappearDelay = disapper_time;

            return loot;
        }
    }
}
