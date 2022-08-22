using UnityEngine;
using System.Collections.Generic;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoShapeType
    {
        public static readonly List<List<Vector2Int>> I = new List<List<Vector2Int>>() {
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(2,0),
                new Vector2Int(3,0)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(0,1),
                new Vector2Int(0,2),
                new Vector2Int(0,3)
            },
        };

        public static readonly List<List<Vector2Int>> O = new List<List<Vector2Int>>() {
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1)
            },
        };

        public static readonly List<List<Vector2Int>> S = new List<List<Vector2Int>>() {
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(1,1),
                new Vector2Int(2,1)
            },
            new List<Vector2Int> {
                new Vector2Int(0,1),
                new Vector2Int(1,0),
                new Vector2Int(1,1),
                new Vector2Int(2,0)
            },
        };

        public static readonly List<List<Vector2Int>> Z = new List<List<Vector2Int>>() {
            new List<Vector2Int> {
                new Vector2Int(1,0),
                new Vector2Int(2,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(1,1),
                new Vector2Int(2,1)
            },
        };

        public static readonly List<List<Vector2Int>> J = new List<List<Vector2Int>>() {
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(2,0),
                new Vector2Int(0,1)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(0,1),
                new Vector2Int(0,2),
                new Vector2Int(1,2)
            },
            new List<Vector2Int> {
                new Vector2Int(2,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(2,1)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(1,1),
                new Vector2Int(1,2)
            },
        };

        public static readonly List<List<Vector2Int>> L = new List<List<Vector2Int>>() {
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(2,0),
                new Vector2Int(2,1)
            },
            new List<Vector2Int> {
                new Vector2Int(1,0),
                new Vector2Int(1,1),
                new Vector2Int(0,2),
                new Vector2Int(1,2)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(2,0),
                new Vector2Int(2,1)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(0,1),
                new Vector2Int(0,2)
            },
        };

        public static readonly List<List<Vector2Int>> T = new List<List<Vector2Int>>() {
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(2,0),
                new Vector2Int(1,1)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(0,0)
            },
            new List<Vector2Int> {
                new Vector2Int(1,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(2,1)
            },
            new List<Vector2Int> {
                new Vector2Int(1,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(1,2)
            },
        };
    }
}
