using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.MinoSets;
using UniRx;
using System;
using System.Threading.Tasks;

public class Sample : MonoBehaviour
{
    [SerializeField] GameObject _minoViewPrefab;
    Mino _mino;
    Board _board;
    MinoFactory _minoFactory;
    MinoShadow _minoShadow;
    BoardService _boardService;
    MinoShadowService _minoShadowService;
    MinoSetList _minoSetManagement;

    private void Awake()
    {
        _minoFactory = new MinoFactory();
        _board = new Board();
        _mino = null;
        _minoShadow = new MinoShadow();
        _boardService = new BoardService();
        _minoShadowService = new MinoShadowService(_boardService);
        _minoSetManagement = new MinoSetList(
            _minoFactory.CreateMinos(),
            _minoFactory.CreateMinos()
        );

        _minoSetManagement.WhenMinoSetRemove.Subscribe(_ => {
            _minoSetManagement.AddMinoSet(
                _minoFactory.CreateMinos()
            );
        });

        // MinoShadowのViewを作成
        List<GameObject> minoShadowPieceViews = new List<GameObject>();
        for (int i = 0; i < 4; i++) {
            GameObject copy = GameObject.Instantiate(_minoViewPrefab);
            copy.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 30);
            minoShadowPieceViews.Add(copy);
        }

        // MinoShadowのポジション変更通知購読
        _minoShadow.WhenPositionsChange.Subscribe(positions => {
            for (int i = 0; i < minoShadowPieceViews.Count; i++) {
                minoShadowPieceViews[i].transform.position = GetPosition(positions[i]);
            }
        });
    }

    float span = 1f;
    private float currentTime = 0f;

    bool moveNow = false;

    void Update()
    {
        // ■ 入力受付
        if (moveNow) {

            Vector3 cameraPosition = Input.mousePosition;
            cameraPosition.z = 10.0f;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(cameraPosition);

            var posX = GetPositionX(mousePos.x);

            if (_boardService.HasSpaceForMino(_board, _mino, new Vector2Int(posX, _mino.Position.Y))) {
                _mino.MoveTo(posX, _mino.Position.Y);
                _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
            }

            float wh = Input.GetAxis("Mouse ScrollWheel");
            if (wh > 0) {
                if (_boardService.HasSpaceForMino(_board, _mino, _mino.GetPrevShape())) {
                    _mino.RotateLeft();
                    _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                }
            }
            if (wh < 0) {
                if (_boardService.HasSpaceForMino(_board, _mino, _mino.GetNextShape())) {
                    _mino.RotateRight();
                    _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                moveNow = false;
                _mino.MoveTo(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                _board.Add(_mino);
                return;
            }
        }

        currentTime += Time.deltaTime;

        // ■ 0.4秒毎に実行する
        if(currentTime > span){

            // ■ ミノが移動中でなければ
            if (!moveNow) {
                // ■ 新しいミノを作成
                _mino = _minoSetManagement.PopMino();
                // ■ ミノViewを作る
                List<GameObject> minoViews = new List<GameObject>();
                for (int i = 0; i < 4; i++) {
                    var copy = GameObject.Instantiate(_minoViewPrefab);
                    copy.GetComponent<Renderer>().material.color = MinoPieceColor.GetColor(_mino.Color);
                    minoViews.Add(copy);
                }
                // ■ Minoのポジション変更された時の処理を登録
                List<IObservable<Vector2Int>> observables = _mino.GetWhenChangePositionObservables();
                int index = 0;
                foreach (GameObject minoView in minoViews) {
                    observables[index++].Subscribe(pos => {
                        minoView.transform.position = GetPosition(pos);
                    });
                }

                // ■ Minoが削除された時の処理を登録
                List<IObservable<Unit>> observables1 = _mino.GetWhenDeleteObservables();
                index = 0;
                foreach (GameObject minoView in minoViews) {
                    observables1[index++].Subscribe(_ => {
                        GameObject.Destroy(minoView);
                    });
                }
                moveNow = true;
            }

            currentTime = 0f;

            // ■ 動けるか調べる
            // Can Move Down?
            if (!_boardService.CanMove(0, -1, _board, _mino)) {
                moveNow = false;
                // ■ 盤面に固定
                _board.Add(_mino);
                return;
            }

            // ■ 動く
            _mino.MoveTo(_mino.Position.X, _mino.Position.Y-1);
        }
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
}
