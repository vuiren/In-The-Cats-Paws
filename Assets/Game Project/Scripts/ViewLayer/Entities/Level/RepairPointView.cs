using System;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
    public class RepairPointView : Entity<RepairPoint>
    {
        [SerializeField] private Vector2Int room;
        [SerializeField] private GameObject repairUI;
        [SerializeField] private UnityEngine.UI.Button repairButton;
        [SerializeField] private Text turnsText;
        [SerializeField] private int turnsToFix = 1;
        public bool pointFixed;

        public Action<RepairPointView> OnRepairPointFixButtonPressed { get; set; }
        
        protected override void SetModel()
        {
            model.Room = room;
            model.TurnsToFix = turnsToFix;
            model.TurnsLeftToFix = turnsToFix;
        }

        public override void Construct()
        {
            base.Construct();
            turnsText.text = $"Repair: {turnsToFix.ToString()} turns left";
        }

        private void Awake()
        {
            repairButton.onClick.AddListener(()=> OnRepairPointFixButtonPressed?.Invoke(this));
        }

        public void Draw(bool draw)
        {
            UpdateFixProgress();
            if(draw)
                ShowUI();
            else
            {
                HideUI();
            }
        }

        public void ShowUI()
        {
            if(pointFixed) return;
            repairUI.SetActive(true);
        }

        public void HideUI()
        {
            repairUI.SetActive(false);
        }

        private void UpdateFixProgress()
        {
            turnsText.text = $"Repair: {turnsToFix.ToString()} turns left";
            if (turnsToFix > 0) return;
        
            pointFixed = true;
           // viewModel.SetActive(false);
            HideUI();
        }
    }
}
