using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    internal sealed class RoomsPointsService: IRoomsPointsService
    {
        /*private IRoomsService _roomsService;
        private readonly Dictionary<Room, Queue<Vector3>> _pointsInRooms;

        public RoomsPointsService(IRoomsService roomsService)
        {
            _pointsInRooms = roomsService
                .GetAll()
                .ToDictionary(x => x, x => new Queue<Vector3>(x.GetFreePoints()));
        }

        public Vector3 GetFreePointFromRoom(Room room)
        {
            if (_pointsInRooms.ContainsKey(room) && _pointsInRooms[room].Count > 0)
            {
                return _pointsInRooms[room].Dequeue();
            }

            throw new Exception($"No free points in room {room.GameObjectLink().name}");
        }

        public void AddFreePointToRoom(Room room, Vector3 point)
        {
            _pointsInRooms[room].Enqueue(point);
        }*/
    }
}