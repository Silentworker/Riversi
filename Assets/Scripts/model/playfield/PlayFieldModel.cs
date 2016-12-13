using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.consts;
using Assets.Scripts.controller.events;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.controller.settings;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.model;
using Assets.Scripts.sw.core.settings;
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

        [Inject]
        private ISettingsManager settingsManager;

        public PlayFieldModel(IEventDispatcher dispatcher, DiContainer container) : base(dispatcher, container)
        {
            CalcMode = false;
        }

        private byte oppositeTurn
        {
            get { return currentTurn == CellState.white ? CellState.black : CellState.white; }
        }

        public byte currentTurn { get; private set; }

        public bool isFinishGame
        {
            get { return scoreBlack + scoreWhite >= Distance.PlayFieldSize * Distance.PlayFieldSize; }
        }

        public bool isDeadlock
        {
            get
            {
                var cells = allowedStepCells;
                return !isFinishGame && (cells == null || cells.Length == 0);
            }
        }

        public bool CalcMode { get; set; }

        public int scoreBlack { get; private set; }

        public int scoreWhite { get; private set; }

        public Cell[] allowedStepCells
        {
            get { return _cells.Cast<Cell>().Where(cell => cell.State == CellState.allow).ToArray(); }
        }

        public Cell[] chipCells
        {
            get { return _cells.Cast<Cell>().Where(cell => cell.State == CellState.white || cell.State == CellState.black).ToArray(); }
        }

        public void Init(Cell[,] cells = null, byte turn = 0)
        {
            if (cells != null)
            {
                _cells = cells;
                currentTurn = turn;
            }
            else
            {
                #region Default playfield

                currentTurn = CellState.black;

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

                #endregion
            }

            Debug.LogWarningFormat("Init playfield. Current turn {0}", currentTurn == CellState.black ? "black" : "white");

            Analize();

            Logging();
        }

        public void SwitchStepOnDeadLock()
        {
            currentTurn = oppositeTurn;
            Analize();
        }

        public void SaveCurrentState()
        {
            settingsManager.SetSetting(SettingName.Cells, _cells.Cast<Cell>().ToList());
            settingsManager.SetSetting(SettingName.Turn, currentTurn);
        }

        public Cell[,] GetCellsClone()
        {
            return (Cell[,])_cells.Clone();
        }

        public Cell GetCell(int X, int Y)
        {
            return _cells.Cast<Cell>().FirstOrDefault(cell => cell.X == X && cell.Y == Y);
        }

        public Cell[] MakeStepAndGetChangingCells(Cell stepCell)
        {
            var changingCells = ChangingCells(stepCell);
            foreach (var cell in changingCells)
            {
                if (cell.State == CellState.white) cell.State = CellState.black;
                else if (cell.State == CellState.black) cell.State = CellState.white;
            }

            stepCell.State = currentTurn;

            currentTurn = oppositeTurn;

            Analize();

            return changingCells;
        }

        public Cell[] ChangingCells(Cell cell)
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

                    if (currentCell.State == currentTurn) // same reached
                    {
                        foreach (var changingCell in changingLine)
                        {
                            if (!result.Contains(changingCell)) result.Add(changingCell);
                        }
                        break;
                    }

                    if ((currentCell.State == CellState.empty) || (currentCell.State == CellState.allow))
                        break; // empty cell reached

                    if (currentCell.State == oppositeTurn) changingLine.Add(currentCell);
                }
            }

            return result.ToArray();
        }

        public bool AllowStep(int X, int Y)
        {
            return _cells[X, Y].State == CellState.allow;
        }

        private void Analize()
        {
            foreach (var cell in _cells)
            {
                if (cell.State == CellState.allow) cell.State = CellState.empty;
            }

            foreach (var cell in _cells)
            {
                if (cell.State == CellState.empty && ChangingCells(cell).Length > 0) cell.State = CellState.allow;
            }

            scoreWhite = 0;
            scoreBlack = 0;
            foreach (var cell in _cells)
            {
                if (cell.State == CellState.white) scoreWhite++;
                else if (cell.State == CellState.black) scoreBlack++;
            }

            if (!CalcMode && isDeadlock)
            {
                eventDispatcher.DispatchEvent(GameEvent.Deadlock);
                SwitchStepOnDeadLock();
            }

            Logging();
        }

        private void Logging()
        {
            var c = _cells;
            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                Debug.LogFormat("{0}      {1}      {2}      {3}      {4}      {5}      {6}      {7}",
                    c[0, i].State, c[1, i].State, c[2, i].State, c[3, i].State,
                    c[4, i].State, c[5, i].State, c[6, i].State, c[7, i].State);
            }
        }
    }
}