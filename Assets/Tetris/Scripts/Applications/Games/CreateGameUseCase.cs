using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.Others;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Application.Games
{
    public class CreateGameUseCase
    {
        Game _game;
        IFinishCanvasView _finishCanvasView;
        CreateNextMinoUseCase _createNextMinoUseCase;
        ILeftMouseClickPresenterFactory _leftMouseClickPresenterFactory;
        IRightMouseClickPresenterFactory _rightMouseClickPresenterFactory;
        IMouseMovePresenterFactory _mouseMovePresenterFactory;
        IScrollUpPresenterFactory _scrollUpPresenterFactory;
        IScrollDownPresenterFactory _scrollDownPresenterFactory;
        IIntervalPresenterFactory _intervalPresenterFactory;
        GameRegistry _gameRegistry;

        private readonly CompositeDisposable _disposable = new();

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
            IIntervalPresenterFactory intervalPresenterFactory
        )
        {
            _createNextMinoUseCase = createNextMinoUseCase;
            _leftMouseClickPresenterFactory = leftMouseClickPresenterFactory;
            _rightMouseClickPresenterFactory = rightMouseClickPresenterFactory;
            _mouseMovePresenterFactory = mouseMovePresenterFactory;
            _scrollUpPresenterFactory = scrollUpPresenterFactory;
            _scrollDownPresenterFactory = scrollDownPresenterFactory;
            _intervalPresenterFactory = intervalPresenterFactory;

            _game = new Game();
            _gameRegistry = gameRegistry;
            _gameRegistry.Register(_game);

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

            // ゲームオーバー時の処理
            bool gameOverFlg = false;
            _game.Board.WhenPieceCrossOver.First().Subscribe(_ => {
                gameOverFlg = true;
                _finishCanvasView.Display();
            }).AddTo(_disposable);

            var gameOverObservable = Observable.EveryUpdate().Where(_ => gameOverFlg);

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
