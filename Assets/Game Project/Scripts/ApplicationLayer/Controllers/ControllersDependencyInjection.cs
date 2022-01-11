using Game_Project.Scripts.ApplicationLayer.Controllers.Drawers;
using Game_Project.Scripts.ApplicationLayer.Controllers.UnitControllers;
using Zenject;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public static class ControllersDependencyInjection
    {
        public static void BindControllers(DiContainer container)
        {
            container.Bind<UnitMovingController>().ToSelf().AsSingle();
            container.Bind<EngineerRepairPointFixer>().ToSelf().AsSingle();
            container.Bind<UnitsExplosionController>().ToSelf().AsSingle();
            container.Bind<EffectsSpawner>().ToSelf().AsSingle();
            container.Bind<InputWorker>().ToSelf().AsSingle().NonLazy();
            container.Bind<SelectedUnitAnimator>().ToSelf().AsSingle().NonLazy();
            container.Bind<ButtonsController>().ToSelf().AsSingle().NonLazy();
            container.Bind<TurnUIController>().ToSelf().AsSingle().NonLazy();
            container.Bind<EngineerWinWatcher>().ToSelf().AsSingle().NonLazy();
            container.Bind<CatWinWatcher>().ToSelf().AsSingle().NonLazy();
            container.Bind<RepairPointsController>().ToSelf().AsSingle().NonLazy();
            container.Bind<CatBombController>().ToSelf().AsSingle().NonLazy();
            container.Bind<CatBiteController>().ToSelf().AsSingle().NonLazy();

            BindDrawers(container);
        }   
        
        private static void BindDrawers(DiContainer container)
        {
            container.Bind<CatMapDrawer>().ToSelf().AsSingle();
            container.Bind<EngineerMapDrawer>().ToSelf().AsSingle();
            container.Bind<IMapDrawer>().To<MapDrawer>().AsSingle().NonLazy();
            container.Bind<SelectedUnitDrawer>().ToSelf().AsSingle().NonLazy();
            container.Bind<ExplodingUnitAnimator>().ToSelf().AsSingle().NonLazy();
        }
    }
}