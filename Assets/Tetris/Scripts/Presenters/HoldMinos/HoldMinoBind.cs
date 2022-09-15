using UnityEngine;
using System.Collections.Generic;
using UniRx;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Domains.MinoShapes;
using Tetris.Scripts.Domains.MinoColors;

namespace Tetris.Scripts.Presenters.HoldMinos
{
    public class HoldMinoBind
    {
        public HoldMinoBind(
            HoldMino holdMino,
            MinoPieceView minoPieceViewPrefab
        )
        {
            Vector2 pos = new Vector2(-1, 3.2f);

            MinoShape minoShapePatten = new MinoShape(holdMino.GetMinoType());
            MinoColor minoColor = new MinoColor(holdMino.GetMinoType());

            // minoのview作成
            List<MinoPieceView> pieceViews = new List<MinoPieceView>();
            int pieceNum = 4;
            for (int i = 0; i < pieceNum; i++) {
                MinoPieceView copy = GameObject.Instantiate(minoPieceViewPrefab);
                copy.SetColor(minoColor.Value);
                copy.SetPosition(new Vector2(pos.x + minoShapePatten.GetShape()[i].x * 0.16f, pos.y + minoShapePatten.GetShape()[i].y * 0.16f));
                pieceViews.Add(copy);
            }

            foreach (MinoPieceView view in pieceViews) {
                holdMino.WhenSet.Subscribe(_ => {
                    if (view != null) {
                        GameObject.Destroy(view.gameObject);
                    }
                });
            }
        }
    }
}
