using Assets.Scripts.controller.events;
using Assets.Scripts.controller.settings;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.model;
using Assets.Scripts.sw.core.settings;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.core
{
    public class ApplicationModel : Model
    {
        [Inject]
        private IPlayFieldModel _playFieldModel;
        [Inject]
        private ISettingsManager settingsManager;

        public ApplicationModel(IEventDispatcher dispatcher, DiContainer container) : base(dispatcher, container)
        {
        }

        public void Init()
        {
            DOVirtual.DelayedCall(0.1f, () =>
            {
                var cells = (Cell[,])settingsManager.GetSetting(SettingName.Cells);
                var turn = (byte)settingsManager.GetSetting(SettingName.Turn);

                eventDispatcher.DispatchEvent(GameEvent.StartGame, new object[] { cells, turn });
            });
        }
    }
}