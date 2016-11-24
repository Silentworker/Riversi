using System;
using System.Collections.Generic;
using Assets.Scripts.consts;
using Assets.Scripts.controller.events;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.model;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.model.playfield
{
    public class PlayFieldModel : Model, IPlayFieldModel
    {
        private readonly int[][] Way =
        {
            /*   0° */ new[] {1, 0},
            /*  45° */ new[] {1, 1},
            /*  90° */ new[] {0, 1},
            /* 135° */ new[] {-1, 1},
            /* 180° */ new[] {-1, 0},
            /* 225° */ new[] {-1, -1},
            /* 270° */ new[] {0, -1},
            /* 315° */ new[] {1, -1}
        };

        private Cell[,] _cells = new Cell[Distance.PlayFieldSize, Distance.PlayFieldSize];

        public PlayFieldModel(IEventDispatcher dispatcher, DiContainer container) : base(dispatcher, container)
        {
        }

        private byte OppositeStep
        {
            get { return CurrentStep == CellState.White ? CellState.Black : CellState.White; }
        }

        private byte CurrentStep { get; set; }

        private int ScoreBlack { get; set; }

        private int ScoreWhite { get; set; }

        private int AllowStepCellsAmount { get; set; }

        public void StartGame()
        {
            CurrentStep = CellState.Black;

            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                for (var j = 0; j < Distance.PlayFieldSize; j++)
                {
                    _cells[i, j] = new Cell(i, j, CellState.Empty);
                }
            }

            _cells[3, 3].State = CellState.White;
            _cells[3, 4].State = CellState.Black;
            _cells[4, 3].State = CellState.Black;
            _cells[4, 4].State = CellState.White;

            Analize();

            Logging();

            MakeStep(3, 2);
        }

        public void MakeStep(int X, int Y)
        {
            if (_cells[X, Y].State != CellState.AllowStep)
                throw new Exception("Trying to make a step on not allowed cell");

            var changingCells = CheckChangableCells(X, Y);

            foreach (var cell in changingCells)
            {
                cell.State = CurrentStep;
            }

            _cells[X, Y].State = CurrentStep;

            eventDispatcher.DispatchEvent(GameEvent.RotateCells, new object[] { changingCells, _cells[X, Y] });

            CurrentStep = OppositeStep;

            Analize();

            Logging();
        }

        public bool AllowStep(int X, int Y)
        {
            return _cells[X, Y].State == CellState.AllowStep;
        }

        private void Analize()
        {
            CheckAllowStep();
            GetScore();
            CheckWin();
            CheckNoAllowStep();
        }

        private void CheckNoAllowStep()
        {
            if (AllowStepCellsAmount > 0) return;

            eventDispatcher.DispatchEvent(GameEvent.Deadlock);
            ChangeCurrentStep();
        }

        private void ChangeCurrentStep()
        {
            CurrentStep = OppositeStep;
        }

        private void CheckWin()
        {
            if (ScoreWhite + ScoreBlack >= Distance.PlayFieldSize * Distance.PlayFieldSize)
            {
                eventDispatcher.DispatchEvent(GameEvent.GameComplete,
                    new GameResult { ScoreWhite = ScoreWhite, ScoreBlack = ScoreBlack });
            }
        }

        private void GetScore()
        {
            ScoreWhite = 0;
            ScoreBlack = 0;
            AllowStepCellsAmount = 0;
            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                for (var j = 0; j < Distance.PlayFieldSize; j++)
                {
                    if (_cells[i, j].State == CellState.White) ScoreWhite++;
                    else if (_cells[i, j].State == CellState.Black) ScoreBlack++;
                    else if (_cells[i, j].State == CellState.AllowStep) AllowStepCellsAmount++;
                }
            }
        }

        private void CheckAllowStep()
        {
            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                for (var j = 0; j < Distance.PlayFieldSize; j++)
                {
                    if (_cells[i, j].State == CellState.AllowStep) _cells[i, j].State = CellState.Empty;
                    if (CheckChangableCells(i, j).Count > 0) _cells[i, j].State = CellState.AllowStep;
                }
            }
        }

        private List<Cell> CheckChangableCells(int X, int Y)
        {
            var result = new List<Cell>();

            foreach (var way in Way)
            {
                var deltaX = way[0];
                var deltaY = way[1];
                var changingLine = new List<Cell>();
                var i = 0;

                while (true)
                {
                    i++;
                    var spotX = X + deltaX * i;
                    var spotY = Y + deltaY * i;

                    if ((spotX >= Distance.PlayFieldSize) || (spotY >= Distance.PlayFieldSize) || (spotX < 0) || (spotY < 0)) break; // out of field

                    var cell = _cells[spotX, spotY];

                    if (cell.State == CurrentStep && changingLine.Count > 0)
                    {
                        result.AddRange(changingLine);
                        break; // same reached
                    }

                    if ((cell.State == CellState.Empty) || (cell.State == CellState.AllowStep)) break; // empty cell reached

                    if (cell.State == OppositeStep) changingLine.Add(cell);
                }
            }

            return result;
        }

        private void OppositeCell(Cell cell)
        {
            if (cell.State == CellState.Black) cell.State = CellState.White;
            if (cell.State == CellState.White) cell.State = CellState.Black;
        }

        private void Logging()
        {
            Debug.LogFormat("Score. White: {0}. Black: {1}", ScoreWhite, ScoreBlack);

            var c = _cells;
            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                Debug.LogFormat("{0}      {1}      {2}      {3}      {4}      {5}      {6}      {7}",
                             c[i, 0].State, c[i, 1].State, c[i, 2].State, c[i, 3].State, c[i, 4].State, c[i, 5].State, c[i, 6].State, c[i, 7].State);
            }

            //Debug.LogFormat("Length: {0}");
        }
    }
}