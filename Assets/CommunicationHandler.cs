using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CommunicationHandler : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static CommunicationHandler Instance;
    public NetTrainerMode NetTrainerMode;

    private const byte LButtonRedPressEventCode = 1;
    private const byte LButtonBlueEventCode = 2;
    private const byte LButtonUnderEventCode = 3;
    private const byte LButtonMiddleEventCode = 4;
    private const byte LButtonHighEventCode = 5;
    private const byte LButtonUpperEventCode = 6;
    private const byte RButtonRedPressEventCode = 7;
    private const byte RButtonBlueEventCode = 8;
    private const byte RButtonUnderEventCode = 9;
    private const byte RButtonMiddleEventCode = 10;
    private const byte RButtonHighEventCode = 11;
    private const byte RButtonUpperEventCode = 12;


    private void Awake()
    {
        // Singleton pattern to ensure only one instance of CommunicationHandler exists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SendLButtonRedPressEvent()
    {
        PhotonNetwork.RaiseEvent(LButtonRedPressEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void SendLButtonBlueEvent()
    {
        PhotonNetwork.RaiseEvent(LButtonBlueEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendLButtonUnderEvent()
    {
        PhotonNetwork.RaiseEvent(LButtonUnderEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendLButtonMiddleEvent()
    {
        PhotonNetwork.RaiseEvent(LButtonMiddleEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendLButtonHighEvent()
    {
        PhotonNetwork.RaiseEvent(LButtonHighEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendLButtonUpperEvent()
    {
        PhotonNetwork.RaiseEvent(LButtonUpperEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void SendRButtonRedPressEvent()
    {
        PhotonNetwork.RaiseEvent(RButtonRedPressEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void SendRButtonBlueEvent()
    {
        PhotonNetwork.RaiseEvent(RButtonBlueEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendRButtonUnderEvent()
    {
        PhotonNetwork.RaiseEvent(RButtonUnderEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendRButtonMiddleEvent()
    {
        PhotonNetwork.RaiseEvent(RButtonMiddleEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendRButtonHighEvent()
    {
        PhotonNetwork.RaiseEvent(RButtonHighEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
    public void SendRButtonUpperEvent()
    {
        PhotonNetwork.RaiseEvent(RButtonUpperEventCode, null, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        // Check the event code and handle the corresponding event
        byte eventCode = photonEvent.Code;

        switch (eventCode)
        {
            case LButtonRedPressEventCode:
                HandleLButtonRedPress();
                break;
            case LButtonBlueEventCode:
                HandleLButtonBluePress();
                break;
            case LButtonUnderEventCode:
                HandleLButtonUnderPress();
                break;
            case LButtonMiddleEventCode:
                HandleLButtonMiddlePress();
                break;
            case LButtonHighEventCode:
                HandleLButtonHighPress();
                break;
            case LButtonUpperEventCode:
                HandleLButtonUpperPress();
                break;
            case RButtonRedPressEventCode:
                HandleRButtonRedPress();
                break;
            case RButtonBlueEventCode:
                HandleRButtonBluePress();
                break;
            case RButtonUnderEventCode:
                HandleRButtonUnderPress();
                break;
            case RButtonMiddleEventCode:
                HandleRButtonMiddlePress();
                break;
            case RButtonHighEventCode:
                HandleRButtonHighPress();
                break;
            case RButtonUpperEventCode:
                HandleRButtonUpperPress();
                break;
        }
    }

    private void HandleLButtonRedPress()
    {
        Debug.Log("Left Button Red Pressed");
        NetTrainerMode.HanldePadLeftColor(1);
    }

    private void HandleLButtonBluePress()
    {
        Debug.Log("Left Button Blue Pressed");
        NetTrainerMode.HanldePadLeftColor(0);
    }

    private void HandleLButtonUnderPress()
    {
        Debug.Log("Left Button Under Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerLowLeft");
    }

    private void HandleLButtonMiddlePress()
    {
        Debug.Log("Left Button Middle Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerMidLeft");
    }

    private void HandleLButtonHighPress()
    {
        Debug.Log("Left Button High Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerHighLeft");
    }

    private void HandleLButtonUpperPress()
    {
        Debug.Log("Left Button Upper Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerUpperLeft");
    }

    private void HandleRButtonRedPress()
    {
        Debug.Log("Right Button Red Pressed");
        NetTrainerMode.HanldePadRightColor(1);
    }

    private void HandleRButtonBluePress()
    {
       
        Debug.Log("Right Button Blue Pressed");
        NetTrainerMode.HanldePadRightColor(0);
    }

    private void HandleRButtonUnderPress()
    {
        Debug.Log("Right Button Under Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerLowRight");
    }

    private void HandleRButtonMiddlePress()
    {
        Debug.Log("Right Button Middle Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerMidRight");
    }

    private void HandleRButtonHighPress()
    {
        Debug.Log("Right Button High Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerHighRight");
    }

    private void HandleRButtonUpperPress()
    {
        Debug.Log("Right Button Upper Pressed");
        NetTrainerMode.HandlePadLeftAnimate("TrainerUpperRight");
    }
}