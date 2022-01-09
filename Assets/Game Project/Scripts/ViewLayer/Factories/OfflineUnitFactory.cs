using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Data;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.Factories
{
    public class OfflineUnitFactory : IUnitFactory
    {
        [Inject] private StaticData _staticData;
        [Inject] private IUnitsService _unitsService;
        [Inject] private IRoomsService _roomsService;

        private int index;
        
        public UnitView CreateUnit(GameObject unitPrefab, SpawnPointView spawnPoint)
        {
            var position = _roomsService.GetPlaceInRoom(spawnPoint.model.Room);
            var unitInstance = 
                Object.Instantiate(unitPrefab, position, new Quaternion());

            var unitView = unitInstance.GetComponent<UnitView>();
            var id = _unitsService.GetAll().Length + 1;

            var unitModel = new Unit()
            {
                UnitType = spawnPoint.model.UnitType,
                Room = spawnPoint.model.Room,
                GameObjectLink = unitInstance,
                ID = id,
                Position = position,
            };

            unitView.Construct(unitModel, _staticData);
            _unitsService.RegisterUnit(unitView.model);
            return unitView;
        }
    }
}