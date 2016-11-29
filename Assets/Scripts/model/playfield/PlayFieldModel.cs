using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.consts;
using Assets.Scripts.controller.events;
using Assets.Scripts.controller.headsup;
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

        [Inject]
        private IHeadsUpController headsUpController;

        public PlayFieldModel(IEventDispatcher dispatcher, DiContainer container) : base(dispatcher, container)
        {
        }

        private byte oppositeStep
        {
            get { return currentStep == CellState.white ? CellState.black : CellState.white; }
        }

        public byte currentStep { get; private set; }

        public int scoreBlack { get; private set; }

        public int scoreWhite { get; private set; }

        public Cell[] allowedStepCells
        {
            get { return _cells.Cast<Cell>().Where(cell => cell.State == CellState.allow).ToArray(); }
        }

        public Cell[] notEmptyCells
        {
            get { return _cells.Cast<Cell>().Where(cell => cell.State != CellState.empty).ToArray(); }
        }

        public void ResetGame()
        {
            currentStep = CellState.black;

            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                for (var j = 0; j < Distance.PlayFieldSize; j++)
                {
                    _cells[i, j] = new Cell(i, j, CellState.empty);
                }
            }

            _cells[3, 3].State = CellState.white;
            _cells[3, 4].State = CellState.black;
            _cells[4, 3].State = CellState.black;
            _cells[4, 4].State = CellState.white;

            Analize();

            Logging();
        }

        public void SwitchStepOnDeadLock()
        {
            currentStep = oppositeStep;
            Analize();
        }

        public List<Cell> CalculateChangingCells(Cell stepCell)
        {
            var changingCells = ChangingCells(stepCell);
            foreach (var cell in changingCells)
            {
                if (cell.State == CellState.white) cell.State = CellState.black;
                else if (cell.State == CellState.black) cell.State = CellState.white;
            }

            stepCell.State = currentStep;

            currentStep = oppositeStep;

            Analize();

            Logging();

            return changingCells;
        }

        public List<Cell> ChangingCells(Cell cell)
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
                    var spotX = cell.X + deltaX * i;
                    var spotY = cell.Y + deltaY * i;

                    if ((spotX >= Distance.PlayFieldSize) || (spotY >= Distance.PlayFieldSize) || (spotX < 0) ||
                        (spotY < 0)) break; // out of field

                    var currentCell = _cells[spotX, spotY];

                    if (currentCell.State == currentStep) // same reached
                    {
                        foreach (var changingCell in changingLine)
                        {
                            if (!result.Contains(changingCell)) result.Add(changingCell);
                        }
                        break;
                    }

                    if ((currentCell.State == CellState.empty) || (currentCell.State == CellState.allow))
                        break; // empty cell reached

                    if (currentCell.State == oppositeStep) changingLine.Add(currentCell);
                }
            }

            return result;
        }

        public bool AllowStep(int X, int Y)
        {
            return _cells[X, Y].State == CellState.allow;
        }

        private void Analize()
        {
            #region Check allow 
            foreach (var cell in _cells)
            {
                if (cell.State == CellState.allow) cell.State = CellState.empty;
            }

            foreach (var cell in _cells)
            {
                if (cell.State == CellState.empty && ChangingCells(cell).Count > 0) cell.State = CellState.allow;
            }
            #endregion

            #region Get score
            scoreWhite = 0;
            scoreBlack = 0;
            foreach (var cell in _cells)
            {
                if (cell.State == CellState.white) scoreWhite++;
                else if (cell.State == CellState.black) scoreBlack++;
            }
            #endregion

            #region Check win
            if (scoreWhite + scoreBlack >= Distance.PlayFieldSize * Distance.PlayFieldSize)
            {
                eventDispatcher.DispatchEvent(GameEvent.GameComplete);
            }
            #endregion

            #region Check deadlock
            if (_cells.Cast<Cell>().All(cell => cell.State != CellState.allow))
            {
                eventDispatcher.DispatchEvent(GameEvent.Deadlock);
            }
            #endregion
        }

        private void Logging()
        {
            var c = _cells;
            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                Debug.LogFormat("{0}      {1}      {2}      {3}      {4}      {5}      {6}      {7}",
                c[0, i].State, c[1, i].State, c[2, i].State, c[3, i].State, c[4, i].State, c[5, i].State, c[6, i].State, c[7, i].State);
            }
        }
    }
}