using System.Collections.Generic;
using Assets.Scripts.consts;
using Assets.Scripts.controller.events;
using Assets.Scripts.controller.settings;
using Assets.Scripts.model.ai;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.settings;
using DG.Tweening;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands.init
{
    public class InitGameCommand : Command
    {
        [Inject]
        private IAIModel aiModel;
        [Inject]
        private ISettingsManager settingsManager;
        [Inject]
        private IEventDispatcher eventDispatcher;

        public override void Execute(object data = null)
        {
            base.Execute();

            //aiModel.AddAIPlayer(Side.Dark);

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
        }
    }
}