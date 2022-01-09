using Game_Project.Scripts.ApplicationLayer.Controllers;
using Game_Project.Scripts.ApplicationLayer.Controllers.Drawers;
using Game_Project.Scripts.ApplicationLayer.Factories;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.NetworkLayer;
using Game_Project.Scripts.NetworkLayer.Services;
using Game_Project.Scripts.ViewLayer.Data;
using Game_Project.Scripts.ViewLayer.Factories;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ApplicationLayer.Installers
{
    public class NetworkGameInstaller : MonoInstaller
    {
        [SerializeField] private SceneData sceneData;
        [SerializeField] private StaticData staticData;
        [SerializeField] private CommandsExecutor commandsExecutor;
        
        public override void InstallBindings()
        {
            LevelBindings();
            ControllersDependencyInjection.BindControllers(Container);
            OtherServices();

            Container.Bind<StaticData>().FromInstance(staticData).AsSingle();
            Container.Bind<SceneData>().FromInstance(sceneData).AsSingle();
            Container.Bind<GameManager>().FromInstance(sceneData.gameManager).NonLazy();
            Container.Bind<NetworkPrefabFactory>().ToSelf().AsSingle();
            Container.Bind<IUnitFactory>().To<NetworkUnitFactory>().AsSingle(); 
            Container.Bind<CommandsExecutor>().FromInstance(commandsExecutor).AsSingle().NonLazy();
        }

        private void OtherServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IUnitsSelectionService>().To<UnitsSelectionService>().AsSingle();

            Container.Bind<IGameStatusService>().To<GameStatusService>().AsSingle();
            Container.Bind<ICurrentPlayerService>().To<NetworkCurrentPlayerService>().AsSingle();
            Container.Bind<IPlayersService>().FromInstance(sceneData.playersService).AsSingle();
            Container.Bind<IUnitsService>().FromInstance(sceneData.unitsService).AsSingle();
            Container.Bind<IUnitExplosionService>().FromInstance(sceneData.networkUnitExplosionService).AsSingle();
            Container.Bind<ITurnService>().FromInstance(sceneData.turnsService).AsSingle();
            Container.Bind<IWinService>().FromInstance(sceneData.networkWinService).AsSingle();
        }

        private void LevelBindings()
        {
            Container.Bind<IRoomsService>().FromInstance(sceneData.networkRoomsService).AsSingle();
            Container.Bind<ICorridorsService>().FromInstance(sceneData.corridorsService).AsSingle();
            Container.Bind<IButtonsService>().To<ButtonsService>().AsSingle();
            Container.Bind<IRepairPointsService>().FromInstance(sceneData.repairPointsService).AsSingle();
            Container.Bind<IExitPointsService>().To<ExitPointsService>().AsSingle();
            Container.Bind<ISpawnPointsService>().To<SpawnPointsService>().AsSingle();
        }
    }
}