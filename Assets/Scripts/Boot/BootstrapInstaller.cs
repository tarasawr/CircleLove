﻿using Services;
using Zenject;


public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IConfigService>().To<ConfigService>().AsSingle();
        Container.Bind<IAddressableService>().To<AddressableService>().AsSingle().NonLazy();
        Container.Bind<ISaveSystem>().To<SaveSystemJsonService>().AsSingle();
    }
}