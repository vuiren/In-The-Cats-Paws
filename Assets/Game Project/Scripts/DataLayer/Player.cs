namespace Game_Project.Scripts.DataLayer
{
    public class Player
    {
        public PlayerType PlayerType { get; set; }
        public bool IsReady { get; set; }

        public override string ToString()
        {
            return $"Player {PlayerType} is ready: {IsReady}";
        }
    }
}