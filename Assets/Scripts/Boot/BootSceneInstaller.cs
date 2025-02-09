using Enemy;
using Score;
using Services;
using UnityEngine;
using Zenject;

public class BootSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<IPathService>().To<PathService>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
        
        Container.Bind<IEnemyFactory>().FromMethod(context =>
        {
            var configService = context.Container.Resolve<IConfigService>();
            var enemyConfig = configService.GetConfig<EnemyConfig>();
            return new EnemyFactory(enemyConfig.PrefabAddresses);
        }).AsSingle().NonLazy();

    
        Container.Bind<IPositionProvider>().FromMethod(context =>
        {
            var camera = context.Container.Resolve<Camera>();
            return new PositionProvider(camera);
        }).AsSingle();


        Container.BindInterfacesTo<EnemyController>().AsSingle().NonLazy();
    }
}