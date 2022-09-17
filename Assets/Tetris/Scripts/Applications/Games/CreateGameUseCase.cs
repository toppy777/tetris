using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.MoveDownTimes;
using Tetris.Scripts.Domains.HorizontalPositions;
using Tetris.Scripts.Domains.Others;

using Tetris.Scripts.Application.Minos;
using Tetris.Scripts.Application.HoldMinos;

namespace Tetris.Scripts.Application.Games
{
    public class CreateGameUseCase
    {
        Game _game;

        BoardService _boardService;

        IFinishCanvasView _finishCanvasView;

        CreateNextMinoUseCase _createNextMinoUseCase;
        MinoMoveDownUseCase _minoMoveDownUseCase;
        MinoMoveHorizontalUseCase _minoMoveHorizontalUseCase;
        MinoRotateRightUseCase _minoRotateRightUseCase;
        MinoRotateLeftUseCase _minoRotateLeftUseCase;
        FastPlaceMinoUseCase _fastPlaceMinoUseCase;
        PlaceMinoUseCase _placeMinoUseCase;

        SetHoldMinoUseCase _setHoldMinoUseCase;
        SwapHoldMinoUseCase _swapHoldMinoUseCase;

        GameRegistry _gameRegistry;

        private readonly CompositeDisposable _disposable = new();

        public CreateGameUseCase(
            IFinishCanvasViewFactory finishCanvasViewFactory,
            IMinoShadowBindFactory minoShadowBindFactory,
            CreateNextMinoUseCase createNextMinoUseCase,
            GameRegistry gameRegistry,
            MinoMoveDownUseCase minoMoveDownUseCase,
            MinoMoveHorizontalUseCase minoMoveHorizontalUseCase,
            MinoRotateRightUseCase minoRotateRightUseCase,
            MinoRotateLeftUseCase minoRotateLeftUseCase,
            FastPlaceMinoUseCase fastPlaceMinoUseCase,
            PlaceMinoUseCase placeMinoUseCase,
            SetHoldMinoUseCase setHoldMinoUseCase,
            SwapHoldMinoUseCase swapHoldMinoUseCase
        )
        {
            _createNextMinoUseCase = createNextMinoUseCase;
            _minoMoveDownUseCase = minoMoveDownUseCase;
            _minoMoveHorizontalUseCase = minoMoveHorizontalUseCase;
            _minoRotateRightUseCase = minoRotateRightUseCase;
            _minoRotateLeftUseCase = minoRotateLeftUseCase;
            _fastPlaceMinoUseCase = fastPlaceMinoUseCase;
            _placeMinoUseCase = placeMinoUseCase;
            _setHoldMinoUseCase = setHoldMinoUseCase;
            _swapHoldMinoUseCase = swapHoldMinoUseCase;

            _game = new Game();
            _gameRegistry = gameRegistry;
            _gameRegistry.Register(_game);

            _boardService = new BoardService();

            _finishCanvasView = finishCanvasViewFactory.CreateFinishCanvasView();
            _finishCanvasView.SetRestartButtonClick(() => {
                // ボードクリア
                _game.Board.Clear();
                _game.Mino.Release();
                _finishCanvasView.UnDisplay();
                Execute();
            });
            _finishCanvasView.SetBackToTitleButton(() => SceneManager.LoadScene("TitleScene"));
            _finishCanvasView.UnDisplay();

            _game.Board.WhenRowRemove.Subscribe(_ => {
                _game.Point.Add(_game.Level, 1);
                _game.Level.Calc(_game.Point);
                Debug.Log($"ポイント: {_game.Point.Value}");
                Debug.Log($"レベル: {_game.Level.Value}");
            }).AddTo(_disposable);

            minoShadowBindFactory.CreateMinoShadowBind(_game.MinoShadow);
        }

        public void Execute()
        {
            _createNextMinoUseCase.Execute();

            // ゲームオーバー時の処理
            bool gameOverFlg = false;
            _game.Board.WhenPieceCrossOver.First().Subscribe(_ => {
                gameOverFlg = true;
                _finishCanvasView.Display();
            }).AddTo(_disposable);

            var gameOverObservable = Observable.EveryUpdate().Where(_ => gameOverFlg);

            // カーソルを動かしたときの処理
            Observable.EveryUpdate()
                .Where(_ => _game.Mino.Exists())
                .Where(_ => _game.HorizontalPosition.Value != HorizontalPosition.GetHorizontalPos())
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ MinoShadowを移動(Presenterのどこかのクラスに移動)
                    _minoMoveHorizontalUseCase.TryExecute();
                }).AddTo(_disposable);

            // 左クリックしたときの処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ => _game.Mino.Exists())
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ Mino即置き
                    _fastPlaceMinoUseCase.Execute();
                }).AddTo(_disposable);

            // 右クリックしたときの処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(1))
                .Where(_ => _game.Mino.Exists())
                .Where(_ => _game.HoldMino.IsAvailable)
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    if (_game.HoldMino.Exists) {
                        // ■ HoldMinoに登録
                        _swapHoldMinoUseCase.Execute();
                    } else {
                        // ■ HoldMinoに登録(初回)
                        _setHoldMinoUseCase.Execute();
                    }
                }).AddTo(_disposable);

            // スクロールした時の処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetAxis("Mouse ScrollWheel") > 0)
                .Where(_ => _game.Mino.Exists())
                .Where(_ => _boardService.HasSpaceForMino(_game.Board, _game.Mino, _game.Mino.GetNextShape()))
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ 回転
                    _minoRotateLeftUseCase.Execute();
                }).AddTo(_disposable);

            // スクロールした時の処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetAxis("Mouse ScrollWheel") < 0)
                .Where(_ => _game.Mino.Exists())
                .Where(_ => _boardService.HasSpaceForMino(_game.Board, _game.Mino, _game.Mino.GetNextShape()))
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ 回転
                    _minoRotateRightUseCase.Execute();
                }).AddTo(_disposable);

            // minoが空になったら新しいミノを作る
            Observable.EveryUpdate()
                .Where(_ => !_game.Mino.Exists())
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    _createNextMinoUseCase.Execute();
                }).AddTo(_disposable);
            
            // 0.5秒毎に実行する処理
            Observable.Interval(TimeSpan.FromSeconds(MoveDownTime.GetSeconds(_game.Level.Value)))
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ 動けるか調べる
                    // Can Move Down?
                    if (!_boardService.CanMove(0, -1, _game.Board, _game.Mino)) {
                        // ■ 盤面に固定
                        _placeMinoUseCase.Execute();
                        return;
                    }

                    // ■ 動く
                    _minoMoveDownUseCase.Execute();
                }).AddTo(_disposable);
        }

    }
}
