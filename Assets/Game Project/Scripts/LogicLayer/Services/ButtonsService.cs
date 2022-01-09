using System;
using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class ButtonsService : IButtonsService
    {
        private readonly Dictionary<Tuple<Vector2Int, Vector2Int, Vector2Int>, Button> _buttons = new();
        private readonly IMyLogger _logger;

        [Inject]
        public ButtonsService()
        {
            _logger = LoggerFactory.Create(this);
        }

        public Button[] GetAll()
        {
            return _buttons.Values.ToArray();
        }

        public void RegisterButton(Button button)
        {
            var key = new Tuple<Vector2Int, Vector2Int, Vector2Int>(button.ButtonRoom, button.Room1, button.Room2);

            if (_buttons.ContainsKey(key))
            {
                _logger.LogWarning($"Button: {button.ToString()} already registered");
            }
            else
            {
                _buttons.Add(key, button);
            }
        }

        public Button GetButton(Vector2Int buttonRoom, Vector2Int room1, Vector2Int room2)
        {
            var key = new Tuple<Vector2Int, Vector2Int, Vector2Int>(buttonRoom, room1, room2);

            if (_buttons.ContainsKey(key))
            {
                return _buttons[key];
            }
            else
            {
                _logger.LogWarning($"Button not found");
                return null;
            }
        }
    }
}