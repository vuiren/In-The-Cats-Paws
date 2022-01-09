using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Photon.Pun;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkCurrentPlayerService: ICurrentPlayerService
    {
        PlayerType ICurrentPlayerService.CurrentPlayerType() => 
            PhotonNetwork.IsMasterClient ? PlayerType.Engineer : PlayerType.SmartCat;
    }
}