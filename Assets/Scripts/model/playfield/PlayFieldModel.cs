using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.consts;
using Assets.Scripts.controller.events;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.model;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.model.playfield
{
    public class PlayFieldModel : Model, IPlayFieldModel
    {

        [Inject]
        private IHeadsUpController headsUpController;

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
        private List<Cell> _allowStepCells = new List<Cell>();

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

            //_cells[2, 2].State = CellState.White;
            //_cells[3, 3].State = CellState.White;
            //_cells[4, 4].State = CellState.White;

            //_cells[2, 3].State = CellState.Black;
            //_cells[3, 4].State = CellState.Black;
            //_cells[4, 3].State = CellState.Black;

            Analize();

            //var ss = CheckChangableCells(_cells[1, 3]);


            var updateCells = _cells.Cast<Cell>().Where(cell => cell.State != CellState.Empty).ToList();

            eventDispatcher.DispatchEvent(GameEvent.ResetField, updateCells);

            Logging();
        }

        public void MakeStep(Cell stepCell)
        {
            if (stepCell.State != CellState.AllowStep)
                throw new Exception("Trying to make a step on not allowed cell");

            var changingCells = CheckChangableCells(stepCell);

            foreach (var cell in changingCells)
            {
                if (cell.State == CellState.White) cell.State = CellState.Black;
                else if (cell.State == CellState.Black) cell.State = CellState.White;
            }
            Debug.LogFormat("Change cells {0}: {1}", changingCells.Count, string.Join("#", changingCells.Select(x => x.ToString()).ToArray()));

            stepCell.State = CurrentStep;
            CurrentStep = OppositeStep;
            Analize();

            _allowStepCells.Clear();
            _allowStepCells.AddRange(_cells.Cast<Cell>().Where(cell => cell.State == CellState.AllowStep));

            eventDispatcher.DispatchEvent(GameEvent.MakeStep, new object[] { changingCells, _allowStepCells, stepCell });

            Logging();
        }

        private void ShowStats()
        {
            headsUpController.SetTurn("Turn: " + (CurrentStep == CellState.White ? "White" : "Black"));
            headsUpController.SetScore("W: {0}  B: {1}".Fmt(ScoreWhite, ScoreBlack));
        }

        public bool AllowStep(int X, int Y)
        {
            return _cells[X, Y].State == CellState.AllowStep;
        }

        private void Analize()
        {
            #region Check allow 
            foreach (var cell in _cells)
            {
                if (cell.State == CellState.AllowStep) cell.State = CellState.Empty;
            }

            foreach (var cell in _cells)
            {
                if (cell.State == CellState.Empty && CheckChangableCells(cell).Count > 0) cell.State = CellState.AllowStep;
            }
            #endregion

            #region Get score
            ScoreWhite = 0;
            ScoreBlack = 0;
            foreach (var cell in _cells)
            {
                if (cell.State == CellState.White) ScoreWhite++;
                else if (cell.State == CellState.Black) ScoreBlack++;
            }
            #endregion

            #region Check win
            if (ScoreWhite + ScoreBlack >= Distance.PlayFieldSize * Distance.PlayFieldSize)
            {
                eventDispatcher.DispatchEvent(GameEvent.GameComplete, new object[] { ScoreWhite, ScoreBlack });
            }
            #endregion

            #region Check deadlock
            if (_cells.Cast<Cell>().All(cell => cell.State != CellState.AllowStep)) //todo check this expression
            {
                eventDispatcher.DispatchEvent(GameEvent.Deadlock);
                CurrentStep = OppositeStep;
                Analize();
            }
            #endregion

            ShowStats();
        }

        private List<Cell> CheckChangableCells(Cell cell)
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

                    if ((spotX >= Distance.PlayFieldSize) || (spotY >= Distance.PlayFieldSize) || (spotX < 0) || (spotY < 0)) break; // out of field

                    var currentCell = _cells[spotX, spotY];

                    if (currentCell.State == CurrentStep)// same reached
                    {
                        foreach (var changingCell in changingLine)
                        {
                            if (!result.Contains(changingCell)) result.Add(changingCell);
                        }
                        break;
                    }

                    if ((currentCell.State == CellState.Empty) || (currentCell.State == CellState.AllowStep)) break; // empty cell reached

                    if (currentCell.State == OppositeStep) changingLine.Add(currentCell);
                }
            }

            if (result.Count > 0)
                Debug.LogFormat("Alow {0}: {1}", result.Count, string.Join("##", result.Select(x => x.ToString()).ToArray()));

            return result;
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