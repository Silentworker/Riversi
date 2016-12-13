using System;
using System.Collections.Generic;
using Assets.Scripts.controller.commands.step.deadlock;
using Assets.Scripts.controller.commands.step.win;
using Assets.Scripts.controller.events;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro;
using Assets.Scripts.sw.core.command.map;
using Assets.Scripts.sw.core.eventdispatcher;
using UnityEngine;
using Zenject;
using ICommand = Assets.Scripts.sw.core.command.ICommand;

namespace Assets.Scripts.controller.commands.step
{
    public class MakeStepCommand : SequenceMacro
    {
        private Cell[] _allowStepCells;
        private Cell[] _changingCells;
        private Cell _stepCell;

        [Inject]
        private ICommandsMap commandsMap;
        [Inject]
        private IPlayFieldModel playFieldModel;
        [Inject]
        private IEventDispatcher evendDispatcher;

        public override void Prepare()
        {
            Add(typeof(SaveGameCommand));
            Add(typeof(HideLightsAndTurnTextCommand));
            Add(typeof(SpawnCellsCommand)).WithData(new[] { _stepCell });
            Add(typeof(SpawnExplosionsCommand)).WithData(_changingCells);
            Add(typeof(SpawnCellsCommand)).WithData(_changingCells);
            Add(typeof(ShowLightTouchesCommand)).WithData(_allowStepCells);
            Add(typeof(ShowStatsCommand));
            Add(typeof(ShowGameResultsCommand)).WithGuard(typeof(IsFinishGameGuard));
        }

        public override void Execute(object data = null)
        {
            _stepCell = (Cell)data;

            if (_stepCell == null)
            {
                Debug.LogError("Step cell is null");
                DispatchComplete(false);
                return;
            }

            commandsMap.UnMap(GameEvent.MakeStep, typeof(MakeStepCommand));
            commandsMap.UnMap(GameEvent.StartGame, typeof(StartGameCommand));
            CompleteHandler += OnComplete;

            if (_stepCell.State != CellState.allow)
            {
                DispatchComplete(false);
                throw new Exception("Trying to make a step on not allowed cell");
            }

            _changingCells = playFieldModel.MakeStepAndGetChangingCells(_stepCell);
            _allowStepCells = playFieldModel.allowedStepCells;

            base.Execute();
        }

        private void OnComplete(ICommand command, bool success)
        {
            commandsMap.Map(GameEvent.MakeStep, typeof(MakeStepCommand));
            commandsMap.Map(GameEvent.StartGame, typeof(StartGameCommand));

            if (!playFieldModel.isFinishGame) evendDispatcher.DispatchEvent(GameEvent.InterStep);
        }
    }
}