using System;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private Containers _containers;

    public override void InstallBindings()
    {
        BindSnake();
        BindUI();
        BindSpawner();
        BindContainers();
        BindStatistic();
    }

    private void BindSnake()
    {
        Container
            .Bind<Snake>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
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

    private void BindSpawner()
    {
        Container
            .Bind<Spawner>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}
