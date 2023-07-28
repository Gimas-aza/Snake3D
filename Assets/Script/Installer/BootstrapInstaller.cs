using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private Containers _containers;

    public override void InstallBindings()
    {
        BindStatistic();
        BindContainers();
    }

    private void BindStatistic()
    {
        Container
            .Bind<Statistic>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void BindContainers()
    {
        Container
            .Bind<Containers>()
            .FromComponentInNewPrefab(_containers)
            .AsSingle()
            .NonLazy();
    }
}
