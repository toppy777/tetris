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
                    _game.Score.Add(_game.Level, 1);
                    _game.Level.Set(_game.Score);
                    _game.MinoMoveSpeed.SetSpeed(_game.Level);
                }).AddTo(_game.Disposable);

                _levelView = _levelViewFactory.Create();
                ILevelDataView levelDataView = _levelView.GetScoreDataView();
                _levelDataPresenterFactory.Create(_game, levelDataView).AddTo(_game.Disposable);

                _scoreView = _scoreViewFactory.Create();
                IScoreDataView scoreDataView = _scoreView.GetScoreDataView();
                _scoreDataViewPresenterFactory.Create(_game, scoreDataView).AddTo(_game.Disposable);
            }

            _finishCanvasView.SetRestartButtonClick(() => {
                ClickButtonHandler();
                Execute();
            });

            _finishCanvasView.SetBackToTitleButton(() => {
                ClickButtonHandler();
                SceneManager.LoadScene("TitleScene");
            });

            _finishCanvasView.UnDisplay();

            _game.Board.WhenPieceCrossOver.First().Subscribe(_ => {
                _game.GameStatus.GameOver();
                if (_modeRepository.GetMode() == ModeType.Play) {
                    _finishCanvasView.SetScore(_game.Score.Value);
                    _finishCanvasView.DisplayScore();
                } else {
                    _finishCanvasView.DisplayFinishText();
                }
                _finishCanvasView.Display();
            });

            _game.MinoShadowBind = _minoShadowBindFactory.CreateMinoShadowBind(_game.MinoShadow, _game.Disposable);

            _game.GameStatus.Play();

            _createNextMinoUseCase.Execute();

            // ????????????????????????????????????
            _leftMouseClickPresenterFactory.Create(_game).AddTo(_game.Disposable);
            // ????????????????????????????????????
            _rightMouseClickPresenterFactory.Create(_game).AddTo(_game.Disposable);
            // ??????????????????????????????????????????
            _mouseMovePresenterFactory.Create(_game).AddTo(_game.Disposable);
            // ?????????????????????????????????
            _scrollUpPresenterFactory.Create(_game).AddTo(_game.Disposable);
            _scrollDownPresenterFactory.Create(_game).AddTo(_game.Disposable);
            // 0.5???????????????????????????
            _intervalPresenterFactory.Create(_game).AddTo(_game.Disposable);
        }

        public void ClickButtonHandler()
        {
            _game.Board.Clear();
            _game.Mino.Release();
            _game.MinoBind?.DestroyView();
            _game.NextMinoBind?.DestroyView();
            _game.HoldMinoBind?.DestroyView();
            _game.MinoShadowBind?.DestroyView();
            _game.Dispose();
            if (_modeRepository.GetMode() == ModeType.Play) {
                _levelView.Destroy();
                _scoreView.Destroy();
            }
            _finishCanvasView.Destroy();
        }
    }
}
