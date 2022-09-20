using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;
using Tetris.Scripts.Domains.MinoReserves;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Domains.MinoShapes;
using Tetris.Scripts.Domains.MinoColors;
using Tetris.Scripts.Presenters.MinoPieces;

namespace Tetris.Scripts.Presenters.NextMinos
{
    public class NextMinoBind : INextMinoBind, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        public NextMinoBind(
            MinoReserveList minoReserveList,
            MinoPieceView minoPieceView
        )
        {
            List<Vector2> posList = new List<Vector2>();
            for (int i = 0; i < 7; i++) {
                posList.Add(new Vector2(2.72f, 3.12f - 0.6f * i));
            }

            // Minoの形と色を取り出して、特定の位置に配置
            int index = 0;
            List<MinoPieceView> pieceViews = new List<MinoPieceView>();
            foreach (MinoType minoType in minoReserveList.GetLastMinoTypes()) {
                MinoShape minoShapePatten = new MinoShape(minoType);
                MinoColor minoColor = new MinoColor(minoType);

                for (int i = 0; i < 4; i++) {
                    MinoPieceView pieceView = GameObject.Instantiate(minoPieceView);
                    pieceView.AddTo(_disposable);
                    pieceView.SetColor(minoColor.Value);
                    pieceView.SetPosition(new Vector2(posList[index].x + minoShapePatten.GetShape()[i].x * 0.16f, posList[index].y + minoShapePatten.GetShape()[i].y * 0.16f));
                    pieceViews.Add(pieceView);
                }
                index++;
            }

            // foreach (MinoPieceView view in pieceViews) {
            //     minoReserveList.WhenPop.Subscribe(_ => {
            //         if (view != null) {
            //             view.Delete();
            //         }
            //     }).AddTo(_disposable);
            // }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
