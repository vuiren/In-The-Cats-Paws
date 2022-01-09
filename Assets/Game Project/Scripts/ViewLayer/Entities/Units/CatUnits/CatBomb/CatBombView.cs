using System;
using Game_Project.Scripts.DataLayer.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBomb
{
    public sealed class CatBombView : UnitView
    {
        [SerializeField] private GameObject explosionUI;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Button addTurnButton, removeTurnButton, explodeButton;
        [SerializeField] private int revivingTurnsCount = 1;
        [SerializeField] private Sprite[] turnSprites;

        public Action<Unit, int> OnExplodeButtonPressed { get; set; }
        private int _turns;
        
        private void Awake()
        {
            addTurnButton.onClick.AddListener(Add);
            removeTurnButton.onClick.AddListener(Reduce);
            explodeButton.onClick.AddListener(Explode);
        }

        private void Explode()
        {
            OnExplodeButtonPressed?.Invoke(model, _turns);
        }

        private void Add()
        {
            _turns++;

            if (_turns >= turnSprites.Length)
            {
                _turns = 0;
            }
            
            UpdateSprite(_turns);
        }

        private void Reduce()
        {
            _turns--;

            if (_turns < 0)
            {
                _turns = turnSprites.Length - 1;
            }
            UpdateSprite(_turns);
        }

        public void Draw(bool draw)
        {
            explosionUI.gameObject.SetActive(draw);
        }
        
        private void UpdateSprite(int turnsCount)
        {
            spriteRenderer.sprite = turnSprites[turnsCount];
        }
    }
}