using UnityEngine;
using System.Collections.Generic;

public class NextMinoView
{
    List<GameObject> _blocks;
    Vector2 _pos;

    public NextMinoView(GameObject minoPiecePrefab, Vector2 pos)
    {
        for (int i = 0; i < 4; i++) {
            _blocks.Add(GameObject.Instantiate(minoPiecePrefab));
        }
        _pos = pos;
    }

    public void SetShape(List<Vector2Int> shape)
    {
        float sideLength = _blocks[0].GetComponent<Renderer>().bounds.size.x;
        int i = 0;
        foreach (Vector2Int pos in shape) {
            _blocks[i++].transform.position = new Vector2(_pos.x + pos.x * sideLength, _pos.y + pos.y * sideLength);
        }
    }
}
