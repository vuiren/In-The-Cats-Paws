using Game_Project.Scripts.DataLayer.Level;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IRepairPointsService
    {
        void RegisterRepairPoint(RepairPoint repairPoint);
        RepairPoint GetRepaintPointInRoom(Vector2Int room);
        RepairPoint[] GetAll();
        void FixRepairPoint(RepairPoint repairPoint);
    }
}