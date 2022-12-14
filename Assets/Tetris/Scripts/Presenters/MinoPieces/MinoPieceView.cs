using UnityEngine;
using System;
using UniRx;

namespace Tetris.Scripts.Presenters.MinoPieces
{
    public class MinoPieceView : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        public void SetColor(Color32 color)
        {
            GetComponent<Renderer>().material.color = color;
        }

        public void SetPosition(Vector2 pos)
        {
            GetComponent<Transform>().position = pos;
        }

        public void Delete()
        {
            if (gameObject != null) Destroy(gameObject);
        }
    }
}
