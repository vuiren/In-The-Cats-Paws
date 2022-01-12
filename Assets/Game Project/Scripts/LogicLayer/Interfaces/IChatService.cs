using System;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IChatService
    {
        void SendChatMessage(string message);
        void OnMessageReceived(Action<string> action);
    }
}