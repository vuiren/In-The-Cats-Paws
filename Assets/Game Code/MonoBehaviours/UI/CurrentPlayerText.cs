using Game_Code.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game_Code.MonoBehaviours.UI
{
    public class CurrentPlayerText:MonoBehaviour
    {
        [SerializeField] private Text text;

        private ITurnService _turnService;

        [Inject]
        public void Construct(ITurnService turnService)
        {
            _turnService = turnService;
            
            _turnService.OnEngineerTurn(SetEngineerText);
            _turnService.OnSmartCatTurn(SetSmartCatText);
        }

        private void SetSmartCatText()
        {
            text.text = "Smart Cat's turn";
        }

        private void SetEngineerText()
        {
            text.text = "Engineer's turn";
        }
    }
}