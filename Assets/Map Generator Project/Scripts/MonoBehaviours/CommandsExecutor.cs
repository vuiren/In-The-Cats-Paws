using Map_Generator_Project.Scripts.Data;
using QFSW.QC;
using UnityEngine;
using Zenject;

namespace Map_Generator_Project.Scripts.MonoBehaviours
{
    public class CommandsExecutor: MonoBehaviour
    {
        private EditorModeService _editorModeService;
        private MapDrawer _mapDrawer;

        private MapData _mapData;
        private CameraController _cameraController;
        
        [Inject]
        public void Construct(EditorModeService editorModeService, MapDrawer mapDrawer, 
            MapGenerator mapGenerator, CameraController cameraController)
        {
            _editorModeService = editorModeService;
            _mapDrawer = mapDrawer;
            _cameraController = cameraController;
            mapGenerator.OnMapGenerated += RegisterMap;
        }

        private void RegisterMap(MapData obj)
        {
            _mapData = obj;
        }

        [Command("map.camera.update")]
        private void UpdateCamera()
        {
            _cameraController.MoveToTheCenterOfTheMap(_mapData);
        }
        

        [Command("map.drawmap")]
        private void DrawMap()
        {
            _mapDrawer.DrawMap(_mapData);
        }

        [Command("map.cleanmap")]
        private void CLeanMap()
        {
            _mapDrawer.CleanMap();
        }
        
        [Command("map.mode.setspawnpointsmode")]
        private void SetSpawnPointsMode()
        {
            _editorModeService.EditorMode = EditorMode.SpawnPointsEdit;
        }

        [Command("map.mode.setleveleditmode")]
        private void SetLevelEditMode()
        {
            _editorModeService.EditorMode = EditorMode.LevelEdit;
        }
        
        [Command("map.mode.current")]
        private void CurrentEditMode()
        {
            Debug.Log(_editorModeService.EditorMode);
        }
    }
}