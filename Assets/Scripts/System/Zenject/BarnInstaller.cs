using UnityEngine;
using Zenject;

public class BarnInstaller : MonoInstaller
{
    [SerializeField] private SellZone sellZone;
    [SerializeField] private MarketController marketController;
    public override void InstallBindings()
    {
        Container.Bind<SellZone>().FromInstance(sellZone).AsSingle().NonLazy();
        Container.Bind<MarketController>().FromInstance(marketController).AsSingle().NonLazy();
    }
}