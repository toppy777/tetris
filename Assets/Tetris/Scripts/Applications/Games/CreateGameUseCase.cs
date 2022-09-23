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
                _game.Disposables.Add(
                    _game.Board.WhenRowRemove.Subscribe(_ => {
                        _game.Point.Add(_game.Level, 1);
                        _game.Level.Set(_game.Point);
                        _game.MinoMoveSpeed.SetSpeed(_game.Level);
                        Debug.Log($"ポイント: {_game.Point.Value}");
                        Debug.Log($"レベル: {_game.Level.Value}");
                    })
                );

                _levelView = _levelViewFactory.Create();
                _game.Disposables.Add(
                    _levelDataPresenterFactory.Create(_game)
                );
                _levelView.Display();

                _scoreView = _scoreViewFactory.Create();
                _game.Disposables.Add(
                    _scoreDataViewPresenterFactory.Create(_game)
                );
                _scoreView.Display();
            }

            _finishCanvasView.SetRestartButtonClick(() => {
                // ボードクリア
                _game.Board.Clear();
                _game.Mino.Release();
                _game.MinoShadowBind?.Dispose();
                _game.MinoBind?.Dispose();
                _game.NextMinoBind?.Dispose();
                _game.HoldMinoBind?.Dispose();
                _finishCanvasView.UnDisplay();
                Execute();
            });

            _finishCanvasView.SetBackToTitleButton(() => {
                _game.Board.Clear();
                _game.Mino.Release();
                _game.MinoShadowBind?.Dispose();
                _game.MinoBind?.Dispose();
                _game.NextMinoBind?.Dispose();
                _game.HoldMinoBind?.Dispose();
                _finishCanvasView.Destroy();
                if (_modeRepository.GetMode() == ModeType.Play) {
                    // score level view を削除
                    _levelView.Destroy();
                    _scoreView.Destroy();
                }
                SceneManager.LoadScene("TitleScene");
            });

            _finishCanvasView.UnDisplay();

            _game.Board.WhenPieceCrossOver.First().Subscribe(_ => {
                _game.GameStatus.GameOver();
                _finishCanvasView.Display();
                _game.Dispose();
            });

            _game.MinoShadowBind = _minoShadowBindFactory.CreateMinoShadowBind(_game.MinoShadow);

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
