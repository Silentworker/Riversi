﻿using System;
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
        private List<Cell> _changingCells;
        private List<Cell> _stepCellList;

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
            Add(typeof(SpawnCellsCommand)).WithData(_stepCellList);
            Add(typeof(SpawnExplosionsCommand)).WithData(_changingCells);
            Add(typeof(SpawnCellsCommand)).WithData(_changingCells);
            Add(typeof(ShowLightTouchesCommand)).WithData(_allowStepCells);
            Add(typeof(ShowStatsCommand));
            Add(typeof(ShowGameResultsCommand)).WithGuard(typeof(IsFinishGameGuard));
        }

        public override void Execute(object data = null)
        {
            var stepCell = (Cell)data;

            if (stepCell == null)
            {
                Debug.LogError("Step cell is null");
                DispatchComplete(false);
                return;
            }

            commandsMap.UnMap(GameEvent.MakeStep, typeof(MakeStepCommand));
            commandsMap.UnMap(GameEvent.StartGame, typeof(StartGameCommand));
            CompleteHandler += OnComplete;

            if (stepCell.State != CellState.allow)
            {
                DispatchComplete(false);
                throw new Exception("Trying to make a step on not allowed cell");
            }

            _stepCellList = new List<Cell> { stepCell };

            _changingCells = playFieldModel.MakeStepAndGetChangingCells(stepCell);
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