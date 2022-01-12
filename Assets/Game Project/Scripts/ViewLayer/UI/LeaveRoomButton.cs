using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Project.Scripts.ViewLayer.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class LeaveRoomButton: MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(()=>PhotonNetwork.LeaveRoom());
        }
    }
}