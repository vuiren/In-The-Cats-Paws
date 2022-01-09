using Map_Generator_Project.Scripts.Data;
using Map_Generator_Project.Scripts.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace Map_Generator_Project.Scripts.Factories
{
    public class CellFactory: IFactory<Direction[], Vector2Int, Cell>
    {
        private GameObject _cellPrefab;
        private MapData _mapData;
        
        [Inject]
        public void Construct(MapCreatorStaticData staticData, MapData mapData)
        {
            _cellPrefab = staticData.cellPrefab;
            _mapData = mapData;
        }

        public Cell Create(Direction[] directions, Vector2Int coord)
        {
            var cell = Object
                .Instantiate(_cellPrefab, (coord * _mapData.cellSize), new Quaternion())
                .GetComponent<Cell>();
            
            cell.Construct(directions, coord);
            return cell;
        }
    }
}