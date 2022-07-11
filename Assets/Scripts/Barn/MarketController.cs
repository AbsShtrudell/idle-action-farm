using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    [Header("Coins Flow")]
    [SerializeField] private Transform barn;
    [SerializeField] private RectTransform coinsIcon;
    [SerializeField] private FlowItem coin;
    [SerializeField] private FlowItem resource;
    [SerializeField] private int price = 15;

    [Zenject.Inject] private ItemsFlowWorldUI itemsFlowUI;
    [Zenject.Inject] private ItemsFlowWorld itemsFlowWorld;
    [Zenject.Inject] private PlayerController playerController;
    [Zenject.Inject] private SellZone sellZone;
    [Zenject.Inject] private Bag bag;

    private void OnEnable()
    {
        sellZone.onPlayerEnter += StartSelling;

        itemsFlowWorld.onItemStartMove += OnResourceStartMove;
        itemsFlowWorld.onFlowComplete += CoinStartFlow;
        itemsFlowUI.onItemReachedDestination += OnCoinReachDestination;
    }

    private void OnDisable()
    {
        sellZone.onPlayerEnter -= StartSelling;

        itemsFlowWorld.onItemStartMove -= OnResourceStartMove;
        itemsFlowWorld.onFlowComplete -= CoinStartFlow;
        itemsFlowUI.onItemReachedDestination -= OnCoinReachDestination;
    }

    private void StartSelling()
    {
        itemsFlowWorld.StartFlow(bag.transform, barn, resource, playerController.ResourcesCount);
    }

    private void OnResourceStartMove(FlowItem flowItem)
    {
        TakeResource();
    }

    private void CoinStartFlow(int amount)
    {
        itemsFlowUI.StartFlow(barn, coinsIcon, coin, amount);
    }

    private void OnCoinReachDestination(FlowItem flowItem)
    {
        GiveCoin(price);
    }

    private void TakeResource()
    {
        playerController.DecreaseResources(1);
    }

    private void GiveCoin(int amount)
    {
        playerController.AddCoins(amount);
    }
}
