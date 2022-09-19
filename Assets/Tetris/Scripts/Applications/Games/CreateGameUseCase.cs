using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.Others;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Domains.Levels;
using Tetris.Scripts.Domains.Points;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Application.Games
{
    public class CreateGameUseCase
    {
        Game _game;
        IFinishCanvasView _finishCanvasView;
        IMinoShadowBindFactory _minoShadowBindFactory;
        CreateNextMinoUseCase _createNextMinoUseCase;
        ILeftMouseClickPresenterFactory _leftMouseClickPresenterFactory;
        IRightMouseClickPresenterFactory _rightMouseClickPresenterFactory;
        IMouseMovePresenterFactory _mouseMovePresenterFactory;
        IScrollUpPresenterFactory _scrollUpPresenterFactory;
        IScrollDownPresenterFactory _scrollDownPresenterFactory;
        IIntervalPresenterFactory _intervalPresenterFactory;
        ILevelPresenterFactory _levelPresenterFactory;
        IScoreViewPresenterFactory _scoreViewPresenterFactory;
        GameRegistry _gameRegistry;

        public CreateGameUseCase(
            IFinishCanvasViewFactory finishCanvasViewFactory,
            IMinoShadowBindFactory minoShadowBindFactory,
            CreateNextMinoUseCase createNextMinoUseCase,
            GameRegistry gameRegistry,
            ILeftMouseClickPresenterFactory leftMouseClickPresenterFactory,
            IRightMouseClickPresenterFactory rightMouseClickPresenterFactory,
            IMouseMovePresenterFactory mouseMovePresenterFactory,
            IScrollUpPresenterFactory scrollUpPresenterFactory,
            IScrollDownPresenterFactory scrollDownPresenterFactory,
            IIntervalPresenterFactory intervalPresenterFactory,
            ILevelPresenterFactory levelPresenterFactory,
            IScoreViewPresenterFactory scoreViewPresenterFactory
        )
        {
            _createNextMinoUseCase = createNextMinoUseCase;
            _leftMouseClickPresenterFactory = leftMouseClickPresenterFactory;
            _rightMouseClickPresenterFactory = rightMouseClickPresenterFactory;
            _mouseMovePresenterFactory = mouseMovePresenterFactory;
            _scrollUpPresenterFactory = scrollUpPresenterFactory;
            _scrollDownPresenterFactory = scrollDownPresenterFactory;
            _intervalPresenterFactory = intervalPresenterFactory;
            _gameRegistry = gameRegistry;
            _minoShadowBindFactory = minoShadowBindFactory;
            _finishCanvasView = finishCanvasViewFactory.CreateFinishCanvasView();
            _levelPresenterFactory = levelPresenterFactory;
            _scoreViewPresenterFactory = scoreViewPresenterFactory;
            _game = new Game();
            _gameRegistry.Register(_game);
        }

        public void Execute()
        {
            _game.Disposables.Add(
                _levelPresenterFactory.Create(_game)
            );

            _game.Disposables.Add(
                _scoreViewPresenterFactory.Create(_game)
            );

            _finishCanvasView.SetRestartButtonClick(() => {
                // ボードクリア
                _game.Board.Clear();
                _game.Mino.Release();
                _finishCanvasView.UnDisplay();
                Execute();
            });

            _finishCanvasView.SetBackToTitleButton(() => {
                SceneManager.LoadScene("TitleScene");
            });

            _finishCanvasView.UnDisplay();

            _game.Disposables.Add(
                _game.Board.WhenRowRemove.Subscribe(_ => {
                    _game.Point.Add(_game.Level, 1);
                    _game.Level.Set(_game.Point);
                    _game.MinoMoveSpeed.SetSpeed(_game.Level);
                    Debug.Log($"ポイント: {_game.Point.Value}");
                    Debug.Log($"レベル: {_game.Level.Value}");
                })
            );

            _game.Disposables.Add(
                _game.Board.WhenPieceCrossOver.First().Subscribe(_ => {
                    _game.GameStatus.GameOver();
                    _finishCanvasView.Display();
                    _game.Dispose();
                })
            );

            _minoShadowBindFactory.CreateMinoShadowBind(_game.MinoShadow);

            _game.GameStatus.Play();

            _createNextMinoUseCase.Execute();

            // 左クリックしたときの処理
            _game.Disposables.Add(_leftMouseClickPresenterFactory.Create(_game));
            // 右クリックしたときの処理
            _game.Disposables.Add(_rightMouseClickPresenterFactory.Create(_game));

            // カーソルを動かしたときの処理
            _game.Disposables.Add(_mouseMovePresenterFactory.Create(_game));

            // スクロールした時の処理
            _game.Disposables.Add(_scrollUpPresenterFactory.Create(_game));
            _game.Disposables.Add(_scrollDownPresenterFactory.Create(_game));

            // 0.5秒毎に実行する処理
            _game.Disposables.Add(_intervalPresenterFactory.Create(_game));
        }

    }
}
