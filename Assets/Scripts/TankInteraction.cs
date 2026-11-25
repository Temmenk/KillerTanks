using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class TankInteraction : MonoBehaviour
{
    public GameObject crewSelectionPanel;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            OpenMenu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView player = other.GetComponentInParent<PhotonView>();

        if (player != null && player.IsMine &&
            player.GetComponent<NetworkPlayer>() != null)
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView player = other.GetComponentInParent<PhotonView>();

        if (player != null && player.IsMine &&
            player.GetComponent<NetworkPlayer>() != null)
        {
            playerNearby = false;
        }
    }

    public void OpenMenu()
    {
        crewSelectionPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        crewSelectionPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}