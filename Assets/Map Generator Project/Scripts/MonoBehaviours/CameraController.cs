using DG.Tweening;
using Map_Generator_Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace Map_Generator_Project.Scripts
{
    public class CameraController: MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private MapData _map;
        
        [Inject]
        public void Construct(MapGenerator mapGenerator, MapDrawer mapDrawer)
        {
            mapGenerator.OnMapGenerated += RegisterMap;
        }

        private void RegisterMap(MapData obj)
        {
            _map = obj;

            MoveToTheCenterOfTheMap(_map);
        }
        
        
        public void MoveToTheCenterOfTheMap(MapData mapData)
        {
            var xCellsCount = _map.Cells.GetLength(0);
            var yCellsCount =  _map.Cells.GetLength(1);
            var cellSize = mapData.cellSize;
            
            var xSize = xCellsCount * cellSize.x / 2f - cellSize.x / 2f;
            var ySize = yCellsCount * cellSize.y / 2f - cellSize.y / 2f;

            var size = Mathf.Max(xCellsCount, yCellsCount) * 2f + 1f;
            _camera.DOOrthoSize(size, 0.5f);
            transform.DOMove(new Vector3(xSize, ySize, transform.position.z), 0.5f);
        }
    }
}