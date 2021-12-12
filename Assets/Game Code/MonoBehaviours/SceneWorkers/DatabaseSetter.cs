using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Data.Databases;
using Game_Code.MonoBehaviours.Level;
using UnityEngine;

namespace Game_Code.MonoBehaviours.SceneWorkers
{
    [ExecuteInEditMode]
    public class DatabaseSetter:MonoBehaviour
    {
        [SerializeField] private Database _database;
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
            var doors = FindObjectsOfType<Door>();
            
            var result = new Dictionary<int, Door>();
            for (var index = 0; index < doors.Length; index++)
            {
                var door = doors[index];
                result.Add(index, door);
            }

            _database.SetDoors(result);
            Debug.Log($"{doors.Count()} doors was registered to database");
        }
        
        private void DoSetRooms()
        {
            var rooms = FindObjectsOfType<Room>();
            
            var result = new Dictionary<int, Room>();
            for (var index = 0; index < rooms.Length; index++)
            {
                var door = rooms[index];
                result.Add(index, door);
            }

            _database.SetRooms(result);
            Debug.Log($"{rooms.Count()} rooms was registered to database");

        }
    }
}