using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandController : MonoBehaviour
{
    public ActionBasedController leftController;
    public ActionBasedController rightController;
    public float vibrationDuration = 0.2f;
    public float successAmplitude = 0.5f;
    public float failureAmplitude = 1f;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void HapticLeftSuccess()
    {
       audioManager.PlaySFX(audioManager.LeftPunch);
       leftController.SendHapticImpulse(successAmplitude, vibrationDuration);

    }

    public void HapticRightSuccess()
    {
        audioManager.PlaySFX(audioManager.rightPunch);
        rightController.SendHapticImpulse(successAmplitude, vibrationDuration);

    }

    public void HapticLeftFail()
    {
        audioManager.PlaySFX(audioManager.LeftPunch);
        StartCoroutine(Vibrate(failureAmplitude, vibrationDuration, leftController));
    }

    public void HapticRightFail()
    {
        audioManager.PlaySFX(audioManager.rightPunch);
        StartCoroutine(Vibrate(failureAmplitude, vibrationDuration, rightController));
    }

    private IEnumerator Vibrate(float amplitude, float duration, ActionBasedController controller)
    {

        controller.SendHapticImpulse( amplitude, duration);
        yield return new WaitForSeconds(duration+0.1f);
        controller.SendHapticImpulse(amplitude, duration);
    }
}
