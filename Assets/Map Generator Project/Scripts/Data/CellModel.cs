using System;
using System.Linq;
using UnityEngine;

namespace Map_Generator_Project.Scripts.Data
{
    [Serializable]
    public sealed class CellModel
    {
        [SerializeField] public Direction[] directions;
        [SerializeField] public int x, y;

        public CellModel(Direction[] directions, int x, int y)
        {
            this.directions = directions;
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            var stringDirections = directions.Select(x => x + "\n");

            var result = $"Cell coords: x:{x}, y:{y}, directions:\n";
            foreach (var stringDirection in stringDirections) result += stringDirection;

            return result;
        }
    }
}