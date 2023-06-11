using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class NetworkManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    // Start is called before the first frame update
    void Start()
    {
       ConnectToServer();
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the Room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to Photon server.");
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("Room1");
        Debug.Log("Room Created");
       
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //base.OnJoinedRoom();
    }
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 1)
        {
            Debug.Log("Phone has joined the room!");
            SceneManager.LoadScene("Prototype");
        }
        else
        {
        }
    }
}
