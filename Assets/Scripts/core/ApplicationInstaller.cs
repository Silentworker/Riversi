using Assets.Scripts.controller.chips;
using Assets.Scripts.controller.config;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro.mapper;
using Assets.Scripts.sw.core.command.map;
using Assets.Scripts.sw.core.eventdispatcher;
using Zenject;

namespace Assets.Scripts.core
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        public override void InstallBindings()
        {
            #region Core
            Container.Bind<IEventDispatcher>().To<EventDispatcher>().AsSingle().NonLazy();
            Container.Bind<ISubCommandMapper>().To<SubCommandMapper>();
            Container.Bind<ICommandsMap>().To<CommandsMap>().AsSingle();
            Container.Bind<ICommandsConfig>().To<CommandsConfig>();
            #endregion

            Container.Bind<ApplicationModel>().To<ApplicationModel>().AsSingle();
            Container.Bind<IPlayFieldModel>().To<PlayFieldModel>().AsSingle();
            Container.Bind<IChipFactory>().To<ChipsFactory>();
        }
    }
}