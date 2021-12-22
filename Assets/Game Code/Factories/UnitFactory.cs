using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Units;
using Game_Code.Network;
using Game_Code.Network.Syncs;

namespace Game_Code.Factories
{
    public class UnitFactory
    {
        private readonly NetworkPrefabFactory _networkPrefabFactory;
        private readonly INetworkUnitsSync _networkUnitsSync;

        public UnitFactory(NetworkPrefabFactory networkPrefabFactory, INetworkUnitsSync networkUnitsSync)
        {
            _networkPrefabFactory = networkPrefabFactory;
            _networkUnitsSync = networkUnitsSync;
        }

        public IUnit CreateUnit(string prefabName, SpawnPoint spawnPoint)
        {
            var unit = _networkPrefabFactory.Create(prefabName, spawnPoint).GetComponent<IUnit>();
            _networkUnitsSync.RegisterUnit(unit.UnitGameObject().name);
            return unit;
        }
    }
}