using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate(
                "Player",
                spawnPoint.position,
                spawnPoint.rotation
            );
        }
    }
}