using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static PhotonRoom Room;

        [SerializeField] private GameObject photonUserPrefab = default;
        [SerializeField] private GameObject targetObjectWithPhotonView;  // RigsterationCube 오브젝트
        [SerializeField] private GameObject targetObject; // 내가 보내야하는 머리 위치가 있어야함

        private Player[] photonPlayers;
        private int playersInRoom;
        private int myNumberInRoom;

        private RegisterationCube registerationCubeScript;
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom++;
        }

        private void Awake()
        {
            if (Room == null)
            {
                Room = this;
            }
            else
            {
                if (Room != this)
                {
                    Destroy(Room.gameObject);
                    Room = this;
                }
            }

            if (targetObjectWithPhotonView != null)
            {
                registerationCubeScript = targetObjectWithPhotonView.GetComponent<RegisterationCube>();
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void Start()
        {
            if (PhotonNetwork.PrefabPool is DefaultPool pool)
            {
                if (photonUserPrefab != null) pool.ResourceCache.Add(photonUserPrefab.name, photonUserPrefab);
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom = photonPlayers.Length;
            myNumberInRoom = playersInRoom;
            PhotonNetwork.NickName = myNumberInRoom.ToString();

            StartGame();
        }

        private void StartGame()
        {
            CreatePlayer();

            if (!PhotonNetwork.IsMasterClient) return;
        }

        private void CreatePlayer()
        {
            var player = PhotonNetwork.Instantiate(photonUserPrefab.name, Vector3.zero, Quaternion.identity);
        }

        public void SendPosition()
        {
            if (registerationCubeScript != null)
            {
                Transform transfom = targetObject.transform;
                registerationCubeScript.SendPosition(transfom);
            }
            else
            {
                Debug.LogError("RegisterationCube script is not assigned.");
            }
        }
    }
}
