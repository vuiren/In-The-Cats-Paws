using System;
using Map_Generator_Project.Scripts.Data;
using QFSW.QC;
using UnityEngine;
using Zenject;

// ReSharper disable ExplicitCallerInfoArgument

namespace Map_Generator_Project.Scripts
{
    public class MapGenerator : MonoBehaviour
    {
        public Action<MapData> OnMapGenerated { get; set; }

        private MapData _mapData;


        [Inject]
        public void Construct(MapData mapData)
        {
            _mapData = mapData;
        }

        public void UpdateCell(CellModel cellModel)
        {
            _mapData.Cells[cellModel.x, cellModel.y] = cellModel;
        }

        private void GenerateMap()
        {
            var size = _mapData.size;
            _mapData.Cells = new CellModel[size.x, size.y];
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    _mapData.Cells[x, y] = new CellModel(Array.Empty<Direction>(), x, y);
                }
            }

            OnMapGenerated?.Invoke(_mapData);
        }

        [Command("map.generate")]
        private void GenerateMap(Vector2Int mapSize)
        {
            CleanMapData();
            
            _mapData.size = mapSize;
            GenerateMap();
        }

        private void GenerateMap(MapData mapData)
        {
            Debug.Log($"Map size x:{mapData.size.x} y: {mapData.size.y}");
            _mapData = mapData;
            OnMapGenerated?.Invoke(_mapData);
        }

        [Command("map.savemap")]
        private void SaveMap(string mapName)
        {
            Debug.Log("Saving map");
            MapExport.SaveFile(_mapData, mapName);
            Debug.Log("Done saving map");
        }

        [Command("map.showmapdata")]
        private void ShowMapData()
        {
            foreach (var cellModel in _mapData.Cells)
            {
                Debug.Log($"{cellModel}");
            }
        }
        
        [Command("map.loadMap")]
        private void LoadMap(string mapName)
        {
            CleanMapData();
            var data = MapExport.LoadFile(mapName);
            GenerateMap(data);
        }

        private void CleanMapData()
        {
            _mapData = new MapData();
        }
    }
}