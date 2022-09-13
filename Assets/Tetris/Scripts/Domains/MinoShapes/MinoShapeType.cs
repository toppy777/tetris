using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.MinoTypes;

namespace Tetris.Scripts.Domains.MinoShapes
{
    public class MinoShapeType
    {
        public static List<List<Vector2Int>> GetShape(MinoType minoType)
        {
            switch(minoType) {
                default:
                case MinoType.I:
                    return I;
                case MinoType.O:
                    return O;
                case MinoType.S:
                    return S;
                case MinoType.Z:
                    return Z;
                case MinoType.J:
                    return J;
                case MinoType.L:
                    return L;
                case MinoType.T:
                    return T;
            }
        }

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
                new Vector2Int(1,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(0,2)
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
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(1,2)
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
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(0,1),
                new Vector2Int(0,2)
            },
            new List<Vector2Int> {
                new Vector2Int(0,0),
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(2,1)
            },
            new List<Vector2Int> {
                new Vector2Int(1,0),
                new Vector2Int(1,1),
                new Vector2Int(0,2),
                new Vector2Int(1,2)
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
                new Vector2Int(0,2)
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
