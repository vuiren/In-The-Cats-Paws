using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Players;
using Game_Code.MonoBehaviours.Units;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.DebugViewer
{
    [Serializable]
    public class RoomViewer
    {
        public bool enabled;
        public Room room;
        public List<Unit> units;

        public RoomViewer(bool enabled, Room room, List<Unit> units)
        {
            this.enabled = enabled;
            this.room = room;
            this.units = units;
        }

        public override string ToString()
        {
            return room.gameObject.name + " Units Count: " + units.Count();
        }
    }

    public class Viewer : MonoBehaviour
    {
        [SerializeField] private bool refresh;

        [SerializeField] private Unit[] units;
        [SerializeField] private Room[] rooms;
        [SerializeField] private Player[] players;
        [SerializeField] private List<RoomViewer> unitRoomViewers;

        private IUnitsService _unitsService;
        private IRoomsService _roomsService;
        private IPlayersService _playersService;
        private IUnitRoomService _unitRoomService;


        [Inject]
        public void Construct(IUnitsService unitsService, IRoomsService roomsService,
            IPlayersService playersService, IUnitRoomService unitRoomService)
        {
            _unitsService = unitsService;
            _roomsService = roomsService;
            _playersService = playersService;
            _unitRoomService = unitRoomService;
        }

        private void Update()
        {
            if (!refresh) return;
            Refresh();
            refresh = false;
        }

        private void Refresh()
        {
            unitRoomViewers = new List<RoomViewer>();

            units = _unitsService.GetAll();
            rooms = _roomsService.GetAll();
            players = _playersService.GetAll();

            foreach (var room in rooms)
            {
                var roomId = _roomsService.GetRoomId(room);
                var allUnitsInRoom = _unitRoomService.GetAllUnitsInRoom(roomId);
                unitRoomViewers.Add(new RoomViewer(room.roomEnabled, room, allUnitsInRoom));
            }
        }
    }
}