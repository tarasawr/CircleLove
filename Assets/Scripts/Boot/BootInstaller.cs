using Score;
using Services;
using UnityEngine;
using Zenject;

public class BootInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<IPathService>().To<PathService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
    }
}