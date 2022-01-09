using System;
using UnityEngine;

namespace Map_Generator_Project.Scripts.Data
{
    [Serializable]
    public class MapData
    {
        public Vector2 cellSize;
        public Vector2Int size;
        public CellModel[,] Cells;
    }
}