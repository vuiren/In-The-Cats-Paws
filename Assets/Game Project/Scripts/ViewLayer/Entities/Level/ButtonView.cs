using System;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
    public class ButtonView : Entity<Button>
    {
        [SerializeField] private Vector2Int buttonRoom, room1, room2;
        [SerializeField] private UnityEngine.UI.Button _button;
        [SerializeField] private GameObject pressButton;

        public Action<ButtonView> OnButtonPressed { get; set; }


        private void Awake()
        {
            _button.onClick.AddListener(() => OnButtonPressed?.Invoke(this));
        }
        
        protected override void SetModel()
        {
            model.ButtonRoom = buttonRoom;
            model.Room1 = room1;
            model.Room2 = room2;
        }
        
        public void DrawButton(bool draw)
        {
            pressButton.SetActive(draw);
        }
    }
}