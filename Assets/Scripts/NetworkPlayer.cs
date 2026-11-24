using UnityEngine;
using Photon.Pun;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using StarterAssets;

public class NetworkPlayer : MonoBehaviourPun
{
    public Transform cameraTarget;

    void Start()
    {
        Debug.Log("Player ownership: " + photonView.IsMine);
        if (photonView.IsMine)
        {
            CinemachineCamera followCamera =
                FindAnyObjectByType<CinemachineCamera>();

            if (followCamera != null)
            {
                followCamera.Follow = cameraTarget;
            }
        }
        else
        {
            GetComponent<PlayerInput>().enabled = false;
            GetComponent<ThirdPersonController>().enabled = false;
            GetComponent<StarterAssetsInputs>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
        }
    }
}