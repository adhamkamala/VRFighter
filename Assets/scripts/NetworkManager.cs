using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            ConnectToServer();
        }

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

    public override void OnJoinedLobby()
    {
       // mainMenu.playMuliMode();
    }
    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom("Room1", roomOptions);
            Debug.Log("Room creation requested.");
        }
        else
        {
            Debug.Log("Not connected to Master Server. Wait for callback: OnConnectedToMaster.");
        }

       // PhotonNetwork.JoinRoom("Room1");
       
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
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name != "Prototype")
            {
                SceneManager.LoadScene("Prototype");
            }
        }
        else
        {
        }
    }
    public void BackToMainMenu()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LeaveRoom();
        }
       // PhotonNetwork.Disconnect();
        Debug.Log("Disconnected from Photon server. Reconnecting...");
        SceneManager.LoadScene("MainMenu");
      //  ConnectToServer();
    }
}
    