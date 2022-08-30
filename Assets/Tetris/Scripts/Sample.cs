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

    float span = 0.4f;
    private float currentTime = 0f;

    bool moveNow = false;

    void Update()
    {
        // ■ 入力受付
        if (moveNow) {
            if (Input.GetKeyDown(KeyCode.D)) {
                if (_boardService.CanMove(1, 0, _board, _mino)) {
                    _mino.MoveRight();
                    _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                }
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                if (_boardService.CanMove(-1, 0,_board, _mino)) {
                    _mino.MoveLeft();
                    _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
                }
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                _mino.RotateRight();
                _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                _mino.RotateLeft();
                _minoShadow.Set(_minoShadowService.GetMinoShadowPositions(_board, _mino));
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                _mino.MoveTo(_minoShadowService.GetMinoShadowPositions(_board, _mino));
            }
        }

        currentTime += Time.deltaTime;

        // ■ 0.4秒毎に実行する
        if(currentTime > span){

            // ■ ミノが移動中でなければ
            if (!moveNow) {
                // ■ 新しいミノを作成
                _mino = _minoSetManagement.PopMino();
                // ■ ミノViewを作って、通知購読
                foreach (MinoPiece piece in _mino.GetMinoPieces()) {
                    GameObject copy = GameObject.Instantiate(_minoViewPrefab);
                    // MinoPieceのポジションを変更したとき，Viewを更新
                    piece.WhenPositionChange.Subscribe(position => {
                        Vector2 pos = GetPosition(position);
                        copy.transform.position = pos;
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
                foreach (MinoPiece piece in _mino.GetMinoPieces()) {
                    _board.Add(piece);
                }
                return;
            }

            // ■ 動く
            _mino.MoveDown();
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
}
