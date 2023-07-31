using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private Containers _containers;

    public override void InstallBindings()
    {
        BindUI();
        BindContainers();
        BindStatistic();
    }

    private void BindUI()
    {
        Container
            .Bind<UI>()
            .FromComponentInHierarchy()
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

    private void BindStatistic()
    {
        Container
            .Bind<Statistic>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}
