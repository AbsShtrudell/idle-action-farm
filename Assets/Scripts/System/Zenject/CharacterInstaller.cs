using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private MovementController movementController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Sickle sickle;
    [SerializeField] private Bag bag;

    public override void InstallBindings()
    {
        Container.Bind<Animator>().FromInstance(characterAnimator).WhenInjectedInto<PlayerController>().NonLazy();
        Container.Bind<Sickle>().FromInstance(sickle).WhenInjectedInto<PlayerController>().NonLazy();
        Container.Bind<MovementController>().FromInstance(movementController).NonLazy();
        Container.Bind<PlayerController>().FromInstance(playerController).NonLazy();
        Container.Bind<Bag>().FromInstance(bag).AsSingle().NonLazy();
    }
}