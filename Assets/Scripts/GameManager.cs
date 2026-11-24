using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public string playerPrefabLocation = "Player";
    public Transform[] spawnPoints;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        Transform spawn = spawnPoints[
            Random.Range(0, spawnPoints.Length)];

        PhotonNetwork.Instantiate(
            playerPrefabLocation,
            spawn.position,
            spawn.rotation);
    }
}