using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.NetworkLayer;
using Game_Project.Scripts.ViewLayer.Data;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using Game_Project.Scripts.ViewLayer.Factories;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ApplicationLayer.Factories
{
    public class NetworkUnitFactory : IUnitFactory
    {
        private readonly NetworkPrefabFactory _prefabFactory;
        private readonly StaticData _staticData;
        private readonly IUnitsService _unitsService;
        private readonly IRoomsService _roomsService;

        [Inject]
        public NetworkUnitFactory(NetworkPrefabFactory prefabFactory, StaticData staticData, IUnitsService unitsService,
            IRoomsService roomsService)
        {
            _prefabFactory = prefabFactory;
            _staticData = staticData;
            _unitsService = unitsService;
            _roomsService = roomsService;
        }

        public UnitView CreateUnit(GameObject unitPrefab, SpawnPointView spawnPoint)
        {
            var position = spawnPoint.SpawnPointTransform.position;
;
            var unitInstance =
                _prefabFactory.Create(unitPrefab.name, position, false);

            var unitView = unitInstance.GetComponent<UnitView>();
            var id = spawnPoint.SpawnID;

            var unitModel = new Unit()
            {
                UnitType = spawnPoint.model.UnitType,
                Room = spawnPoint.model.Room,
                GameObjectLink = unitInstance,
                ID = id,
                Position = position,
            };
            _unitsService.RegisterUnit(unitModel);
            _unitsService.UnitGoToRoom(unitModel.ID, spawnPoint.model.Room);
            unitView.Construct(unitModel, _staticData);
            return unitView;
        }
    }
}