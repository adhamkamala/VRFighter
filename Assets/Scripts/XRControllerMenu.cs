using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XRControllerMenu : MonoBehaviour
{
    public ActionBasedController leftController;
    public ActionBasedController rightController;
    private int pressCount = 0;
    private bool isWaitingForNextPress = false;
    private float doublePressTimeThreshold = 0.4f;
    private Coroutine resetPressCountCoroutine;
    private bool showMenu = false;
    public GameObject leftGloves;
    public GameObject rightGloves;
    public GameObject leftCont;
    public GameObject rightCont;
    public XRRayInteractor rayInteractorLeft;
    public XRRayInteractor rayInteractorRight;
    public LineRenderer lineRendererLeft;
    public LineRenderer lineRendererRight;
    public XRInteractorLineVisual xrInteractorLineVisualLeft;
    public XRInteractorLineVisual xrInteractorLineVisualRight;
    public GameObject UICanvasMode1;
    public GameObject UICanvasMode2;
    public VisibilitySystem trainerDoll2;
    public Gamesystem gameSystem;
    public RoundSystem roundSystem;

    private void Start()
    {
        leftController.selectAction.action.performed += OnMenuButtonPressed;
    }
    private void OnDestroy()
    {
        if (leftController != null)
        {
            leftController.selectAction.action.performed -= OnMenuButtonPressed;
        }
    }
    private void OnMenuButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isWaitingForNextPress)
        {
            //Debug.Log("Double press detected!");
            showMenu = !showMenu;
            if (showMenu)
            {
                ShowMenu();
            } else
            {
                HideMenu();
            }
         
            pressCount = 0;
            isWaitingForNextPress = false;
            if (resetPressCountCoroutine != null)
                StopCoroutine(resetPressCountCoroutine);
        }
        else
        {
            pressCount++;
            isWaitingForNextPress = true;
            if (resetPressCountCoroutine != null)
                StopCoroutine(resetPressCountCoroutine);
            resetPressCountCoroutine = StartCoroutine(ResetPressCount());
        }
    }

    private IEnumerator ResetPressCount()
    {
        yield return new WaitForSeconds(doublePressTimeThreshold);
        if (pressCount == 1)
        {
            pressCount = 0;
            isWaitingForNextPress = false;
        }
    }
    private void ShowMenu()
    {
        // Hide GLoves show lines
        if (gameSystem.getGameMode() == 0) // Normal NPC trainer //NormalTrainerMode
        {
            UICanvasMode1.SetActive(true);
            roundSystem.HideUINPC();
            roundSystem.FreezeTimer();
        }
        else if (gameSystem.getGameMode() == 1) // PUN NET Trainer // NetTrainerMode
        {
            UICanvasMode2.SetActive(true);
            roundSystem.HideUINet();
        }
      
        lineRendererLeft.enabled = true;
        xrInteractorLineVisualLeft.enabled = true;
        rayInteractorLeft.enabled = true;
        lineRendererRight.enabled = true;
        xrInteractorLineVisualRight.enabled = true;
        rayInteractorRight.enabled = true;
        // TrainerDoll.SetActive(false);
        trainerDoll2.Hide();

    }
    private void HideMenu()
    {
        // Hide Lines show gloves
        if (gameSystem.getGameMode() == 0) // Normal NPC trainer //NormalTrainerMode
        {
            UICanvasMode1.SetActive(false);
            roundSystem.ShowUINPC();
            roundSystem.UnfreezeTimer();
        }
        else if (gameSystem.getGameMode() == 1) // PUN NET Trainer // NetTrainerMode
        {
            UICanvasMode2.SetActive(false);
            roundSystem.ShowUINet();
        }
        lineRendererLeft.enabled = false;
       // TrainerDoll.SetActive(true);
        trainerDoll2.Show();
        xrInteractorLineVisualLeft.enabled = false;
        rayInteractorLeft.enabled = false;
        lineRendererRight.enabled = false;
        xrInteractorLineVisualRight.enabled = false;
        rayInteractorRight.enabled = false;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
