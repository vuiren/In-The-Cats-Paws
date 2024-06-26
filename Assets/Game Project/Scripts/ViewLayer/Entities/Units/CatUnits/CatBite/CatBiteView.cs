﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBite
{
    public sealed class CatBiteView : UnitView
    {
        [SerializeField] private GameObject ui;
        [SerializeField] private Button biteButton;

        public Action OnBiteButtonPressed { get; set; }
        
        [Inject]
        public override void Construct()
        {
            base.Construct();
        }

        private void Awake()
        {
            biteButton.onClick.AddListener(Bite);
        }

        private void Bite()
        {
            OnBiteButtonPressed?.Invoke();
        }
        

        public void Draw(bool draw)
        {
            if (draw)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }

        public void ShowUI()
        {
            ui.SetActive(true);
        }

        public void HideUI()
        {
            ui.SetActive(false);
        }
    }
}