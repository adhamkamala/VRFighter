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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HapticLeftSuccess()
    {
       leftController.SendHapticImpulse(successAmplitude, vibrationDuration);

    }

    public void HapticRightSuccess()
    {
        rightController.SendHapticImpulse(successAmplitude, vibrationDuration);

    }

    public void HapticLeftFail()
    {
        StartCoroutine(Vibrate(failureAmplitude, vibrationDuration, leftController));
    }

    public void HapticRightFail()
    {
        StartCoroutine(Vibrate(failureAmplitude, vibrationDuration, rightController));
    }

    private IEnumerator Vibrate(float amplitude, float duration, ActionBasedController controller)
    {

        controller.SendHapticImpulse( amplitude, duration);
        yield return new WaitForSeconds(duration+0.1f);
        controller.SendHapticImpulse(amplitude, duration);
    }
}
