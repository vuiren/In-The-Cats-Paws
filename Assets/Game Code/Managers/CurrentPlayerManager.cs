using Photon.Pun;

namespace Game_Code.Managers
{
    public class CurrentPlayerManager
    {
        public PlayerType CurrentPlayerType => PhotonNetwork.IsMasterClient ? PlayerType.Engineer : PlayerType.SmartCat;

        public CurrentPlayerManager(ILogger logger)
        {
            logger.Log($"Current player is {CurrentPlayerType}");
        }
    }

    public enum PlayerType
    {
        Engineer,
        SmartCat
    }
}