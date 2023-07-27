using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        BindStatistic();
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
