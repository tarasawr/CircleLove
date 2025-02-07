using Enemy;
using Save;
using Score;
using Zenject;

public class BootInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AddressableService>().AsSingle().NonLazy();
        
        Container.Bind<ISaveSystem>().To<SaveSystemJsonService>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle().NonLazy();

        Container.Bind<IEnemyFactory>().To<EnemyFactoryService>().AsSingle();
    }
}