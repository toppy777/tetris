using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.MinoReserves;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Presenters.Minos;
using Tetris.Scripts.Presenters.NextMinos;
using Tetris.Scripts.Presenters.HoldMinos;

namespace Tetris.Scripts.Application.Games
{
    public class CreateGameUseCase
    {
        MinoPieceView _minoPieceViewPrefab;
        Mino _mino;
        Board _board;
        MinoFactory _minoFactory;
        MinoShadow _minoShadow;
        BoardService _boardService;
        MinoShadowService _minoShadowService;
        MinoReserveList _minoReserveList;
        HoldMino _holdMino;

        private readonly CompositeDisposable _disposable = new();

        public CreateGameUseCase(
            MinoPieceView prefab
        )
        {
            _minoPieceViewPrefab = prefab;
            _minoFactory = new MinoFactory();
            _board = new Board();
            _mino = null;
            _minoShadow = new MinoShadow();
            _boardService = new BoardService();
            _minoShadowService = new MinoShadowService(_boardService);
            _minoReserveList = new MinoReserveList();
            _holdMino = new HoldMino();

            List<Vector2> posList = new List<Vector2>();
            for (int i = 0; i < 6; i++) {
                posList.Add(new Vector2(2.72f, 3.12f - 0.6f * i));
            }

            // MinoShadowのViewを作成
            List<MinoPieceView> minoShadowPieceViews = new List<MinoPieceView>();
            for (int i = 0; i < 4; i++) {
                MinoPieceView copy = GameObject.Instantiate(_minoPieceViewPrefab);
                copy.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 30);
                minoShadowPieceViews.Add(copy);
            }

            // MinoShadowのポジション変更通知購読
            _minoShadow.WhenPositionsChange.Subscribe(positions => {
                for (int i = 0; i < minoShadowPieceViews.Count; i++) {
                    minoShadowPieceViews[i].transform.position = GetPosition(positions[i]);
                }
            }).AddTo(_disposable);
        }

        public void Execute()
        {
            CreateNextMino();

            // ゲームオーバー時の処理
            bool gameOverFlg = false;
            _board.WhenPieceCrossOver.First().Subscribe(_ => {
                gameOverFlg = true;
                Debug.Log("Game Over");
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
                    // _moveNow = false;
                    _mino.MoveTo(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                    _board.Add(_mino);
                    // _mino = null;
                    _mino.Release();
                    // NextMinoSetから最初のNextMinoを削除
                    // _nextMinoSet.DeleteHead();
                }).AddTo(_disposable);

            // 右クリックしたときの処理
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(1))
                .Where(_ => _mino.Exists())
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    if (_holdMino.Exists()) {
                        MinoType minoType = _mino.MinoType;
                        _mino.Delete();
                        _mino.Release();
                        CreateMino(_holdMino.GetMinoType());
                        _holdMino.Set(minoType);
                    } else {
                        _holdMino.Set(_mino.MinoType);
                        _mino.Delete();
                        _mino.Release();
                    }
                    new HoldMinoPresenter(_holdMino, _minoPieceViewPrefab);
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
            Observable.Interval(TimeSpan.FromSeconds(0.5f))
                .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ 動けるか調べる
                    // Can Move Down?
                    if (!_boardService.CanMove(0, -1, _board, _mino)) {
                        // _moveNow = false;
                        // ■ 盤面に固定
                        _board.Add(_mino);
                        // _mino = null;
                        _mino.Release();
                        return;
                    }

                    // ■ 動く
                    _mino.MoveTo(_mino.Position.X, _mino.Position.Y-1);
                }).AddTo(_disposable);
        }

        private const float xBegin = 0.24f;
        private const float yBegin = 0.24f;
        public Vector2 GetPosition(Vector2Int indexPos)
        {
            float x = indexPos.x * 0.16f;
            float y = indexPos.y * 0.16f;

            return new Vector2(x + xBegin, y + yBegin);
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
        }

        public void CreateMino(MinoType minoType)
        {
            _mino = _minoFactory.CreateMino(minoType);
            new MinoPresenter(_mino, _minoPieceViewPrefab);
            new NextMinoPresenter(_minoReserveList, _minoPieceViewPrefab);
        }
    }
}
