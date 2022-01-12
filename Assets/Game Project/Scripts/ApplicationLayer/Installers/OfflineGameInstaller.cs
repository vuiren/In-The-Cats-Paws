using Game_Project.Scripts.ApplicationLayer.Controllers;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.ViewLayer.Data;
using Game_Project.Scripts.ViewLayer.Factories;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ApplicationLayer.Installers
{
    public sealed class OfflineGameInstaller : MonoInstaller
    {
        [SerializeField] private SceneData sceneData;
        [SerializeField] private StaticData staticData;
        [SerializeField] private CommandsExecutor commandsExecutor;

        public override void InstallBindings()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Container.BindInstance(sceneData.touchInput).AsSingle();
            }
            else
            {
                Container.BindInstance(sceneData.mouseInput).AsSingle();
            }
            
            LevelBindings();
            ControllersDependencyInjection.BindControllers(Container);
            OtherServices();

            Container.BindInstance(staticData).AsSingle();
            Container.BindInstance(sceneData).AsSingle();
            Container.BindInstance(sceneData.gameManager).AsSingle().NonLazy();
            Container.BindInstance(commandsExecutor).AsSingle().NonLazy();
            Container.BindInstance(sceneData.cameraController).AsSingle().NonLazy();
            Container.Bind<AudioSource>().FromInstance(sceneData.backgroundMusic).AsSingle().NonLazy();
            Container.Bind<IUnitFactory>().To<OfflineUnitFactory>().AsSingle();
            Container.Bind<IWinService>().To<WinService>().AsSingle().NonLazy();
        }
        

        private void OtherServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();

            Container.Bind<IGameStatusService>().To<GameStatusService>().AsSingle();
            Container.Bind<ICurrentPlayerService>().To<CurrentPlayerService>().AsSingle();
            Container.Bind<IPlayersService>().To<PlayersService>().AsSingle();
            Container.Bind<IUnitsService>().To<UnitsService>().AsSingle();
            Container.Bind<IUnitExplosionService>().To<UnitExplosionService>().AsSingle();
            Container.Bind<ITurnService>().To<TurnService>().AsSingle();
            Container.Bind<IUnitsSelectionService>().To<UnitsSelectionService>().AsSingle();
        }

        private void LevelBindings()
        {
            Container.Bind<IRoomsService>().To<RoomsService>().AsSingle();
            Container.Bind<ICorridorsService>().To<CorridorsService>().AsSingle();
            Container.Bind<IButtonsService>().To<ButtonsService>().AsSingle();
            Container.Bind<IRepairPointsService>().To<RepairPointsService>().AsSingle();
            Container.Bind<IExitPointsService>().To<ExitPointsService>().AsSingle();
            Container.Bind<ISpawnPointsService>().To<SpawnPointsService>().AsSingle();
        }
    }
}