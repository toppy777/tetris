using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.Boards;

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

    void Start()
    {
        List<Vector2Int> positions = _mino.GetPiecePositions();
        foreach(Vector2Int indexPos in positions) {
            Vector2 pos = GetPosition(indexPos);
            GameObject copy = Instantiate(_minoViewPrefab);
            copy.transform.position = pos;
            _minoViews.Add(copy);
        }
    }

    float span = 0.1f;
    private float currentTime = 0f;

    bool now = false;

    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > span){

            // 新しいミノを作成
            if (now) {
                foreach (MinoPiece piece in _mino.GetMinoPieces()) {
                    _board.Add(piece);
                }
                _mino = _minoFactory.CreateJMino();
                now = false;

                List<Vector2Int> poss = _mino.GetPiecePositions();
                _minoViews.Clear();
                foreach(Vector2Int indexPos in poss) {
                    Vector2 pos = GetPosition(indexPos);
                    GameObject copy = Instantiate(_minoViewPrefab);
                    copy.transform.position = pos;
                    _minoViews.Add(copy);
                }
            }

            currentTime = 0f;
            // gameObject.transform.position = _displayMinoService.GetPosition(gameObject.transform.position);

            List<Vector2Int> positions = _mino.GetPiecePositions();
            foreach(Vector2Int indexPos in positions) {
                if (!_board.CheckBoundary(indexPos.x, indexPos.y-1)) {
                    now = true;
                    return;
                }
                if (_board.Exists(indexPos.x, indexPos.y-1)) {
                    now = true;
                    return;
                }
            }

            _mino.MoveDown();

            positions = _mino.GetPiecePositions();

            for (int i = 0; i < _minoViews.Count; i++) {
                _minoViews[i].transform.position = GetPosition(positions[i]);
            }
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
