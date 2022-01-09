using System;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IInputService
    {
        void OnPlayerClick(Action<Vector3> action);
        void PlayerClick(Vector3 pointerWorldPosition);
    }
}