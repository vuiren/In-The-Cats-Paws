using System;
using System.Collections.Generic;
using Map_Generator_Project.Scripts.Data;
using Map_Generator_Project.Scripts.Factories;
using Map_Generator_Project.Scripts.MonoBehaviours;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Map_Generator_Project.Scripts
{
    public class MapDrawer
    {
        private GameObject _upDownCorridorPrefab, _leftRightCorridorPrefab, _cellPrefab;
        private CellFactory _cellFactory;
        private readonly Dictionary<CellModel, Cell> cellViews = new();
        
        public Action<Dictionary<CellModel, Cell>> OnMapDrawed { get; set; }

        [Inject]
        public void Construct(MapGenerator mapGenerator, CellFactory cellFactory)
        {
            mapGenerator.OnMapGenerated += DrawMap;
            _cellFactory = cellFactory;
        }

        public void DrawMap(MapData mapData)
        {
            CleanMap();
            
            DrawRooms(mapData);
            DrawCorridors(mapData);

            OnMapDrawed?.Invoke(cellViews);
        }

        private void DrawCorridors(MapData mapData)
        {
            
        }
        
        public void CleanMap()
        {
            foreach (var cell in cellViews)
            {
               Object.Destroy(cell.Value.gameObject);
            }

            cellViews.Clear();
        }

        private void DrawRooms(MapData mapData)
        {
            
            foreach (var mapCell in mapData.Cells)
            {
                Debug.Log("Drawing cell: " + mapCell);
                var coord = new Vector2Int(mapCell.x, mapCell.y);
                var cell = _cellFactory.Create(mapCell.directions, coord);
                cellViews.Add(mapCell, cell);
            }
        }
    }
}