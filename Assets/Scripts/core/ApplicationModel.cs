using System.Collections.Generic;
using Assets.Scripts.consts;
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
            DOVirtual.DelayedCall(0.5f, () =>
            {
                var loadCells = (List<Cell>)settingsManager.GetSetting(SettingName.Cells);

                Cell[,] cells = null;

                if (loadCells != null)
                {
                    cells = new Cell[Distance.PlayFieldSize, Distance.PlayFieldSize];
                    foreach (var loadCell in loadCells)
                    {
                        cells[loadCell.X, loadCell.Y] = loadCell;
                    }
                }

                var turn = (byte)settingsManager.GetSetting(SettingName.Turn);

                eventDispatcher.DispatchEvent(GameEvent.StartGame, new object[] { cells, turn });
            });
        }
    }
}