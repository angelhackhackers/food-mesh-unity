using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<StartDragSignal>();
        Container.DeclareSignal<EndDragSignal>();
        Container.DeclareSignal<DraggingSignal>();

        Container.Bind<InputManager>().AsSingle();
        Container.Bind<Plate>().AsSingle();


    }
}