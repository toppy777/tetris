using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.MinoReserves;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Domains.RowCounts;
using Tetris.Scripts.Domains.Levels;
using Tetris.Scripts.Domains.Points;
using Tetris.Scripts.Domains.MoveDownTimes;
using Tetris.Scripts.Domains.Others;

namespace Tetris.Scripts.Application.Games
{
    public class CreateGameUseCase
    {
        Mino _mino;
        Board _board;
        MinoFactory _minoFactory;
        MinoShadow _minoShadow;
        BoardService _boardService;
        MinoShadowService _minoShadowService;
        MinoReserveList _minoReserveList;
        HoldMino _holdMino;
        RowCount _rowCount;
        Level _level;
        Point _point;
        MoveDownTime _moveDownTime;

        IMinoBindFactory _minoBindFactory;
        INextMinoBindFactory _nextMinoBindFactory;
        IHoldMinoBindFactory _holdMinoBindFactory;
        IFinishCanvasView _finishCanvasView;

        private readonly CompositeDisposable _disposable = new();

        public CreateGameUseCase(
            IMinoBindFactory minoBindFactory,
            IFinishCanvasViewFactory finishCanvasViewFactory,
            INextMinoBindFactory nextMinoBindFactory,
            IHoldMinoBindFactory holdMinoBindFactory,
            IMinoShadowBindFactory minoShadowBindFactory
        )
        {
            _minoBindFactory = minoBindFactory;
            _nextMinoBindFactory = nextMinoBindFactory;
            _holdMinoBindFactory = holdMinoBindFactory;

            _minoFactory = new MinoFactory();
            _board = new Board();
            _mino = null;
            _minoShadow = new MinoShadow();
            _boardService = new BoardService();
            _minoShadowService = new MinoShadowService(_boardService);
            _minoReserveList = new MinoReserveList();
            _holdMino = new HoldMino();
            _rowCount = new RowCount();
            _level = new Level();
            _point = new Point();
            _moveDownTime = new MoveDownTime();

            _finishCanvasView = finishCanvasViewFactory.CreateFinishCanvasView();
            _finishCanvasView.SetRestartButtonClick(() => {
                // ボードクリア
                _board.Clear();
                _mino.Release();
                _finishCanvasView.UnDisplay();
                Execute();
            });
            _finishCanvasView.SetBackToTitleButton(() => SceneManager.LoadScene("TitleScene"));
            _finishCanvasView.UnDisplay();

            _board.WhenRowRemove.Subscribe(_ => {
                _rowCount.Add(1);
                _point.Add(_level, 1);
                _level.Calc(_point);
                Debug.Log($"ポイント: {_point.Value}");
                Debug.Log($"レベル: {_level.Value}");
            }).AddTo(_disposable);

            minoShadowBindFactory.CreateMinoShadowBind(_minoShadow);
        }

        public void Execute()
        {

            CreateNextMino();

            // ゲームオーバー時の処理
            bool gameOverFlg = false;
            _board.WhenPieceCrossOver.First().Subscribe(_ => {
                gameOverFlg = true;
                _finishCanvasView.Display();
            }).AddTo(_disposable);

            var gameOverObservable = Observable.EveryUpdate().Where(_ => gameOverFlg);

            // カーソルを動かしたときの処理
            Observable.EveryUpdate()
                .Where(_ => _mino.Exists())
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    Vector3 cameraPosition = Input.mousePosition;
                    cameraPosition.z = 10.0f;
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(cameraPosition);

                    var posX = GetPositionX(mousePos.x);

                    if (_boardService.HasSpaceForMino(_board, _mino, new Vector2Int(posX, _mino.Position.Y))) {
                        _mino.MoveTo(posX, _mino.Position.Y);
                        _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                    }
                }).AddTo(_disposable);

            // 左クリックしたときの処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ => _mino.Exists())
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    _mino.MoveTo(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                    _board.Add(_mino);
                    _mino.Release();
                }).AddTo(_disposable);

            // 右クリックしたときの処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(1))
                .Where(_ => _mino.Exists())
                .Where(_ => _holdMino.IsAvailable)
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    if (_holdMino.Exists) {
                        MinoType minoType = _mino.MinoType; _mino.Delete();
                        _mino.Release();
                        CreateMino(_holdMino.GetMinoType());
                        _holdMino.Set(minoType);
                    } else {
                        _holdMino.Set(_mino.MinoType);
                        _mino.Delete();
                        _mino.Release();
                    }
                    _holdMinoBindFactory.CreateHoldMinoBind(_holdMino);
                }).AddTo(_disposable);

            // スクロールした時の処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetAxis("Mouse ScrollWheel") > 0)
                .Where(_ => _mino.Exists())
                .Where(_ => _boardService.HasSpaceForMino(_board, _mino, _mino.GetNextShape()))
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    _mino.RotateLeft();
                    _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                }).AddTo(_disposable);

            // スクロールした時の処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetAxis("Mouse ScrollWheel") < 0)
                .Where(_ => _mino.Exists())
                .Where(_ => _boardService.HasSpaceForMino(_board, _mino, _mino.GetNextShape()))
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    _mino.RotateRight();
                    _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                }).AddTo(_disposable);

            // minoが空になったら新しいミノを作る
            Observable.EveryUpdate()
                .Where(_ => !_mino.Exists())
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    CreateNextMino();
                }).AddTo(_disposable);
            
            // 0.5秒毎に実行する処理
            Observable.Interval(TimeSpan.FromSeconds(MoveDownTime.GetSeconds(_level.Value)))
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ 動けるか調べる
                    // Can Move Down?
                    if (!_boardService.CanMove(0, -1, _board, _mino)) {
                        // ■ 盤面に固定
                        _board.Add(_mino);
                        _mino.Release();
                        return;
                    }

                    // ■ 動く
                    _mino.MoveTo(_mino.Position.X, _mino.Position.Y-1);
                }).AddTo(_disposable);
        }

        public int GetPositionX(float posX)
        {
            int ret = Mathf.FloorToInt(posX / 0.16f) - 1;
            if (ret < 0) {
                ret = 0;
            }
            if (ret > 9) {
                ret = 9;
            }
            return ret;
        }

        public void CreateNextMino()
        {
            CreateMino(_minoReserveList.PopMinoType());
            if (_holdMino.Exists) {
                if (!_holdMino.IsFirst) _holdMino.SetAvailable();
                _holdMino.SetNotFirst();
            }
        }

        public void CreateMino(MinoType minoType)
        {
            _mino = _minoFactory.CreateMino(minoType);
            _minoBindFactory.CreateMinoBind(_mino);
            _nextMinoBindFactory.CreateNextMinoBind(_minoReserveList);
        }
    }
}
