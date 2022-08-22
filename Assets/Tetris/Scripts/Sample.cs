using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.Boards;
using UniRx;
using System;
using System.Threading.Tasks;

public class Sample : MonoBehaviour
{
    [SerializeField] GameObject _minoViewPrefab;
    Mino _mino;
    Board _board;
    List<GameObject> _minoViews = new List<GameObject>();
    MinoFactory _minoFactory;

    private void Awake()
    {
        _minoFactory = new MinoFactory();
        _board = new Board();
        _mino = _minoFactory.CreateTMino();
    }

    float span = 0.4f;
    private float currentTime = 0f;

    bool moveNow = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            if (CanMove(1,0)) {
                _mino.MoveRight();
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            if (CanMove(-1,0)) {
                _mino.MoveLeft();
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            _mino.RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            _mino.RotateLeft();
        }

        currentTime += Time.deltaTime;

        if(currentTime > span){

            // ミノが移動中でなければ
            if (!moveNow) {
                // 新しいミノを作成
                _mino = _minoFactory.CreateJMino();
                foreach (MinoPiece piece in _mino.GetMinoPieces()) {
                    GameObject copy = Instantiate(_minoViewPrefab);
                    // MinoPieceのポジションを変更したとき，Viewを更新
                    piece.WhenPositionChange.Subscribe(position => {
                        Vector2 pos = GetPosition(position);
                        copy.transform.position = pos;
                    });
                }
                moveNow = true;
            }

            currentTime = 0f;

            // Can Move Down?
            if (!CanMove(0,-1)) {
                moveNow = false;
                // 盤面に固定
                foreach (MinoPiece piece in _mino.GetMinoPieces()) {
                    _board.Add(piece);
                }
                return;
            }

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

    public bool CanMove(int distX, int distY)
    {
        List<Vector2Int> positions = _mino.GetPiecePositions();
        foreach(Vector2Int indexPos in positions) {
            if (!_board.CheckBoundary(indexPos.x + distX, indexPos.y + distY)) {
                return false;
            }
            if (_board.Exists(indexPos.x + distX, indexPos.y + distY)) {
                return false;
            }
        }
        return true;
    }
}
