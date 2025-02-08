using Save;
using Score;
using Zenject;

public class BootInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IConfigService>().To<ConfigService>().AsSingle();
        Container.Bind<IAddressableService>().To<AddressableService>().AsSingle().NonLazy();
        
        Container.Bind<ISaveSystem>().To<SaveSystemJsonService>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle().NonLazy();
    }
}