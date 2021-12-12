using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Level;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Data.Databases
{
    [Serializable]
    public class RoomView
    {
        public int id;
        public Room room;

        public RoomView(int id, Room room)
        {
            this.id = id;
            this.room = room;
        }
    }
    
    [Serializable]
    public class DoorView
    {
        public int id;
        public Door door;

        public DoorView(int id, Door door)
        {
            this.id = id;
            this.door = door;
        }
    }
    
    public class Database : MonoBehaviour
    {
        public Dictionary<int, Room> Rooms => 
            roomViews.ToDictionary(roomView => roomView.id, roomView => roomView.room);

        public Dictionary<int, Door> Doors =>
            doorViews.ToDictionary(doorView => doorView.id, doorView => doorView.door);

        [SerializeField] private RoomView[] roomViews;
        [SerializeField] private DoorView[] doorViews;

        public void SetRooms(Dictionary<int, Room> rooms)
        {
            roomViews = rooms.Select(x=>new RoomView(x.Key, x.Value)).ToArray();
        }
        
        public void SetDoors(Dictionary<int, Door> doors)
        {
            doorViews = doors.Select(x=>new DoorView(x.Key, x.Value)).ToArray();
        }
    }
}
