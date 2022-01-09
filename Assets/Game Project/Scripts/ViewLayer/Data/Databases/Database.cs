using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Data.Databases
{
    /*[Serializable]
    public class RoomView
    {
        public int id;
        public Entities.Level.RoomView roomView;

        public RoomView(int id, Entities.Level.RoomView roomView)
        {
            this.id = id;
            this.roomView = roomView;
        }
    }*/
    
    /*[Serializable]
    public class DoorView
    {
        public int id;
        public Entities.Level.DoorView doorView;

        public DoorView(int id, Entities.Level.DoorView doorView)
        {
            this.id = id;
            this.doorView = doorView;
        }
    }*/
    
    public class Database : MonoBehaviour
    {
        /*public Dictionary<int, Entities.Level.RoomView> Rooms => 
            roomViews.ToDictionary(roomView => roomView.id, roomView => roomView.roomView);*/

        /*public Dictionary<int, Entities.Level.DoorView> Doors =>
            doorViews.ToDictionary(doorView => doorView.id, doorView => doorView.doorView);*/

     //   [SerializeField] private RoomView[] roomViews;
   //     [SerializeField] private DoorView[] doorViews;

        /*public void SetRooms(Dictionary<int, Entities.Level.RoomView> rooms)
        {
            roomViews = rooms.Select(x=>new RoomView(x.Key, x.Value)).ToArray();
        }*/
        
        /*public void SetDoors(Dictionary<int, Entities.Level.DoorView> doors)
        {
            doorViews = doors.Select(x=>new DoorView(x.Key, x.Value)).ToArray();
        }*/
    }
}
