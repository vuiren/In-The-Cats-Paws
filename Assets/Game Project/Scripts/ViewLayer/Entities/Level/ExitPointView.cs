using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
    public class ExitPointView:Entity<ExitPoint>
    {
        [SerializeField] private Vector2Int room;
        
        protected override void SetModel()
        {
            model = new ExitPoint
            {
                Room = room
            };
        }
    }
}