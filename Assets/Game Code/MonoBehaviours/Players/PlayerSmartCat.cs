using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Units;
using Game_Code.MonoBehaviours.Units.CatUnits;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Players
{
    public class PlayerSmartCat : Player
    {
        private IEnumerable<SpawnPoint> _unitsSpawnPoints;
        private INetworkRoomsSync _networkRoomsSync;
        private IUnitRoomService _unitRoomService;
        private INetworkUnitsSync _networkUnitsSync;
        private IRoomsService _roomsService;

        [Inject]
        public void Construct(SceneData sceneData, INetworkTurnsSync networkTurnsSync,
            INetworkRoomsSync networkRoomsSync, INetworkUnitsSync networkUnitsSync, 
            IUnitRoomService unitRoomService, IRoomsService roomsService)
        {
            _networkRoomsSync = networkRoomsSync;
            _unitRoomService = unitRoomService;
            _networkUnitsSync = networkUnitsSync;
            _roomsService = roomsService;
            var controlledUnitTypes = new[] {UnitType.CatBotBiter, UnitType.CatBotBomb, UnitType.CatBotButtonPusher};
            _unitsSpawnPoints = sceneData.spawnPoints.Where(x => controlledUnitTypes.Contains(x.SpawnUnitType));
        }

        private void SelectUnit(Unit unit)
        {
            var previousUnit = SelectionService.GetPlayerSelectedUnit(this);
            if (previousUnit != null && (Unit) previousUnit != unit)
            {
                DeselectUnit(previousUnit);
            }
            
            Logger.Log($"Selecting unit: {unit.name}");
            SelectionService.SelectUnit(unit, this);
            CameraController.selectedUnit = unit.UnitGameObject();
            Logger.Log($"Done selecting unit: {unit.name}");
        }

        private void DeselectUnit(IUnit unit)
        {
            Logger.Log($"Deselecting unit: {unit.UnitGameObject().name}");
            SelectionService.DeselectUnit(unit, this);
            CameraController.selectedUnit = null;
            Logger.Log($"Done deselecting unit: {unit.UnitGameObject().name}");
        }
        
        protected override void SpawnControllableUnits()
        {
            var bots = StaticData.smartCatBots
                .ToDictionary(e => e.unitType, e => e.prefab);
            
            foreach (var spawnPoint in _unitsSpawnPoints)
            {
                if (!bots.ContainsKey(spawnPoint.SpawnUnitType)) continue;
                var unit = UnitSpawnManager.CreateUnit(bots[spawnPoint.SpawnUnitType].name, spawnPoint);

                var unitTransform = unit.UnitGameObject().transform;
                unitTransform.position = spawnPoint.SpawnPointTransform.position;
                unitTransform.parent = transform.root;

               var roomId= _roomsService.GetRoomId(spawnPoint.SpawnRoom);

               if (unit.UnitType() == UnitType.CatBotButtonPusher)
               {
                   spawnPoint.SpawnRoom.DrawRoom(true, true);
               }
               
                _networkRoomsSync.RegisterUnitToRoom(unit.UnitGameObject().name, roomId);
            }

            _networkUnitsSync.RefreshUnitsModel();
        }

        public override void MakeStep(Vector3 cursorPos)
        {
            var mousePos = Camera.ScreenToWorldPoint(cursorPos);

            var room = RaycastForComponent<Room>(mousePos, roomLayers);
            var unit = RaycastForComponent<Unit>(mousePos, unitsLayers);

            if (room)
            {
                Logger.Log($"Found room: {room.name}");
            }


            if (unit)
            {
                Logger.Log($"Found unit: {unit.name}");
                SelectUnit(unit);
            }

            var selectedUnit = SelectionService.GetPlayerSelectedUnit(this);

            if (selectedUnit != null && !room && !unit)
            {
                DeselectUnit(selectedUnit);
            }

            if (selectedUnit == null || !room || unit) return;
            
            if(!_unitRoomService.CanUnitGoToRoom(selectedUnit, room))
                return;
            
            Logger.Log($"Setting room for unit: {selectedUnit.UnitGameObject().name}");

            var unitRoom = _unitRoomService.FindUnitRoom(selectedUnit);
            var roomId= _roomsService.GetRoomId(room);
            var unitRoomId= _roomsService.GetRoomId(unitRoom);
            
            if(roomId == unitRoomId) return;

            var unitType = selectedUnit.UnitType();

            switch (unitType)
            {
                case UnitType.Engineer:
                    break;
                case UnitType.CatBotBomb:
                    if (selectedUnit is ICatBomb catBomb && catBomb.GetCatBombState() == CatBombState.NotExploding)
                    {
                        UnitGoToRoom(room);
                        OnStepMade?.Invoke();
                    }
                    break;
                case UnitType.CatBotButtonPusher:
                    unitRoom.HideRoom(false,true);
                    room.DrawRoom(true,true);
                    UnitGoToRoom(room);
                    OnStepMade?.Invoke();
                    break;
                case UnitType.CatBotBiter:
                    UnitGoToRoom(room);
                    OnStepMade?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }



        }

        private void UnitGoToRoom(Room room)
        {
            var selectedUnit = SelectionService.GetPlayerSelectedUnit(this);
            
            if(selectedUnit == null) return;
            
            var unitRoom = _unitRoomService.FindUnitRoom(selectedUnit);
            var roomId= _roomsService.GetRoomId(room);
            var unitRoomId= _roomsService.GetRoomId(unitRoom);
            
            _networkRoomsSync.RemoveUnitFromRoom(selectedUnit.UnitGameObject().name, unitRoomId);
            _networkRoomsSync.RegisterUnitToRoom(selectedUnit.UnitGameObject().name, roomId);

            unitRoom.FreePointRPC(selectedUnit.UnitGameObject().transform.position);
            var pointForUnit = room.GetPointForUnit();
            selectedUnit.SetTargetPointForUnit(pointForUnit);
        }
    }
}
