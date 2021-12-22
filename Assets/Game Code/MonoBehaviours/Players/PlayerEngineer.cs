using System.ComponentModel.Design;
using System.Linq;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Units;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Players
{
    public class PlayerEngineer : Player
    {
        private SpawnPoint _engineerSpawnPoint;
        private INetworkRoomsSync _networkRoomsSync;
        private INetworkUnitsSync _networkUnitsSync;
        private IUnitRoomService _unitRoomService;
        private IRoomsService _roomsService;
        private IUnitsSelectionService _selectionService;
        
        [Inject]
        public void Construct(SceneData sceneData, INetworkRoomsSync networkRoomsSync, 
            INetworkUnitsSync networkUnitsSync, IUnitRoomService unitRoomService, IRoomsService roomsService,
            IUnitsSelectionService selectionService)
        {
            _unitRoomService = unitRoomService;
            _engineerSpawnPoint = sceneData.spawnPoints.Single(x => x.SpawnUnitType == UnitType.Engineer);
            _networkRoomsSync = networkRoomsSync;
            _networkUnitsSync = networkUnitsSync;
            _roomsService = roomsService;
            _selectionService = selectionService;
        }

        public override void MakeStep(Vector3 cursorPos)
        {
            var mousePos = Camera.ScreenToWorldPoint(cursorPos);

            var room = RaycastForComponent<Room>(mousePos, roomLayers);
            if (!room) return;

            var selectedUnit = _selectionService.GetPlayerSelectedUnit(this);
            
            if(selectedUnit == null) return;
            
            var roomId = _roomsService.GetRoomId(room); 
            var unitRoom = _unitRoomService.FindUnitRoom(selectedUnit);
            var unitRoomId = _roomsService.GetRoomId(unitRoom);

            if(roomId == unitRoomId) return;
            if(!_unitRoomService.CanUnitGoToRoom(selectedUnit, room))
                return;
            
            _networkRoomsSync.RemoveUnitFromRoom(selectedUnit.UnitGameObject().name, unitRoomId);
            _networkRoomsSync.RegisterUnitToRoom(selectedUnit.UnitGameObject().name, roomId);
            
            unitRoom.FreePointRPC(selectedUnit.UnitGameObject().transform.position);
            var pointForUnit = room.GetPointForUnit();
            selectedUnit.SetTargetPointForUnit(pointForUnit);

            unitRoom.HideRoom(true,true);
            room.DrawRoom(true,true);
            CameraController.selectedRoom = room;
            OnStepMade?.Invoke();
        }

        protected override void SpawnControllableUnits()
        {
            var spawnPoint = _engineerSpawnPoint;
            var unit = UnitSpawnManager.CreateUnit(StaticData.engineerCharacterPrefab.prefab.name, spawnPoint);
    
            var unitTransform = unit.UnitGameObject().transform;
            unitTransform.position = spawnPoint.SpawnPointTransform.position;
            unitTransform.parent = transform.root;

            var roomId = _roomsService.GetRoomId(spawnPoint.SpawnRoom);
            _networkRoomsSync.RegisterUnitToRoom(unit.UnitGameObject().name, roomId);
            _networkUnitsSync.RefreshUnitsModel();
            spawnPoint.SpawnRoom.DrawRoom(true,true);

            Logger.Log($"{this.name} created the {unit.UnitGameObject().name} ");
            _selectionService.SelectUnit(unit, this);
        }

        protected override void Start()
        {
            base.Start();

            if (PhotonView.IsMine)
            {
                CameraController.selectedUnit = _selectionService.GetPlayerSelectedUnit(this).UnitGameObject();
            }
        }
    }
}
