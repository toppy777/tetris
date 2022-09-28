using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.Others;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Domains.Levels;
using Tetris.Scripts.Domains.Scores;
using Tetris.Scripts.Application.Minos;
using Tetris.Scripts.Infrastructures.BetweenScenes;

namespace Tetris.Scripts.Application.Games
{
    public class CreateGameUseCase
    {
        Game _game;
        IFinishCanvasView _finishCanvasView;
        IFinishCanvasViewFactory _finishCanvasViewFactory;
        IMinoShadowBindFactory _minoShadowBindFactory;
        CreateNextMinoUseCase _createNextMinoUseCase;
        ILeftMouseClickPresenterFactory _leftMouseClickPresenterFactory;
        IRightMouseClickPresenterFactory _rightMouseClickPresenterFactory;
        IMouseMovePresenterFactory _mouseMovePresenterFactory;
        IScrollUpPresenterFactory _scrollUpPresenterFactory;
        IScrollDownPresenterFactory _scrollDownPresenterFactory;
        IIntervalPresenterFactory _intervalPresenterFactory;
        ILevelDataPresenterFactory _levelDataPresenterFactory;
        ILevelView _levelView;
        IScoreDataViewPresenterFactory _scoreDataViewPresenterFactory;
        IScoreView _scoreView;
        GameRegistry _gameRegistry;
        ModeRepository _modeRepository;
        ILevelViewFactory _levelViewFactory;
        IScoreViewFactory _scoreViewFactory;

        public CreateGameUseCase (
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
            ILevelDataPresenterFactory levelDataPresenterFactory,
            IScoreDataViewPresenterFactory scoreDataViewPresenterFactory,
            ILevelViewFactory levelViewFactory,
            IScoreViewFactory scoreViewFactory
        )
        {
            _finishCanvasViewFactory = finishCanvasViewFactory;
            _createNextMinoUseCase = createNextMinoUseCase;
            _leftMouseClickPresenterFactory = leftMouseClickPresenterFactory;
            _rightMouseClickPresenterFactory = rightMouseClickPresenterFactory;
            _mouseMovePresenterFactory = mouseMovePresenterFactory;
            _scrollUpPresenterFactory = scrollUpPresenterFactory;
            _scrollDownPresenterFactory = scrollDownPresenterFactory;
            _intervalPresenterFactory = intervalPresenterFactory;
            _gameRegistry = gameRegistry;
            _minoShadowBindFactory = minoShadowBindFactory;
            _levelDataPresenterFactory = levelDataPresenterFactory;
            _scoreDataViewPresenterFactory = scoreDataViewPresenterFactory;
            _levelViewFactory = levelViewFactory;
            _scoreViewFactory = scoreViewFactory;
            _modeRepository = new ModeRepository();
        }

        public void Execute()
        {
            _game = new Game();
            _gameRegistry.Register(_game);

            _finishCanvasView = _finishCanvasViewFactory.CreateFinishCanvasView();

            if (_modeRepository.GetMode() == ModeType.Play)
            {
                _game.Board.WhenRowRemove.Subscribe(_ => {
                    _game.Point.Add(_game.Level, 1);
                    _game.Level.Set(_game.Point);
                    _game.MinoMoveSpeed.SetSpeed(_game.Level);
                }).AddTo(_game.Disposable);

                _levelView = _levelViewFactory.Create();
                _levelDataPresenterFactory.Create(_game).AddTo(_game.Disposable);
                _levelView.Display();

                _scoreView = _scoreViewFactory.Create();
                _scoreDataViewPresenterFactory.Create(_game).AddTo(_game.Disposable);
                _scoreView.Display();
            }

            _finishCanvasView.SetRestartButtonClick(() => {
                ClickButtonHandler();
                _finishCanvasView.UnDisplay();
                Execute();
            });

            _finishCanvasView.SetBackToTitleButton(() => {
                ClickButtonHandler();
                _finishCanvasView.Destroy();
                SceneManager.LoadScene("TitleScene");
            });

            _finishCanvasView.UnDisplay();

            _game.Board.WhenPieceCrossOver.First().Subscribe(_ => {
                _game.GameStatus.GameOver();
                _finishCanvasView.Display();
            });

            _minoShadowBindFactory.CreateMinoShadowBind(_game.MinoShadow, _game.Disposable);

            _game.GameStatus.Play();

            _createNextMinoUseCase.Execute();

            // 左クリックしたときの処理
            _leftMouseClickPresenterFactory.Create(_game).AddTo(_game.Disposable);
            // 右クリックしたときの処理
            _rightMouseClickPresenterFactory.Create(_game).AddTo(_game.Disposable);
            // カーソルを動かしたときの処理
            _mouseMovePresenterFactory.Create(_game).AddTo(_game.Disposable);
            // スクロールした時の処理
            _scrollUpPresenterFactory.Create(_game).AddTo(_game.Disposable);
            _scrollDownPresenterFactory.Create(_game).AddTo(_game.Disposable);
            // 0.5秒毎に実行する処理
            _intervalPresenterFactory.Create(_game).AddTo(_game.Disposable);
        }

        public void ClickButtonHandler()
        {
            _game.Board.Clear();
            _game.Mino.Release();
            _game.NextMinoBind.DestroyView();
            _game.HoldMinoBind.DestroyView();
            if (_modeRepository.GetMode() == ModeType.Play) {
                _levelView.Destroy();
                _scoreView.Destroy();
            }
            _game.Dispose();
        }
    }
}
