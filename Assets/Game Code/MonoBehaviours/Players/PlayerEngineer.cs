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
        
        [Inject]
        public void Construct(SceneData sceneData, INetworkRoomsSync networkRoomsSync, 
            INetworkUnitsSync networkUnitsSync, IUnitRoomService unitRoomService, IRoomsService roomsService)
        {
            _unitRoomService = unitRoomService;
            _engineerSpawnPoint = sceneData.spawnPoints.Single(x => x.SpawnUnitType == UnitType.Engineer);
            _networkRoomsSync = networkRoomsSync;
            _networkUnitsSync = networkUnitsSync;
            _roomsService = roomsService;
        }

        public override void MakeStep(Vector3 cursorPos)
        {
            var mousePos = Camera.ScreenToWorldPoint(cursorPos);

            var room = RaycastForComponent<Room>(mousePos, roomLayers);
            if (!room) return;

            var roomId = _roomsService.GetRoomId(room); 
            var unitRoom = _unitRoomService.FindUnitRoom(ControlledUnit);
            var unitRoomId = _roomsService.GetRoomId(unitRoom);

            if(roomId == unitRoomId) return;
            if(!_unitRoomService.CanUnitGoToRoom(ControlledUnit, room))
                return;
            
            _networkRoomsSync.RemoveUnitFromRoom(ControlledUnit.gameObject.name, unitRoomId);
            _networkRoomsSync.RegisterUnitToRoom(ControlledUnit.gameObject.name, roomId);
            ControlledUnit.RefreshTargetPos();

            unitRoom.DisableRoom();
            room.EnableRoom();
            CameraController.selectedRoom = room;
            OnStepMade?.Invoke();
        }

        protected override void SpawnControllableUnits()
        {
            var spawnPoint = _engineerSpawnPoint;
            ControlledUnit = UnitSpawnManager.CreateUnit(StaticData.engineerCharacterPrefab.prefab.name, spawnPoint);

            var controlledUnitTransform = ControlledUnit.transform;
            controlledUnitTransform.position = spawnPoint.SpawnPointTransform.position;
            controlledUnitTransform.parent = transform.root;

            var roomId = _roomsService.GetRoomId(spawnPoint.SpawnRoom);
            _networkRoomsSync.RegisterUnitToRoom(ControlledUnit.gameObject.name, roomId);
            _networkUnitsSync.RefreshUnitsModel();
            spawnPoint.SpawnRoom.EnableRoom();

            Logger.Log($"{this.name} created the {ControlledUnit.name} ");
            ControlledUnit.ChooseUnit();
        }

        protected override void Start()
        {
            base.Start();

            if (PhotonView.IsMine)
                CameraController.selectedUnit = ControlledUnit;
        }
    }
}
