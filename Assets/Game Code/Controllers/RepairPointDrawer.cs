using System.Linq;
using Game_Code.Managers;
using Game_Code.MonoBehaviours.Units;
using Game_Code.Services;

namespace Game_Code.Controllers
{
    public class RepairPointDrawer
    {
        private readonly IUnitsService _unitsService;
        private readonly IUnitRoomService _unitRoomService;
        private readonly IRepairPointsService _repairPointsService;
        private readonly CurrentPlayerManager _currentPlayerManager;
        private RepairPoint _repairPoint;


        public RepairPointDrawer(ITurnService turnService, IUnitsService unitsService, IUnitRoomService unitRoomService,
            IRepairPointsService repairPointsService, CurrentPlayerManager currentPlayerManager)
        {
            _unitsService = unitsService;
            _unitRoomService = unitRoomService;
            _repairPointsService = repairPointsService;
            _currentPlayerManager = currentPlayerManager;

            foreach (var repairPoint in _repairPointsService.GetAll())
            {
                repairPoint.HideUI();
            }
            
            turnService.OnEngineerTurn(RedrawRepairPoints);
            turnService.OnSmartCatTurn(RedrawRepairPoints);
        }

        private void RedrawRepairPoints()
        {
            if(_currentPlayerManager.CurrentPlayerType != PlayerType.Engineer) return;
            
            if (_repairPoint)
            {
                _repairPoint.HideUI();    
            }
            
            var engineerUnit = _unitsService.GetUnitsByUnitType(UnitType.Engineer).Single();
            var room = _unitRoomService.FindUnitRoom(engineerUnit);
            _repairPoint = _repairPointsService.GetRepaintPointInRoom(room);

            if (_repairPoint)
            {
                _repairPoint.ShowUI();
            }
        }
    }
}