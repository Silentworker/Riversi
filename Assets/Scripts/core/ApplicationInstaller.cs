using Assets.Scripts.controller.config;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro.mapper;
using Assets.Scripts.sw.core.command.map;
using Assets.Scripts.sw.core.eventdispatcher;
using Zenject;

namespace Assets.Scripts.core
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        public ChipsFactory chipsFactoryInstance;
        public LightTouchFactory lightTouchFactoryInstance;
        public HeadsUpController headsUpControllerInstance;

        public override void InstallBindings()
        {
            #region Core
            Container.Bind<IEventDispatcher>().To<EventDispatcher>().AsSingle().NonLazy();
            Container.Bind<ISubCommandMapper>().To<SubCommandMapper>();
            Container.Bind<ICommandsMap>().To<CommandsMap>().AsSingle();
            Container.Bind<ICommandsConfig>().To<CommandsConfig>();
            #endregion

            #region Common
            Container.Bind<ApplicationModel>().To<ApplicationModel>().AsSingle();
            Container.Bind<IPlayFieldModel>().To<PlayFieldModel>().AsSingle();
            #endregion

            #region MonoBehaviour instances
            Container.Bind<IChipFactory>().FromInstance(chipsFactoryInstance).AsSingle();
            Container.Bind<ILightTouchFactory>().FromInstance(lightTouchFactoryInstance).AsSingle();
            Container.Bind<IHeadsUpController>().FromInstance(headsUpControllerInstance).AsSingle();
            #endregion
        }
    }
}