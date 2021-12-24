using System;
using Game_Code.Controllers.CatBotControllers;
using Game_Code.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Code.MonoBehaviours.Units.CatUnits.UI
{
    public class ExplosionUI: MonoBehaviour
    {
        [SerializeField] private Button addTurnButton, removeTurnButton, explodeButton;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] turnSprites;
        
        private ICatBomb _catBomb;
        private ITurnService _turnService;
        private int _turns;
        private ICatBotExplosionController _explosionController;
        
        public void Construct(ITurnService turnService, ICatBomb catBomb, ICatBotExplosionController explosionController)
        {
            _explosionController = explosionController;
            _turnService = turnService;
            _catBomb = catBomb;
            _explosionController.OnExplodingStart(Sync);
            _turnService.OnSmartCatTurn(UpdateTurns);
        }

        private void Sync(CatBomb obj)
        {
            if (obj.gameObject == gameObject)
            {
                _turns = _explosionController.TurnsUntilExplosion();
                UpdateSprite(_turns);
            }
        }

        private void UpdateTurns()
        {
            switch (_catBomb.GetCatBombState())
            {
                case CatBombState.NotExploding:
                    break;
                case CatBombState.Exploding:
                    Reduce();
                    break;
                case CatBombState.Reviving:
                    Reduce();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void Awake()
        {
            addTurnButton.onClick.AddListener(Add);
            removeTurnButton.onClick.AddListener(Reduce);
            explodeButton.onClick.AddListener(Explode);
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
        
        private void UpdateSprite(int turnsCount)
        {
            spriteRenderer.sprite = turnSprites[turnsCount];
        }

        private void Explode()
        {
            _catBomb.StartExploding(_turns);
            gameObject.SetActive(false);
        }
    }
}