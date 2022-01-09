using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.ViewLayer.Data.Databases;
using UnityEngine;
using RoomView = Game_Project.Scripts.ViewLayer.Entities.Level.RoomView;

namespace Game_Project.Scripts.ViewLayer.SceneWorkers
{
    [ExecuteInEditMode]
    public class DatabaseSetter:MonoBehaviour
    {
        [SerializeField] private Database database;
        [SerializeField] private bool setDoors, setRooms;

        private void Update()
        {
            if (setDoors)
            {
                DoSetDoors();
                setDoors = false;
            }

            if (setRooms)
            {
                DoSetRooms();
                setRooms = false;
            }
        }

        private void DoSetDoors()
        {
            /*var doors = FindObjectsOfType<DoorView>();
            
            var result = new Dictionary<int, DoorView>();
            for (var index = 0; index < doors.Length; index++)
            {
                var door = doors[index];
                result.Add(index, door);
            }

            database.SetDoors(result);
            Debug.Log($"{doors.Count()} doors was registered to database");*/
        }
        
        private void DoSetRooms()
        {
            var rooms = FindObjectsOfType<RoomView>();
            
            var result = new Dictionary<int, RoomView>();
            for (var index = 0; index < rooms.Length; index++)
            {
                var door = rooms[index];
                result.Add(index, door);
            }

         //   database.SetRooms(result);
            Debug.Log($"{rooms.Count()} rooms was registered to database");

        }
    }
}