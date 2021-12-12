using System.Linq;
using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Players;
using Player = Game_Code.MonoBehaviours.Players.Player;

namespace Game_Code.Services
{
    public interface IRoomsVisibilityService
    {
        bool CanPlayerSeeRoom(Player player, Room room);
        void HideAllRooms();
        void ShowAllRooms();
    }

    public class RoomsVisibilityService : IRoomsVisibilityService
    {
        private readonly IUnitRoomService _unitRoomService;
        private readonly IRoomsService _roomsService;

        public RoomsVisibilityService(IUnitRoomService unitRoomService, IRoomsService roomsService)
        {
            _unitRoomService = unitRoomService;
            _roomsService = roomsService;
        }

        public bool CanPlayerSeeRoom(Player player, Room room)
        {
            switch (player)
            {
                case PlayerSmartCat:
                    return true;
                case PlayerEngineer:
                {
                    var units = _unitRoomService.GetAllUnitsInRoom(room);
                    return units.Any();
                }
                default:
                    return false;
            }
        }

        public void HideAllRooms()
        {
            var rooms = _roomsService.GetAll();
            foreach (var room in rooms)
            {
                room.DisableRoom();
            }
        }

        public void ShowAllRooms()
        {
            var rooms = _roomsService.GetAll();
            foreach (var room in rooms)
            {
                room.EnableRoom();
            }
        }
    }
}