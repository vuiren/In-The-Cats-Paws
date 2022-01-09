using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.NetworkLayer.Base
{
    [RequireComponent(typeof(PhotonView))]
    public abstract class NetworkService: MonoBehaviour
    {
        protected PhotonView PhotonView;

        [Inject]
        protected virtual void Construct()
        {
            PhotonView = GetComponent<PhotonView>();
        }
    }
}