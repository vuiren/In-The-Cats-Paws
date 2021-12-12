using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Units;
using Game_Code.Network;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Players
{
    public class PlayerSmartCat : Player
    {
        [SerializeField] private List<Unit> units = new List<Unit>();

        private IEnumerable<SpawnPoint> _unitsSpawnPoints;
        private INetworkTurnsSync _networkTurnsSync;
        private INetworkRoomsSync _networkRoomsSync;
        private IUnitRoomService _unitRoomService;
        private INetworkUnitsSync _networkUnitsSync;
        private IRoomsService _roomsService;


        [Inject]
        public void Construct(SceneData sceneData, INetworkTurnsSync networkTurnsSync,
            INetworkRoomsSync networkRoomsSync, INetworkUnitsSync networkUnitsSync, 
            IUnitRoomService unitRoomService, IRoomsService roomsService)
        {
            _networkTurnsSync = networkTurnsSync;
            _networkRoomsSync = networkRoomsSync;
            _unitRoomService = unitRoomService;
            _networkUnitsSync = networkUnitsSync;
            _roomsService = roomsService;
            var controlledUnitTypes = new[] {UnitType.CatBotBiter, UnitType.CatBotBomb, UnitType.CatBotButtonPusher};
            _unitsSpawnPoints = sceneData.spawnPoints.Where(x => controlledUnitTypes.Contains(x.SpawnUnitType));
        }

        private void SelectUnit(Unit unit)
        {
            Logger.Log($"Selecting unit: {unit.name}");
            ControlledUnit = unit;
            ControlledUnit.ChooseUnit();
            CameraController.selectedUnit = ControlledUnit;
        }

        private void DeselectCurrentUnit()
        {
            if (!ControlledUnit) return;
            Logger.Log($"Deselecting unit: {ControlledUnit.name}");
            ControlledUnit.UnchooseUnit();
            CameraController.selectedUnit = null;
            ControlledUnit = null;
        }

        private Dictionary<UnitType, GameObject> BotListToDictionary(IEnumerable<UnitPrefab> smartCatBots)
        {
            return smartCatBots.ToDictionary(e => e.unitType, e => e.prefab);
        }

        protected override void SpawnControllableUnits()
        {
            var bots = BotListToDictionary(StaticData.smartCatBots);
            foreach (var spawnPoint in _unitsSpawnPoints)
            {
                if (!bots.ContainsKey(spawnPoint.SpawnUnitType)) continue;
                var unit = UnitSpawnManager.CreateUnit(bots[spawnPoint.SpawnUnitType].name, spawnPoint);

                var unitTransform = unit.transform;
                unitTransform.position = spawnPoint.SpawnPointTransform.position;
                unitTransform.parent = transform.root;

               var roomId= _roomsService.GetRoomId(spawnPoint.SpawnRoom);
                _networkRoomsSync.RegisterUnitToRoom(unit.gameObject.name, roomId);
                //spawnPoint.SpawnRoom.EnableRoom();
                units.Add(unit);
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
            }

            if (unit)
            {
                DeselectCurrentUnit();
                SelectUnit(unit);
            }

            if (ControlledUnit && !room && !unit)
            {
                DeselectCurrentUnit();
            }

            if (!ControlledUnit || !room || unit) return;
            
            if(!_unitRoomService.CanUnitGoToRoom(ControlledUnit, room))
                return;
            
            Logger.Log($"Setting room for unit: {ControlledUnit.name}");

            var unitRoom = _unitRoomService.FindUnitRoom(ControlledUnit);
            var roomId= _roomsService.GetRoomId(room);
            var unitRoomId= _roomsService.GetRoomId(unitRoom);
            
            if(roomId == unitRoomId) return;

            _networkRoomsSync.RemoveUnitFromRoom(ControlledUnit.gameObject.name, unitRoomId);
            _networkRoomsSync.RegisterUnitToRoom(ControlledUnit.gameObject.name, roomId);

            ControlledUnit.RefreshTargetPos();
            OnStepMade?.Invoke();

        }
    }
}
