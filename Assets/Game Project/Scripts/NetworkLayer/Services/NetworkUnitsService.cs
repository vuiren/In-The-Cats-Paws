using System;
using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.NetworkLayer.Base;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkUnitsService : NetworkService, IUnitsService
    {
        private readonly Dictionary<int, Unit> _units = new();
        private IMyLogger _logger;
        [Inject] private IRoomsService _roomsService;
        private Action<Unit> _onUnitRegistered;

        [Inject]
        protected override void Construct()
        {
            base.Construct();
            _logger = LoggerFactory.Create(this);
        }

        public Unit[] GetAll()
        {
            return _units.Select(x => x.Value).ToArray();
        }

        public Unit GetUnitByID(int id)
        {
            if (_units.ContainsKey(id))
            {
                return _units[id];
            }
            else
            {
                throw new Exception("Unit with id {id} is not exist");
            }
        }

        IEnumerable<Unit> IUnitsService.GetUnitsByUnitType(UnitType unitType)
        {
            return _units.Select(x => x.Value).Where(x => x.UnitType == unitType);
        }

        public void RegisterOnUnitRegistration(Action<Unit> action)
        {
            _onUnitRegistered += action;
        }

        public void RegisterUnit(Unit unit)
        {
            PhotonView.RPC("RegisterUnitRPC", RpcTarget.All,
                unit.GameObjectLink.name, unit.ID, unit.Room.x, unit.Room.y, unit.Position, unit.UnitType);
        }

        public void UnitGoToRoom(int unitId, Vector2Int room)
        {
            PhotonView.RPC("UnitGoToRoomRPC", RpcTarget.All, unitId, room.x, room.y);
        }
        
        [PunRPC]
        private void RegisterUnitRPC(string gameObjectName, int unitId, int roomX, int roomY, Vector3 position, UnitType unitType)
        {
            var gameObjectLink = GameObject.Find(gameObjectName);
            
            
            var unit = new Unit()
            {
                ID = unitId,
                Room = new Vector2Int(roomX, roomY),
                Position = position,
                UnitType = unitType,
                GameObjectLink = gameObjectLink,
            };
            
            var id = unit.ID;
            
            _logger.Log($"Registering unit {unitType} with id {id}");

            if (_units.ContainsKey(id))
            {
                _logger.LogWarning($"{unitType} with id {id} already exist");
            }
            else
            {
                _units[id] = unit;
                _onUnitRegistered?.Invoke(unit);
                _logger.Log($"Done registering unit {unitType}");
            }
        }

        [PunRPC]
        private void UnitGoToRoomRPC(int unitId, int x, int y)
        {
            var roomCoords = new Vector2Int(x, y);
            _units[unitId].Room = roomCoords;

            var room = _roomsService.GetRoomByCoord(roomCoords);
            _units[unitId].Position = room.GameObjectLink.transform.position;
        }
    }
}