using Assets.Scripts.controller.config;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.controller.factory.explosion;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.controller.settings;
using Assets.Scripts.model.ai;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro.mapper;
using Assets.Scripts.sw.core.command.map;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.settings;
using Zenject;

namespace Assets.Scripts.core
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        public HeadsUpController headsUpControllerInstance;
        public ChipsFactory chipsFactoryInstance;
        public LightTouchFactory lightTouchFactoryInstance;
        public ExplosionFactory explosionFactoryInstance;

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
            //Container.Bind<IPlayFieldModel>().WithId(AIProcessor.CalculationFieldModelIdentyfier).To<PlayFieldModel>().AsSingle();
            Container.Bind<ISettingsManager>().To<SettingsManager>().AsSingle();
            Container.Bind<IAIProcessor>().To<AIProcessor>().AsSingle();
            Container.Bind<IAIModel>().To<AIModel>().AsSingle();

            #endregion

            #region MonoBehaviour instances
            Container.Bind<IChipFactory>().FromInstance(chipsFactoryInstance).AsSingle();
            Container.Bind<ILightTouchFactory>().FromInstance(lightTouchFactoryInstance).AsSingle();
            Container.Bind<IHeadsUpController>().FromInstance(headsUpControllerInstance).AsSingle();
            Container.Bind<IExplosionFactory>().FromInstance(explosionFactoryInstance).AsSingle();
            #endregion
        }
    }
}