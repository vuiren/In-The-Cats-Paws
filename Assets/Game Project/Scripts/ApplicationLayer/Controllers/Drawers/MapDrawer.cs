using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Drawers
{
    public sealed class MapDrawer: IMapDrawer
    {
        private readonly ICurrentPlayerService _playerService;
        private readonly CatMapDrawer _catMapDrawer;
        private readonly EngineerMapDrawer _engineerMapDrawer;

        public MapDrawer(ICurrentPlayerService playerService, CatMapDrawer catMapDrawer, EngineerMapDrawer engineerMapDrawer,
            ITurnService turnService)
        {
            _playerService = playerService;
            _catMapDrawer = catMapDrawer;
            _engineerMapDrawer = engineerMapDrawer;
            
            turnService.OnTurn(x=>ReDrawMap());
        }

        public void ReDrawMap()
        {
            if (_playerService.CurrentPlayerType() == PlayerType.Engineer)
            {
                _engineerMapDrawer.ReDrawMap();
            }
            else
            {
                _catMapDrawer.ReDrawMap();
            }
        }
    }
}