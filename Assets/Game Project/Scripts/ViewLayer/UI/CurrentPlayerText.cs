using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.UI
{
    public class CurrentPlayerText:MonoBehaviour
    {
        [SerializeField] private Text text;

        private ITurnService _turnService;

        [Inject]
        public void Construct(ITurnService turnService)
        {
            _turnService = turnService;
            
            _turnService.OnTurn(SetText);
        }

        private void SetText(Turn obj)
        {
            text.text = obj == Turn.Engineer ? "Engineer's turn" : "Smart Cat's turn";
        }
    }
}