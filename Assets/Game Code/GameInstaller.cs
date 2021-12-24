using Game_Code.Controllers;
using Game_Code.Controllers.CatBotControllers;
using Game_Code.Factories;
using Game_Code.Managers;
using Game_Code.MonoBehaviours;
using Game_Code.MonoBehaviours.Data;
using Game_Code.Network;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneData sceneData;
        [SerializeField] private StaticData staticData;

        public override void InstallBindings()
        {
            Container.Bind<ILogger>().To<Logger>().AsSingle();

            BindControllers();
            BindPrimitiveSingle();
            BindAllFromInstance();
            BindNetworkWorkers();
            BindServices();
        }

        private void BindControllers()
        {
            Container.Bind<ICatBotExplosionController>().To<CatBotExplosionController>().AsSingle().NonLazy();
            Container.Bind<CatBotRevivalController>().ToSelf().AsSingle().NonLazy();
            Container.Bind<UnitsPainterController>().ToSelf().AsSingle().NonLazy();
            Container.Bind<CatBombUIController>().ToSelf().AsSingle().NonLazy();
            Container.Bind<CatBotExplodingAnimator>().ToSelf().AsSingle().NonLazy();
            Container.Bind<RepairPointDrawer>().ToSelf().AsSingle().NonLazy();
            Container.Bind<EngineerWinWatcher>().ToSelf().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<IGameStatusService>().To<GameStatusService>().AsSingle();
            Container.Bind<IUnitsService>().To<UnitsService>().AsSingle();
            Container.Bind<IRoomsService>().To<RoomsService>().AsSingle();
            Container.Bind<ITurnService>().To<TurnService>().AsSingle();
            Container.Bind<IPlayersService>().To<PlayersService>().AsSingle();
            Container.Bind<IUnitRoomService>().To<UnitRoomService>().AsSingle();        
            Container.Bind<IDoorsService>().To<DoorsService>().AsSingle();
            Container.Bind<IUnitsSelectionService>().To<UnitsSelectionService>().AsSingle();
            Container.Bind<IRoomsVisibilityService>().To<RoomsVisibilityService>().AsSingle();        
            Container.Bind<IRoomsPointsService>().To<RoomsPointsService>().AsSingle();        
            Container.Bind<IRepairPointsService>().To<RepairPointsService>().AsSingle();        
            Container.Bind<IExplosionService>().To<ExplosionService>().AsSingle().NonLazy();        
        }

        private void BindNetworkWorkers()
        {
            Container.Bind<NetworkPrefabFactory>().AsSingle();
            Container.Bind<INetworkPlayersSync>().FromInstance(sceneData.networkPlayersSync);
            Container.Bind<INetworkGameSync>().FromInstance(sceneData.networkGameSync);
            Container.Bind<INetworkRoomsSync>().FromInstance(sceneData.networkRoomsSync);
            Container.Bind<INetworkTurnsSync>().FromInstance(sceneData.networkTurnsSync);
            Container.Bind<INetworkUnitsSync>().FromInstance(sceneData.networkUnitsSync);
            Container.Bind<INetworkDoorsSync>().FromInstance(sceneData.networkDoorsSync);
            Container.Bind<INetworkFreeRoomPointsSync>().FromInstance(sceneData.networkFreeRoomPointsSync);
            Container.Bind<INetworkCatBombExplosionSync>().FromInstance(sceneData.networkCatBombExplosionSync);
            Container.Bind<INetworkRepairSync>().FromInstance(sceneData.repairSync);
        }

        private void BindPrimitiveSingle()
        {
            Container.Bind<CurrentPlayerManager>().AsSingle().NonLazy();
            Container.Bind<UnitFactory>().AsSingle();
            Container.Bind<PlayerFactory>().AsSingle();
            Container.Bind<PlayersWorker>().AsSingle().NonLazy();
            Container.Bind<PlayerEngineerVisibilityManager>().AsSingle().NonLazy();
        }

        private void BindAllFromInstance()
        {
            Container.Bind<SceneData>().FromInstance(sceneData);
            Container.Bind<StaticData>().FromInstance(staticData);
            Container.Bind<CameraController>().FromInstance(sceneData.cameraController);
            Container.Bind<GameManager>().FromInstance(sceneData.gameManager);
        }
    }
}