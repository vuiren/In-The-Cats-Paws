using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Zenject;

namespace Game_Project.Scripts.LogicLayer
{
    public class LogicDependencyInjection: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStatusService>().To<GameStatusService>().AsSingle();
            Container.Bind<IUnitsService>().To<UnitsService>().AsSingle();
            Container.Bind<IRoomsService>().To<RoomsService>().AsSingle();
            Container.Bind<ITurnService>().To<TurnService>().AsSingle();
            Container.Bind<IPlayersService>().To<PlayersService>().AsSingle();
            Container.Bind<IUnitsSelectionService>().To<UnitsSelectionService>().AsSingle();
            Container.Bind<IRoomsPointsService>().To<RoomsPointsService>().AsSingle();        
            Container.Bind<IRepairPointsService>().To<RepairPointsService>().AsSingle();        
            Container.Bind<IUnitExplosionService>().To<UnitExplosionService>().AsSingle().NonLazy();  
            Container.Bind<IUnitPositionService>().To<UnitPositionService>().AsSingle();
        }
    }
}