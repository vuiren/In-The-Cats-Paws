using Map_Generator_Project.Scripts;
using Map_Generator_Project.Scripts.Data;
using Map_Generator_Project.Scripts.Factories;
using UnityEngine;
using Zenject;

public class MapCreatorInstaller : MonoInstaller
{
    [SerializeField] private MapGeneratorSceneData _sceneData;
    [SerializeField] private MapCreatorStaticData _staticData;
    [SerializeField] private MapData _mapData;
    [SerializeField] private Pattern _pattern;
    
    public override void InstallBindings()
    {
        Container.Bind<MapCreatorStaticData>().FromInstance(_staticData);
        Container.Bind<CellFactory>().ToSelf().AsSingle();
        Container.Bind<Pattern>().FromInstance(_pattern).AsSingle();
        Container.Bind<MapGeneratorSceneData>().FromInstance(_sceneData).AsSingle();
        Container.Bind<CameraController>().FromInstance(_sceneData.CameraController).AsSingle();
        Container.Bind<MapGenerator>().FromInstance(_sceneData.mapGenerator).AsSingle();
        Container.Bind<EditorModeService>().ToSelf().AsSingle();
        Container.Bind<MapData>().FromInstance(_mapData).AsSingle().NonLazy();
        Container.Bind<MapDrawer>().ToSelf().AsSingle();
    }
}