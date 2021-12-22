using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Units.CatUnits
{
    [RequireComponent(typeof(ICatBomb))]
    public class CatBombTickingAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float blinkingRate = 0.5f;
        [SerializeField] private Color normalColor, blinkColor = Color.red;
        [SerializeField] private Sprite[] timerSprites;

        private ICatBomb _catBomb;
        private int _turns;
        private int _spriteIndex;
        private Coroutine _tickingRoutine;
        
        private void Awake()
        {
            _catBomb = GetComponent<ICatBomb>();
        }

        public void AddTurn()
        {
            _turns++;
            spriteRenderer.sprite = timerSprites[_turns];
        }
        public void RemoveTurn()
        {
            _turns--;
            spriteRenderer.sprite = timerSprites[_turns];
        }

        public void StartExploding()
        {
        }
    }
}