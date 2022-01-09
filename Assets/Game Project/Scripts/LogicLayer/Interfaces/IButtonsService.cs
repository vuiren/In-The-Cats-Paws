using Game_Project.Scripts.DataLayer.Level;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IButtonsService
    {
        Button[] GetAll();
        void RegisterButton(Button button);
        Button GetButton(Vector2Int buttonRoom, Vector2Int room1, Vector2Int room2);
    }
}