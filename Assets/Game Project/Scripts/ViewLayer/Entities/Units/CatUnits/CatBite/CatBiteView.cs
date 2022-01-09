using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBite
{
    public sealed class CatBiteView : UnitView
    {
        [SerializeField] private GameObject ui;

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