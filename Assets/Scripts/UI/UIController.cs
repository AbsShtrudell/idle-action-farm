using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [Zenject.Inject] private PlayerController playerController;

    [SerializeField] private TextMeshProUGUI stackText;
    [SerializeField] private TextMeshProUGUI maxStackText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private RectTransform stackIcon;
    [SerializeField] private RectTransform coinsIcon;

    private void OnEnable()
    {
        playerController.onResourcesChanged += ChangeStackText;
        playerController.onCoinsChanged += ChangeCoinsText;

        stackText.text = playerController.ResourcesCount.ToString();
        maxStackText.text = playerController.BagCapacity.ToString();
        coinsText.text = playerController.CoinsCount.ToString();
    }

    private void OnDisable()
    {
        playerController.onResourcesChanged -= ChangeStackText;
        playerController.onCoinsChanged -= ChangeCoinsText;
    }

    private void ChangeStackText(int stackCount)
    {
        stackText.text = stackCount.ToString();

        ImpactIcon(stackIcon);
    }

    private void ChangeCoinsText(int coinsCount)
    {
        coinsText.text = coinsCount.ToString();

        ImpactIcon(coinsIcon);
    }

    private void ImpactIcon(RectTransform icon)
    {
        icon.DOKill();
        icon.localScale = Vector3.one;

        icon.DOShakeScale(0.3f, 0.2f, 0);
    }
}
