using System;
using System.IO;
using Map_Generator_Project.Scripts.Data;
using UnityEngine;

namespace Map_Generator_Project.Scripts
{
    [Serializable]
    internal sealed class SaveData
    {
        public int mapSizeX, mapSizeY;
        public float cellSizeX, cellSizeY;
        public CellModel[] cells;

        public SaveData(MapData mapData)
        {
            mapSizeX = mapData.size.x;
            mapSizeY = mapData.size.y;

            cellSizeX = mapData.cellSize.x;
            cellSizeY = mapData.cellSize.y;

            var index = 0;
            var result = new CellModel[mapData.Cells.GetLength(0) * mapData.Cells.GetLength(1)];
            foreach (var cellModel in mapData.Cells)
            {
                result[index] = cellModel;
                index++;
            }

            cells = result;
        }
    }

    public static class MapExport
    {
        private static string path = Application.dataPath + "/Maps/";

        public static void SaveFile(MapData mapData, string fileName)
        {
            string destination = path + $"{fileName}.dat";
            Debug.Log($"Saving at path {destination}");

            if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            var saveData = new SaveData(mapData);

            var json = JsonUtility.ToJson(saveData);

            File.WriteAllText(destination, json);

            Debug.Log("File saved");
        }

        public static MapData LoadFile(string fileName)
        {
            string destination = path + $"{fileName}.dat";

            Debug.Log($"Loading map from {destination}");


            if (!File.Exists(destination))
            {
                Debug.LogError("File not found");
                return null;
            }

            var data = JsonUtility.FromJson<SaveData>(File.ReadAllText(destination));

            foreach (var cellModel in data.cells)
            {
                Debug.Log($"Loaded {cellModel}");
            }

            var resultCells = new CellModel[data.mapSizeX, data.mapSizeY];

            foreach (var cellModel in data.cells)
            {
                resultCells[cellModel.x, cellModel.y] = cellModel;
            }

            var mapData = new MapData()
            {
                cellSize = new Vector2(data.cellSizeX, data.cellSizeY),
                size = new Vector2Int(data.mapSizeX, data.mapSizeY),
                Cells = resultCells
            };

            foreach(var cellModel in mapData.Cells)
            {
                Debug.Log($"Result map data is {cellModel}");
            }

            Debug.Log("File loaded. Cells count: " + data.cells.Length);

            return mapData;
        }
    }
}