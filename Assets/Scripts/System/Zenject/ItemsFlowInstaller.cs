using UnityEngine;
using Zenject;

public class ItemsFlowInstaller : MonoInstaller
{
    [SerializeField] ItemsFlowWorldUI itemsFlowUI;
    [SerializeField] ItemsFlowWorld itemsFlowWorld;
    public override void InstallBindings()
    {
        Container.Bind<ItemsFlowWorldUI>().FromInstance(itemsFlowUI).AsSingle().NonLazy();
        Container.Bind<ItemsFlowWorld>().FromInstance(itemsFlowWorld).AsSingle().NonLazy();
    }
}