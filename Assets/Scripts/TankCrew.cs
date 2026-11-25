using UnityEngine;
using Photon.Pun;

public class TankCrew : MonoBehaviourPun
{
    public enum CrewPosition
    {
        Driver,
        Gunner,
        Commander
    }

    [Header("Crew Positions")]
    public Transform driverSeat;
    public Transform gunnerSeat;
    public Transform commanderSeat;
    public Transform exitPoint;

    [Header("Crew Cameras")]
    public Camera gunnerCamera;
    public Camera commanderCamera;

    [Header("Seat Occupancy")]
    public int driverID = -1;
    public int gunnerID = -1;
    public int commanderID = -1;

    public bool IsSeatAvailable(CrewPosition seat)
    {
        switch (seat)
        {
            case CrewPosition.Driver:
                return driverID == -1;

            case CrewPosition.Gunner:
                return gunnerID == -1;

            case CrewPosition.Commander:
                return commanderID == -1;

            default:
                return false;
        }
    }
    public void RequestSeat(int seat)
    {
        photonView.RPC(
            nameof(ReserveSeat),
            RpcTarget.MasterClient,
            seat
        );
    }
    [PunRPC]
    void ReserveSeat(int seat, PhotonMessageInfo info)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (!IsSeatAvailable((CrewPosition)seat))
            return;

        int playerID = info.Sender.ActorNumber;

        // Prevent a player from occupying multiple seats
        if (driverID == playerID ||
            gunnerID == playerID ||
            commanderID == playerID)
            return;

        UpdateSeat(seat, playerID);

        photonView.RPC(
            nameof(UpdateSeat),
            RpcTarget.AllViaServer,
            seat,
            playerID
        );
    }
    [PunRPC]
    void UpdateSeat(int seat, int playerID)
    {
        switch ((CrewPosition)seat)
        {
            case CrewPosition.Driver:
                driverID = playerID;
                break;

            case CrewPosition.Gunner:
                gunnerID = playerID;
                break;

            case CrewPosition.Commander:
                commanderID = playerID;
                break;
        }

        Debug.Log("Seat " + (CrewPosition)seat +
                  " occupied by player " + playerID);
    }
}