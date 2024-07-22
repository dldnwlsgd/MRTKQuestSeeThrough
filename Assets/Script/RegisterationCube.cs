using Photon.Pun;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class RegisterationCube : MonoBehaviourPun
    {
        private PhotonView pv;
        private Vector3 position;
        private Quaternion rotation;


        private void Awake()
        {
            pv = GetComponent<PhotonView>();
        }

        [PunRPC]
        public void RPC_SendPosition(Vector3 getposition, Quaternion getrotation)
        {
            Debug.Log("Received position: " + getposition + " and rotation: " + getrotation);
            position = getposition;
            rotation = getrotation;
            Debug.Log(GetPosition());
            Debug.Log(GetRotation());

            // position과 rotation 값을 사용하는 추가 로직을 여기에 추가할 수 있습니다.
        }

        public void SendPosition(Transform transform)
        {
            if (pv != null)
            {
                position = transform.position;
                rotation = transform.rotation;
                pv.RPC("RPC_SendPosition", RpcTarget.Others, position, rotation);
            }
            else
            {
                Debug.LogError("PhotonView is not assigned.");
            }
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public Quaternion GetRotation()
        {
            return rotation;
        }



    }
}
