using System;
using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.DebugViewer
{
    [Serializable]
    public class RoomViewer
    {
        public bool enabled;
        public RoomView roomView;
        public List<UnitView> Units;

        public RoomViewer(bool enabled, RoomView roomView, List<UnitView> units)
        {
            this.enabled = enabled;
            this.roomView = roomView;
            this.Units = units;
        }

        public override string ToString()
        {
            return roomView.gameObject.name + " Units Count: " + Units.Count();
        }
    }

    public class Viewer : MonoBehaviour
    {
        /*[SerializeField] private bool refresh;

        [SerializeField] private UnitView[] _units;
        [SerializeField] private RoomView[] rooms;
        [SerializeField] private PlayerView[] players;
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
        
        [Command("view.refresh")]
        private void Refresh()
        {
            unitRoomViewers = new List<RoomViewer>();

         //  . _units = _unitsService.GetAll();
            rooms = _roomsService.GetAll();
            players = _playersService.GetAll();

            foreach (var room in rooms)
            {
                var roomId = _roomsService.GetRoomId(room);
                var allUnitsInRoom = _unitRoomService.GetAllUnitsInRoom(roomId);
             //   unitRoomViewers.Add(new RoomViewer(room.roomEnabled, room, allUnitsInRoom));
            }
        }*/
    }
}