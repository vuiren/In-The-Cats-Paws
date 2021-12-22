using System;
using Game_Code.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Code.MonoBehaviours.Units.CatUnits.UI
{
    public class ExplosionUI: MonoBehaviour
    {
        [SerializeField] private Button addTurnButton, removeTurnButton, explodeButton;
        [SerializeField] private TextMeshProUGUI turnsText;

        private ICatBomb _catBomb;
        private ITurnService _turnService;
        private int _turns;
        
        public void Construct(ITurnService turnService, ICatBomb catBomb)
        {
            _turnService = turnService;
            _catBomb = catBomb;
            _turnService.OnEngineerTurn(UpdateTurns);
            _turnService.OnSmartCatTurn(UpdateTurns);
        }

        private void UpdateTurns()
        {
            switch (_catBomb.GetCatBombState())
            {
                case CatBombState.NotExploding:
                    break;
                case CatBombState.Exploding:
                    RemoveTurn();
                    break;
                case CatBombState.Reviving:
                    RemoveTurn();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void Awake()
        {
            addTurnButton.onClick.AddListener(AddTurn);
            removeTurnButton.onClick.AddListener(RemoveTurn);
            explodeButton.onClick.AddListener(Explode);
        }

        private void AddTurn()
        {
            _turns++;
            turnsText.text = _turns.ToString();
        }

        private void RemoveTurn()
        {
            _turns--;
            turnsText.text = _turns.ToString();
        }

        private void Explode()
        {
            _catBomb.StartExploding(_turns);
            gameObject.SetActive(false);
        }
    }
}