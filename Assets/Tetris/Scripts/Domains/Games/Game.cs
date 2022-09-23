using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.PlacePredictions;
using Tetris.Scripts.Domains.MinoReserves;
using Tetris.Scripts.Domains.Levels;
using Tetris.Scripts.Domains.Scores;
using Tetris.Scripts.Domains.HorizontalPositions;
using Tetris.Scripts.Domains.GameStatuses;
using Tetris.Scripts.Domains.MinoMoveSpeeds;

namespace Tetris.Scripts.Domains.Games
{
    public class Game
    {
        public Game()
        {
            Mino = null;
            Board = new Board();
            MinoReserveList = new MinoReserveList();
            MinoShadow = new MinoShadow();
            HoldMino = new HoldMino();
            Level = new Level();
            Point = new Score();
            HorizontalPosition = new HorizontalPosition();
            Disposables = new List<IDisposable>();
            GameStatus = new GameStatus();
            MinoMoveSpeed = new MinoMoveSpeed();
        }

        public Mino Mino { get; set; }
        public IMinoBind MinoBind { get; set; }
        public Board Board { get; }
        public MinoReserveList MinoReserveList { get; }
        public INextMinoBind NextMinoBind { get; set; }
        public MinoShadow MinoShadow { get; }
        public IMinoShadowBind MinoShadowBind { get; set; }
        public HoldMino HoldMino { get; }
        public Level Level { get; }
        public Score Point { get; }
        public HorizontalPosition HorizontalPosition { get; }
        public List<IDisposable> Disposables { get; }
        public GameStatus GameStatus { get; }
        public MinoMoveSpeed MinoMoveSpeed { get; }

        public void Dispose()
        {
            foreach (IDisposable disposable in Disposables) {
                disposable?.Dispose();
            }
            Disposables.Clear();
        }
    }
}
