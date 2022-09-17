using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.MinoReserves;
using Tetris.Scripts.Domains.Levels;
using Tetris.Scripts.Domains.Points;
using Tetris.Scripts.Domains.HorizontalPositions;

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
            Point = new Point();
            HorizontalPosition = new HorizontalPosition();
        }

        public Mino Mino { get; set; }
        public Board Board { get; }
        public MinoReserveList MinoReserveList { get; }
        public MinoShadow MinoShadow { get; }
        public HoldMino HoldMino { get; }
        public Level Level { get; }
        public Point Point { get; }
        public HorizontalPosition HorizontalPosition { get; }
    }
}
