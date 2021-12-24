using Game_Code.MonoBehaviours.Level;
using Game_Code.Network.Syncs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Button = UnityEngine.UI.Button;

public class RepairPoint : MonoBehaviour
{
    [SerializeField] private GameObject repairUI;
    [SerializeField] private GameObject model;
    [SerializeField] private Button repairButton;
    [SerializeField] private Text turnsText;
    [SerializeField] private int turnsToFix = 1;
    
    private INetworkTurnsSync _turnsSync;
    public Room room;
    public bool pointFixed;

    [Inject]
    public void Construct(INetworkTurnsSync turnsSync, INetworkRepairSync repairSync)
    {
        _turnsSync = turnsSync;
        turnsText.text = $"Repair: {turnsToFix.ToString()} turns left";
        repairButton.onClick.AddListener(() => _turnsSync.EndCurrentTurn());
        repairButton.onClick.AddListener(() => repairSync.RepairPoint(this));
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

    public void FixProgress()
    {
        turnsToFix--;
        turnsText.text = $"Repair: {turnsToFix.ToString()} turns left";
        if (turnsToFix > 0) return;
        
        pointFixed = true;
        model.SetActive(false);
        HideUI();
    }
}
