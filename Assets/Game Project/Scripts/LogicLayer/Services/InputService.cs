using System;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class InputService : IInputService
    {
        private Action<Vector3> onClick;

        public void OnPlayerClick(Action<Vector3> action)
        {
            onClick += action;
        }

        public void PlayerClick(Vector3 pointerWorldPosition)
        {
            onClick?.Invoke(pointerWorldPosition);
        }
    }
}